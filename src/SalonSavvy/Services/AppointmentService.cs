using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SalonSavvy.Data;
using SalonSavvy.Models;
using SalonSavvy.Services;
using System.Collections;
using SalonSavvy.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace SalonSavvy.Services {
    public class AppointmentService {
        private GenericRepository _repo;
        public AppointmentService(GenericRepository repo) {
            _repo = repo;
            }

        public IQueryable<TechAppointment> GetUpcomingAppointments() {


            // get the date for today and the next 6 days

            var scheduleDays = new DateTime[6];
            var start = DateTime.Today;
            start = start.Subtract(start.TimeOfDay);
            var end = start.AddDays(7);

            var count = 0;
            var dayCount = 0;
            while(count < 6) {

                var schedDate = DateTime.Today.AddDays(dayCount);
                var dayOfWeek = schedDate.DayOfWeek;

                if(dayOfWeek == DayOfWeek.Sunday) {
                    dayCount++;
                    schedDate = DateTime.Today.AddDays(dayCount);
                    }
                scheduleDays[count] = schedDate;

                dayCount++;
                count++;
                };

            var TechsAndAppts = from u in _repo.Query<ApplicationUser>()
                                where (u.IsTechnician)
                                select new TechAppointment {

                                    TechId = u.Id,
                                    TechName = (u.FirstName + " " + u.LastName),
                                    DayOff = u.DayOff,
                                    BeginLunchBreak = u.BeginLunchBreak,
                                    EndLunchBreak = u.EndLunchBreak,
                                    IsStylist = u.IsStylist,
                                    IsNailTech = u.IsNailTech,
                                    IsEstician = u.IsEstician,
                                    Appointments = (from v in u.TechAppointments
                                                    where (v.AppointmentDateTime >= start && v.AppointmentDateTime < end)
                                                    select new AppointmentViewModel {
                                                        AppointmentDateTime = v.AppointmentDateTime,
                                                        AppointmentTypeName = v.AppointmentTypeName

                                                        }).ToList()
                                    };


            //foreach(TechAppointment a in TechsAndAppts) {
            //    (from at in _repo.Query<AppointmentType>()
            //         //where (a.AppointmentTypeId = at.Id)
            //     select new AppointmentTypeViewModel {
            //         TypeName = at.TypeName
            //         }).ToList();
            //    };

            return TechsAndAppts;
            }



        //public IList<AppointmentDTO> GetUpcomingAppointments() {
        //    var start = DateTime.Now;
        //    start = start.Subtract(start.TimeOfDay);
        //    var end = start.AddDays(7);


        //    var appts = (from a in _repo.Query<Appointment>()
        //                 where a.AppointmentDateTime >= start && a.AppointmentDateTime <= end
        //                 orderby a.AppointmentDateTime descending
        //                 select new {
        //                     AppointmentDateTime = a.AppointmentDateTime,
        //                     TechUser = a.TechUser,
        //                     CustomerUser = a.CustomerUser,
        //                     AppointmentType = a.AppointmentType
        //                 }).ToList();         


        //    foreach(var dataitem in appts) {
        //        Console.WriteLine("appts dataitem: " + dataitem);
        //    }

        //    return appts as IList<AppointmentDTO>;


        //select new {
        //    TechName = t.FirstName + " " + t.LastName,
        //    CustomerName = c.FirstName + " " + c.LastName,
        //    //TypeName = ty.TypeName,
        //    AppointmentDateTime = a.AppointmentDateTime



        //select new AppointmentDTO() {
        //select new {
        //    AppointmentDateTime = AppointmentDateTime,
        //    CustomerUser = new UserDTO() {
        //        FirstName = CustomerFirstName,
        //        LastName = CustomerLastName
        //    },
        //    TechUser = new UserDTO() {
        //        FirstName = TechFirstName,
        //        LastName = TechLastName,
        //        DayOff = DayOff,
        //        BeginLunchBreak = BeginLunchBreak,
        //        EndLunchBreak = EndLunchBreak,
        //        IsStylist = IsStylist,
        //        IsNailTech = IsNailTech,
        //        IsEstician = IsEstician
        //    },
        //    AppointmentType = new AppointmentTypeDTO() {
        //        TypeName = TypeName,
        //        TypeDuration = TypeDuration
        //    }
        //}).ToList();

        //foreach(var dataitem in techappts) {
        //    CustomerName = CustomerFirstName + " " + CustomerLastName;
        //    TechName = TechFirstName + " " + TechLastName;
        //    Console.WriteLine("dataitem: " + dataitem);
        //}







        //    var apptsByDayQuery = (from a in appts
        //                           group a by a.AppointmentDateTime.Subtract(a.AppointmentDateTime.TimeOfDay) into dayAppts
        //                           select new { dayAppts.Key, dayAppts });

        //    var apptsByDay = new Dictionary<DateTime, IList<AppointmentDTO>>();
        //    foreach(var it in apptsByDayQuery) {
        //        apptsByDay[it.Key] = it.dayAppts.ToList();
        //    }

        //    return apptsByDay;
        //}



        // find a specific appointment by id
        //public IList<AppointmentDTO> FindAppointment(int Id) {

        //    return (from a in _appointmentRepo.FindAppointment(Id)
        //            select new AppointmentDTO() {
        //                CustomerName = a.Customer.UserName,
        //                StylistName = a.StylistId,
        //                AppointmentDateTime = a.AppointmentDateTime,
        //                AppointmentType = new AppointmentTypeDTO() {
        //                    TypeDuration = a.AppointmentType.TypeDuration,
        //                    TypeName = a.AppointmentType.TypeName
        //                }
        //            }).ToList();
        //}

        // add a new appointment
        public void AddAppointment(AppointmentDTO dto) {
            var dbItem = new Appointment() {
                CustomerName = dto.CustomerName,
                TechId = dto.TechId,
                AppointmentDateTime = dto.AppointmentDateTime,
                AppointmentTypeName = dto.AppointmentTypeName
                };

            _repo.Add(dbItem);
            }

        //update an appointment
        //public void UpdateAppointment(AppointmentDTO dto) {
        //    var dbItem = _appointmentRepo.FindAppointment(dto.Id).FirstOrDefault();

        //    dbItem.Customer = new ApplicationUser();
        //    dbItem.StylistId = dto.StylistName;
        //    dbItem.AppointmentDateTime = dto.AppointmentDateTime;
        //    dbItem.AppointmentType = new AppointmentType();

        //    _appointmentRepo.SaveChanges();
        //}

        // delete an appointment
        //public void DeleteAppointment(AppointmentDTO dto) {
        //    var dbItem = _appointmentRepo.FindAppointment(dto.Id).FirstOrDefault();

        //    dbItem.Id = dto.Id;
        //    _appointmentRepo.DeleteAppointment(dbItem);
        //}


        }
}
