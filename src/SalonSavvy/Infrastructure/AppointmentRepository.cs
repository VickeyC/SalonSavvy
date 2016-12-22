using SalonSavvy.Data;
using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Infrastructure
{
    public class AppointmentRepository
    {
        private ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db) {
            _db = db;
        }

        // find all appointments
        public IQueryable<Appointment> GetAllAppointments() {
            return from t in _db.Appointments
                   select t;
        }

        // find all appointment types
        public IQueryable<AppointmentType> GetAllAppointmentTypes() {
            return from at in _db.AppointmentTypes
                   select at;
        }


        public IQueryable<Appointment> GetUpcommingAppointments(DateTime start) {
            var end = start.AddDays(7);

            return from a in _db.Appointments
                   where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
                   select a;
        }

        //find a specific appointment by id
        public IQueryable<Appointment> FindAppointment(int id) {
            return from t in _db.Appointments
                   where t.Id == id
                   select t;
        }

        // add a new appointment
        public void AddAppointment(Appointment appointment) {
            _db.Appointments.Add(appointment);
            _db.SaveChanges();
        }

        public void SaveChanges() {
            _db.SaveChanges();
        }


        // delete an appointment type
        public void DeleteAppointment(Appointment appointment) {
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();
        }

    }
}

