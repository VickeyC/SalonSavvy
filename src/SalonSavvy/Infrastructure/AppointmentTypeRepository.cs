using SalonSavvy.Data;
using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Infrastructure {
    public class AppointmentTypeRepository {
        private ApplicationDbContext _db;
        public AppointmentTypeRepository(ApplicationDbContext db) {
            _db = db;
        }

        // find all appointment types
        public IQueryable<AppointmentType> GetAllAppointmentTypes() {
            return from t in _db.AppointmentTypes
                   select t;
        }

        //find a specific appointment type by id
        public IQueryable<AppointmentType> FindAppointmentType(int id) {
            return from t in _db.AppointmentTypes
                   where t.Id == id
                   select t;
        }

        // add a new appointment type
        public void AddAppointmentType(AppointmentType appointmentType) {
            _db.AppointmentTypes.Add(appointmentType);
            _db.SaveChanges();
        }

        public void SaveChanges() {
            _db.SaveChanges();
        }


        // delete an appointment type
        public void DeleteAppointmentType(AppointmentType appointmentType) {
            _db.AppointmentTypes.Remove(appointmentType);
            _db.SaveChanges();
        }

    }
}
