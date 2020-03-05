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
        private readonly ApplicationDbContextForHallBookingDates applicationDbContextForHallBookingDates;

        public BookingController(ApplicationDbContextForBookings applicationDbContextForBookings, ApplicationDbContextForMarriageHall applicationDbContextForMarriageHall, ApplicationDbContext applicationDbContextForUser, ApplicationDbContextForHallBookingDates applicationDbContextForHallBookingDates)
        {
            this.applicationDbContextForBookings = applicationDbContextForBookings;
            this.applicationDbContextForMarriageHall = applicationDbContextForMarriageHall;
            this.applicationDbContextForUser = applicationDbContextForUser;
            this.applicationDbContextForHallBookingDates = applicationDbContextForHallBookingDates;
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
        public IActionResult doEdit(int? bookingId)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            if (TempData["bookingId"] != null)
            {
                bookingId = Int32.Parse(TempData["bookingId"].ToString());
            }
            var bookings = this.applicationDbContextForBookings.bookings.Find(bookingId);
            ViewBag.booking = bookings;
            var marriageHallsBookedDates = this.applicationDbContextForHallBookingDates.HallBookingDates.Where(x => x.hallId == bookings.hallId).ToList<HallBookingDates>();
            ViewBag.bookingList = marriageHallsBookedDates;

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
            string fromDateOld = formData["fromDateOld"];
            string toDateOld = formData["toDateOld"];
            bool isHallAvailable = true;

            var user = this.applicationDbContextForBookings.bookings.Find(int.Parse(bookingId));
            var bookings = this.applicationDbContextForBookings.bookings.Find((int.Parse(bookingId)));
            int hallId = bookings.hallId;

            var allBookingDatesForHall = this.applicationDbContextForHallBookingDates.HallBookingDates.Where(x => x.hallId == hallId).ToList<HallBookingDates>();
            DateTime fromDateNew = Convert.ToDateTime(fromDate_1);
            DateTime toDateNew = Convert.ToDateTime(toDate_1);

            /*FromDate should be less than ToDate* and both should be greater or equal to system date*/
            if (fromDateNew.CompareTo(DateTime.Today) < 0 || toDateNew.CompareTo(DateTime.Today) < 0)
            {
                TempData["error"] = "FromDate and ToDate should be equal or greater than system date.";
                TempData["bookingId"] = bookingId;
                return RedirectToAction("doEdit");
            }
            else if (fromDateNew.CompareTo(toDateNew) > 0)
            {
                TempData["error"] = "FromDate should be less than or equal to ToDate.";
                TempData["bookingId"] = bookingId;
                return RedirectToAction("doEdit");
            }else if (string.Equals(fromDate_1,fromDateOld) && string.Equals(toDate_1,toDateOld))
            {
                return RedirectToAction("doModifyView");
            }
            /*FromDate should be less than ToDate* and both should be greater or equal to system date*/


            isHallAvailable = dateValidation(isHallAvailable, allBookingDatesForHall, fromDateNew, toDateNew);
            if (!isHallAvailable)
            {
                TempData["error"] = "Hall is not available between " + fromDate_1 + " and " + toDate_1;
                TempData["bookingId"] = bookingId;
                return RedirectToAction("doEdit");
            }
            /*check hall is available for booking on specefied date*/

            user.fromDate = fromDate_1;
            user.toDate = toDate_1;
            this.applicationDbContextForBookings.bookings.Update(user);

            /*Update hall Booked date table*/
            var marriageHallsBookedDates = this.applicationDbContextForHallBookingDates.HallBookingDates.Where(x => x.fromDate == fromDateOld && x.toDate == toDateOld && x.hallId == hallId).ToList<HallBookingDates>();
            
            HallBookingDates hallBookingDates = marriageHallsBookedDates.First<HallBookingDates>();
            hallBookingDates.fromDate = fromDate_1;
            hallBookingDates.toDate = toDate_1;
            this.applicationDbContextForHallBookingDates.HallBookingDates.Update(hallBookingDates);
            this.applicationDbContextForHallBookingDates.SaveChanges();
            this.applicationDbContextForBookings.SaveChanges();

            return RedirectToAction("doModifyView");
        }
        public IActionResult doCreateBooking(int? hallId)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            var marriageHalls = this.applicationDbContextForMarriageHall.MarriageHall.ToList<MarriageHall>();
            ViewBag.marriageHallList = marriageHalls;
            if(TempData["hallId"] != null)
            {
                hallId = Int32.Parse(TempData["hallId"].ToString());
            }
            if (hallId >= 0)
            {
                var marriageHallsBookedDates = this.applicationDbContextForHallBookingDates.HallBookingDates.Where(x=>x.hallId == hallId).ToList<HallBookingDates>();
                ViewBag.bookingList = marriageHallsBookedDates;
                ViewBag.selectedHallId = hallId;
            }
            TempData.Remove("hallId");
            return View();
        }

        public IActionResult doBookHall(IFormCollection formData)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            bool isHallAvailable = true;
            string hallId = formData["marriageHall"];
            string fromDate = formData["fromDate"];
            string toDate = formData["toDate"];
            int userId = (int)HttpContext.Session.GetInt32("userId");



            /*check hall is available for booking on specefied date*/
            var allBookingDatesForHall = this.applicationDbContextForHallBookingDates.HallBookingDates.Where(x => x.hallId == Int32.Parse(hallId)).ToList<HallBookingDates>();
            DateTime fromDateNew = Convert.ToDateTime(fromDate);
            DateTime toDateNew = Convert.ToDateTime(toDate);

            /*FromDate should be less than ToDate* and both should be greater or equal to system date*/
            if (fromDateNew.CompareTo(DateTime.Today) < 0 || toDateNew.CompareTo(DateTime.Today) < 0)
            {
                TempData["error"] = "FromDate and ToDate should be equal or greater than system date.";
                TempData["hallId"] = hallId;
                return RedirectToAction("doCreateBooking");
            }
            else if (fromDateNew.CompareTo(toDateNew) > 0)
            {
                TempData["error"] = "FromDate should be less than or equal to ToDate.";
                TempData["hallId"] = hallId;
                return RedirectToAction("doCreateBooking");
            }
            /*FromDate should be less than ToDate* and both should be greater or equal to system date*/


            isHallAvailable = dateValidation(isHallAvailable, allBookingDatesForHall, fromDateNew, toDateNew);
            if (!isHallAvailable)
            {
                TempData["error"] = "Hall is not available between " + fromDate + " and " + toDate;
                TempData["hallId"] = hallId;
                return RedirectToAction("doCreateBooking");
            }
            /*check hall is available for booking on specefied date*/



            Bookings bookings = new Bookings();
            bookings.hallId = Int32.Parse(hallId);
            bookings.bookingHallname = "HallName_" + hallId;
            bookings.userId = userId;
            bookings.fromDate = fromDate;
            bookings.toDate = toDate;


            this.applicationDbContextForBookings.bookings.Add(bookings);
            this.applicationDbContextForBookings.SaveChanges();

            var user = this.applicationDbContextForUser.Users.Find(userId);
            user.MarriageHallId = (short)bookings.bookingId;
            this.applicationDbContextForUser.Users.Update(user);
            this.applicationDbContextForUser.SaveChanges();

            HallBookingDates bookingDates = new HallBookingDates();
            bookingDates.hallId = Int32.Parse(hallId);
            bookingDates.hallName = "HallName_" + hallId;
            bookingDates.fromDate = fromDate;
            bookingDates.toDate = toDate;
            this.applicationDbContextForHallBookingDates.HallBookingDates.Add(bookingDates);
            this.applicationDbContextForHallBookingDates.SaveChanges();


            return RedirectToAction("doModifyView");
        }

        private static bool dateValidation(bool isHallAvailable, List<HallBookingDates> allBookingDatesForHall, DateTime fromDateNew, DateTime toDateNew)
        {
            DateTime dbFromDate = new DateTime();
            DateTime dbToDate = new DateTime();
            foreach (var item in allBookingDatesForHall)
            {
                dbFromDate = Convert.ToDateTime(item.fromDate);
                dbToDate = Convert.ToDateTime(item.toDate);
                if (dbFromDate.CompareTo(fromDateNew) <= 0 && dbToDate.CompareTo(toDateNew) >= 0)
                {
                    isHallAvailable = false;
                    break;
                }
                else if (dbFromDate.CompareTo(fromDateNew) >= 0 && dbToDate.CompareTo(toDateNew) >= 0 && dbFromDate.CompareTo(toDateNew) <= 0)
                {
                    isHallAvailable = false;
                    break;
                }
                else if (dbFromDate.CompareTo(fromDateNew) <= 0 && dbToDate.CompareTo(fromDateNew) >= 0)
                {
                    isHallAvailable = false;
                    break;
                }
            }

            return isHallAvailable;
        }

        public bool checkSessionIsValidOrNot()
        {
            string firstName = HttpContext.Session.GetString("firstName");
            if (firstName == null || string.Equals("Admin",firstName))
            {
                return true;
            }
            return false;
        }
    }
}