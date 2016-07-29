using Microsoft.AspNetCore.Mvc;
using SalonSavvy.Data;
using SalonSavvy.Infrastructure;
using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Services
{
    public class AppointmentTypeService
    {
        private AppointmentTypeRepository _appointmentTypeRepo;
        public AppointmentTypeService(AppointmentTypeRepository atr) {
            _appointmentTypeRepo = atr;
        }

        // get a list of all appointment types
        public IList<AppointmentTypeDTO> GetAllAppointmentTypes() {

            return (from t in _appointmentTypeRepo.GetAllAppointmentTypes()
                    select new AppointmentTypeDTO() {
                        Id = t.Id,
                        TypeName = t.TypeName,
                        TypeDuration = t.TypeDuration
                    }).ToList();
        }

        // find a specific appointment type by id
        public IList<AppointmentTypeDTO> FindAppointmentType( int id) {

            return (from t in _appointmentTypeRepo.FindAppointmentType(id)
                    select new AppointmentTypeDTO() { 
                        TypeName = t.TypeName,
                        TypeDuration = t.TypeDuration
                    }).ToList();
        }

        // add a new appointment type
        public void AddAppointmentType(AppointmentTypeDTO dto) {
            var dbItem = new AppointmentType() {
                TypeName = dto.TypeName,
                TypeDuration = dto.TypeDuration
            };

            _appointmentTypeRepo.AddAppointmentType(dbItem);
        }


        //update an appointment type
        public void UpdateAppointmentType(AppointmentTypeDTO dto) {
            var dbItem = _appointmentTypeRepo.FindAppointmentType(dto.Id).FirstOrDefault();

            dbItem.TypeName = dto.TypeName;
            dbItem.TypeDuration = dto.TypeDuration;

            _appointmentTypeRepo.SaveChanges();
        }

        // delete an appointment type
        public void DeleteAppointmentType(AppointmentTypeDTO dto) {
            var dbItem = _appointmentTypeRepo.FindAppointmentType(dto.Id).FirstOrDefault();

            dbItem.Id = dto.Id;
            _appointmentTypeRepo.DeleteAppointmentType(dbItem);
        }

    }
}
