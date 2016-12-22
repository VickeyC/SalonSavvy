using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalonSavvy.Models;
using SalonSavvy.Data;

namespace SalonSavvy.Services
{
    public class AppointmentTypeService {
        private GenericRepository _repo;
        public AppointmentTypeService(GenericRepository repo) {
            _repo = repo;
        }

        // get a list of all appointment types
        public IList<AppointmentTypeDTO> GetAllAppointmentTypes() {

            return (from t in _repo.Query<AppointmentType>()
                    select new AppointmentTypeDTO() {
                        Id = t.Id,
                        TypeName = t.TypeName,
                        TypeDuration = t.TypeDuration,
                        TypeSkill = t.TypeSkill
                    }).ToList();
        }

        // find a specific appointment type by id
        public IList<AppointmentTypeDTO> FindAppointmentType(int id) {

            return (from t in _repo.Query<AppointmentType>()
                    where t.Id == id
                    select new AppointmentTypeDTO() {
                        TypeName = t.TypeName,
                        TypeDuration = t.TypeDuration,
                        TypeSkill = t.TypeSkill

                    }).ToList();
        }

        // add a new appointment type
        public void AddAppointmentType(AppointmentTypeDTO dto) {
            var dbItem = new AppointmentType() {
                TypeName = dto.TypeName,
                TypeDuration = dto.TypeDuration,
                TypeSkill = dto.TypeSkill
            };

            _repo.Add(dbItem);
        }


        //update an appointment type
        public void UpdateAppointmentType(AppointmentTypeDTO dto) {

            // get the appointment type to update
            var dbItem = _repo.Query<AppointmentType>()
                .FirstOrDefault(t => t.Id == dto.Id);

            // perform the update
            if(dbItem != null) {
                dbItem.TypeName = dto.TypeName;
                dbItem.TypeDuration = dto.TypeDuration;
                dbItem.TypeSkill = dto.TypeSkill;

                _repo.Update<AppointmentType>(dbItem);
            }


        }

        // delete an appointment type
        public void DeleteAppointmentType(AppointmentTypeDTO dto) {
            // get the appointment type to delete
            var dbItem = _repo.Query<AppointmentType>()
                .FirstOrDefault(t => t.Id == dto.Id);

            // perform the delete
            if(dbItem != null) {
                _repo.Delete<AppointmentType>(dbItem);
            }
        }

    }
}
