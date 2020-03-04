using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MHMSystem.Models;
using Microsoft.AspNetCore.Http;

namespace MHMSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContextForBookings applicationDbContextForBookings;
        private readonly ApplicationDbContextForMarriageHall applicationDbContextForMarriageHall;
        private readonly ApplicationDbContext applicationDbContextForUser;

        public BookingController(ApplicationDbContextForBookings applicationDbContextForBookings, ApplicationDbContextForMarriageHall applicationDbContextForMarriageHall, ApplicationDbContext applicationDbContextForUser)
        {
            this.applicationDbContextForBookings = applicationDbContextForBookings;
            this.applicationDbContextForMarriageHall = applicationDbContextForMarriageHall;
            this.applicationDbContextForUser = applicationDbContextForUser;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult doSeeAllBookings()
        {
            string firstName = HttpContext.Session.GetString("firstName");
            if (!string.Equals("Admin", firstName))
            {
                return RedirectToAction("Index", "Home");
            }
            var allBookings = this.applicationDbContextForBookings.bookings.ToList<Bookings>();
            ViewBag.allBookings = allBookings;
            return View();
        }

        public IActionResult doModifyView()
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            int userId = (int)HttpContext.Session.GetInt32("userId");
            var allBookings = this.applicationDbContextForBookings.bookings.Where(x => x.userId == userId).ToList<Bookings>();
            ViewBag.allBookings = allBookings;
            return View();
        }
        public IActionResult doEdit(int bookingId)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            var bookings = this.applicationDbContextForBookings.bookings.Find(bookingId);
            ViewBag.booking = bookings;
            return View();
        }
        public IActionResult doDelete(int bookingId)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            var bookings = this.applicationDbContextForBookings.bookings.Find(bookingId);
            this.applicationDbContextForBookings.bookings.Remove(bookings);
            this.applicationDbContextForBookings.SaveChanges();
            return RedirectToAction("doModifyView");
        }
        public IActionResult doSubmitAfterEdit(IFormCollection formData)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            string fromDate_1 = formData["fromDate"];
            string toDate_1 = formData["toDate"];
            string bookingId = formData["bookingId"];

            var user = this.applicationDbContextForBookings.bookings.Find(int.Parse(bookingId));
            user.fromDate = fromDate_1;
            user.toDate = toDate_1;
            this.applicationDbContextForBookings.bookings.Update(user);
            this.applicationDbContextForBookings.SaveChanges();

            return RedirectToAction("doModifyView");
        }
        public IActionResult doCreateBooking()
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            var marriageHalls = this.applicationDbContextForMarriageHall.MarriageHall.ToList<MarriageHall>();
            var bookingList = this.applicationDbContextForBookings.bookings.ToList<Bookings>();
            ViewBag.marriageHallList = marriageHalls;
            ViewBag.bookingList = bookingList;
            return View();
        }

        public IActionResult doBookHall(IFormCollection formData)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }

            string hallId = formData["marriageHall"];
            string fromDate = formData["fromDate"];
            string toDate = formData["toDate"];
            int userId = (int)HttpContext.Session.GetInt32("userId");

            Bookings bookings = new Bookings();
            bookings.hallId =Int32.Parse(hallId);
            bookings.bookingHallname = "HallName_" + hallId;
            bookings.userId = userId;
            bookings.fromDate =fromDate;
            bookings.toDate = toDate;


            this.applicationDbContextForBookings.bookings.Add(bookings);
            this.applicationDbContextForBookings.SaveChanges();

            var user = this.applicationDbContextForUser.Users.Find(userId);
            user.MarriageHallId = (short)bookings.bookingId;
            this.applicationDbContextForUser.Users.Update(user);
            this.applicationDbContextForUser.SaveChanges();


            return RedirectToAction("doModifyView");
        }

        public bool checkSessionIsValidOrNot()
        {
            string firstName = HttpContext.Session.GetString("firstName");
            if (firstName == null)
            {
                return true;
            }
            return false;
        }
    }
}