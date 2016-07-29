using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SalonSavvy.Data;
using SalonSavvy.Infrastructure;
using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Services {
    public class AppointmentService {

        private AppointmentRepository _appointmentRepo;
        public AppointmentService(AppointmentRepository ar) {
            _appointmentRepo = ar;
        }

        // get a list of all appointments
        public IList<AppointmentDTO> GetAllAppointments() {

            return (from a in _appointmentRepo.GetAllAppointments()
                    select new AppointmentDTO() {
                        Id = a.Id,
                        CustomerName = a.Customer.UserName,
                        StylistName = a.StylistName,
                        AppointmentDateTime = a.AppointmentDateTime,
                        AppointmentType = new AppointmentTypeDTO() {
                            TypeDuration = a.AppointmentType.TypeDuration,
                            TypeName = a.AppointmentType.TypeName
                        }
                    }).ToList();

        }

        public IDictionary<DateTime, IList<AppointmentDTO>> GetUpcommingAppointments() {

            var start = DateTime.Now;
            start = start.Subtract(start.TimeOfDay);

            var appts = (from a in _appointmentRepo.GetUpcommingAppointments(start)
                         orderby a.AppointmentDateTime ascending
                         select new AppointmentDTO() {
                             Id = a.Id,
                             CustomerName = a.Customer.UserName,
                             StylistName = a.StylistName,
                             AppointmentDateTime = a.AppointmentDateTime,
                             AppointmentType = new AppointmentTypeDTO() {
                                 TypeDuration = a.AppointmentType.TypeDuration,
                                 TypeName = a.AppointmentType.TypeName
                             }
                         }).ToList();
            
            

            var apptsByDayQuery = (from a in appts
                                   group a by a.AppointmentDateTime.Subtract(a.AppointmentDateTime.TimeOfDay) into dayAppts
                                   select new { dayAppts.Key, dayAppts });
            
            var apptsByDay = new Dictionary<DateTime, IList<AppointmentDTO>>();
            foreach(var it in apptsByDayQuery) {
                apptsByDay[it.Key] = it.dayAppts.ToList();
            }

            return apptsByDay;
        }



        // find a specific appointment by id
        public IList<AppointmentDTO> FindAppointment(int Id) {

            return (from a in _appointmentRepo.FindAppointment(Id)
                    select new AppointmentDTO() {
                        CustomerName = a.Customer.UserName,
                        StylistName = a.StylistName,
                        AppointmentDateTime = a.AppointmentDateTime,
                        AppointmentType = new AppointmentTypeDTO() {
                            TypeDuration = a.AppointmentType.TypeDuration,
                            TypeName = a.AppointmentType.TypeName
                        }
                    }).ToList();
        }

        // add a new appointment
        public void AddAppointment(AppointmentDTO dto) {
            var dbItem = new Appointment() {
                Customer = new ApplicationUser(), 
                StylistName = dto.StylistName,
                AppointmentDateTime = dto.AppointmentDateTime,
                AppointmentType = new AppointmentType() {
                    TypeDuration = dto.AppointmentType.TypeDuration,
                    TypeName = dto.AppointmentType.TypeName
                }                       
            };

            _appointmentRepo.AddAppointment(dbItem);
        }


        //update an appointment
        public void UpdateAppointment(AppointmentDTO dto) {
            var dbItem = _appointmentRepo.FindAppointment(dto.Id).FirstOrDefault();

            dbItem.Customer = new ApplicationUser();
            dbItem.StylistName = dto.StylistName;
            dbItem.AppointmentDateTime = dto.AppointmentDateTime;
            dbItem.AppointmentType = new AppointmentType();

            _appointmentRepo.SaveChanges();
        }

        // delete an appointment
        public void DeleteAppointment(AppointmentDTO dto) {
            var dbItem = _appointmentRepo.FindAppointment(dto.Id).FirstOrDefault();

            dbItem.Id = dto.Id;
            _appointmentRepo.DeleteAppointment(dbItem);
        }


    }
}
