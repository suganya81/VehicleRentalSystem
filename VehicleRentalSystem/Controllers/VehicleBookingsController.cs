using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleRentalSystem.Context;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Controllers
{
    public class VehicleBookingsController : Controller
    {
        private VehicleRentalContext db = new VehicleRentalContext();

        // GET: VehicleBookings
        public ActionResult Index()
        {
            var vehicleBookings = db.VehicleBookings.Include(v => v.User).Include(v => v.Vehicle).Include(v => v.VehicleAvailability);
            return View(vehicleBookings.ToList());
        }

        // GET: VehicleBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBooking vehicleBooking = db.VehicleBookings.Find(id);
            if (vehicleBooking == null)
            {
                return HttpNotFound();
            }
            vehicleBooking.AmountToBePaid = vehicleBooking.Vehicle.Tariff;
            return View(vehicleBooking);
        }

        // GET: VehicleBookings/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name");
            ViewBag.VehicleAvailabilityId = new SelectList(db.VehicleAvailabilities, "Id", "Id");
            return View();
        }

        // POST: VehicleBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBooking(VehicleBooking vehicleBooking)
        {
            var availability = db.VehicleAvailabilities.Include(_ => _.Vehicle).ToList().FirstOrDefault(_ => _.Id == vehicleBooking.VehicleAvailabilityId);
            var currentUser = db.Users.Where(_ => _.Email == User.Identity.Name).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var PNR = Guid.NewGuid().ToString();
                vehicleBooking.BookingNumber = PNR;
                vehicleBooking.CreatedAt = DateTime.Now;
                vehicleBooking.CreatedBy = 1;
                vehicleBooking.ModifiedAt = DateTime.Now;
                vehicleBooking.ModifiedBy = 1;
                vehicleBooking.User = currentUser;
                vehicleBooking.Vehicle = availability.Vehicle;
                vehicleBooking.Total = vehicleBooking.Vehicle.Tariff;
                db.VehicleBookings.Add(vehicleBooking);
                db.SaveChanges();
                return View("Confirmation", (object)PNR);
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", vehicleBooking.UserId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", vehicleBooking.VehicleId);
            ViewBag.VehicleAvailabilityId = new SelectList(db.VehicleAvailabilities, "Id", "Id", vehicleBooking.VehicleAvailabilityId);
            return View(vehicleBooking);
        }

        // GET: VehicleBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBooking vehicleBooking = db.VehicleBookings.Find(id);
            if (vehicleBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", vehicleBooking.UserId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", vehicleBooking.VehicleId);
            ViewBag.VehicleAvailabilityId = new SelectList(db.VehicleAvailabilities, "Id", "Id", vehicleBooking.VehicleAvailabilityId);
            return View(vehicleBooking);
        }

        // POST: VehicleBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AmountToBePaid,AdditionalCharges,AdvancePaid,Total,PaymentMode,VehicleId,UserId,VehicleAvailabilityId,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt")] VehicleBooking vehicleBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", vehicleBooking.UserId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", vehicleBooking.VehicleId);
            ViewBag.VehicleAvailabilityId = new SelectList(db.VehicleAvailabilities, "Id", "Id", vehicleBooking.VehicleAvailabilityId);
            return View(vehicleBooking);
        }

        // GET: VehicleBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBooking vehicleBooking = db.VehicleBookings.Find(id);
            if (vehicleBooking == null)
            {
                return HttpNotFound();
            }
            return View(vehicleBooking);
        }

        // POST: VehicleBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleBooking vehicleBooking = db.VehicleBookings.Find(id);
            db.VehicleBookings.Remove(vehicleBooking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BookVehicle(int id)
        {
            var availability = db.VehicleAvailabilities.Include(_ => _.Vehicle).ToList().FirstOrDefault(_ => _.Id == id);
            var currentUser = db.Users.Where(_ => _.Email == User.Identity.Name).FirstOrDefault();
            var vehicleBooking = new VehicleBooking()
            {
                UserId = currentUser.Id,
                User = currentUser,
                AmountToBePaid = availability.Vehicle.Tariff,
                VehicleAvailability = availability,
                VehicleAvailabilityId = availability.Id,
                Vehicle = availability.Vehicle
            };
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Name", vehicleBooking.VehicleId);


            return View("BookVehicle", vehicleBooking);
        }

        public ActionResult MyBookings()
        {
            var vehicleBookings = db.VehicleBookings.Include(v => v.Vehicle).Include(v => v.VehicleAvailability).Where(_ => _.User.Email == User.Identity.Name);
            return View(vehicleBookings.ToList());
        }

        public ActionResult Bookings()
        {
            var vehicleBookings = db.VehicleBookings.Include(v => v.User).Include(v => v.Vehicle).Include(v => v.VehicleAvailability).OrderBy(_ => _.CreatedBy).ToList();
            return View(vehicleBookings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
