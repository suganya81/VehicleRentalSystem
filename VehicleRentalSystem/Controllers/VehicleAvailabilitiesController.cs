using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleRentalSystem.Context;
using VehicleRentalSystem.Helper;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Controllers
{
    [Authorize]
    public class VehicleAvailabilitiesController : Controller
    {
        private VehicleRentalContext db = new VehicleRentalContext();

        // GET: VehicleAvailabilities
        public ActionResult Index()
        {
            return View(db.VehicleAvailabilities.ToList());
        }

        // GET: VehicleAvailabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleAvailability vehicleAvailability = db.VehicleAvailabilities.Find(id);
            if (vehicleAvailability == null)
            {
                return HttpNotFound();
            }
            return View(vehicleAvailability);
        }

        // GET: VehicleAvailabilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromDate,ToDate,IsBooked,CreatedBy,CreatedAt,VehicleId")] VehicleAvailability vehicleAvailability)
        {
            if (ModelState.IsValid)
            {
                db.VehicleAvailabilities.Add(vehicleAvailability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleAvailability);
        }

        // GET: VehicleAvailabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleAvailability vehicleAvailability = db.VehicleAvailabilities.Find(id);
            if (vehicleAvailability == null)
            {
                return HttpNotFound();
            }
            return View(vehicleAvailability);
        }

        // POST: VehicleAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromDate,ToDate,IsBooked,CreatedBy,CreatedAt,VehicleId")] VehicleAvailability vehicleAvailability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleAvailability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleAvailability);
        }

        // GET: VehicleAvailabilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleAvailability vehicleAvailability = db.VehicleAvailabilities.Find(id);
            if (vehicleAvailability == null)
            {
                return HttpNotFound();
            }
            return View(vehicleAvailability);
        }

        // POST: VehicleAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleAvailability vehicleAvailability = db.VehicleAvailabilities.Find(id);
            db.VehicleAvailabilities.Remove(vehicleAvailability);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(VehicleBookingDto dto)
        {
            var bookedVehicles = db.VehicleAvailabilities.ToList().Where(_ => (_.FromDate >= dto.StartDate
                                        && _.ToDate <= dto.EndDate)).Select(_ => _.Vehicle.Id).ToList();

            var vehicles = db.Vehicles.Where(vehicle => !bookedVehicles.Contains(vehicle.Id)).ToList();
            if (dto.VehicleId != 0)
            {
                vehicles = vehicles.Where(_ => _.Id == dto.VehicleId).ToList();
            }
            dto.Vehicles = vehicles;
            return View(dto);
        }

        [HttpPost]
        public ActionResult DoBooking(VehicleBookingDto dto)
        {            
            var selectedVehicle = db.Vehicles.ToList().FirstOrDefault(_ => _.Id == dto.VehicleId);

            var vehichleAvailability = new VehicleAvailability()
            {
                FromDate = dto.StartDate,
                ToDate = dto.EndDate,
                IsBooked = true,
                VehicleId = selectedVehicle.Id,
                Vehicle = selectedVehicle,
                CreatedAt = DateTime.Now,
                CreatedBy = 1
            };
            db.VehicleAvailabilities.Add(vehichleAvailability);
            db.SaveChanges();

            return RedirectToAction("BookVehicle", "VehicleBookings", new { vehichleAvailability.Id } );
        }


        public ActionResult Search()
        {
            var vehicles = db.Vehicles.ToList();

            var vehi = new VehicleBookingDto()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                VehicleId = 0,
                Vehicles = vehicles
            };
            return View(vehi);
        }

        public ActionResult SearchById(int id)
        {
            ViewBag.VehicleId = id;
            var vehicles = db.Vehicles.Where(_ => _.Id == id).ToList();
            var vehi = new VehicleBookingDto()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                VehicleId = id,
                Vehicles = vehicles
            };
            return View("Search", vehi);
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
