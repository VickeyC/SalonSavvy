using SalonSavvy.Data;
using SalonSavvy.Infrastructure;
using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Services
{
    public class _userService {
        private GenericRepository _repo;
        public _userService(GenericRepository repo) {

            _repo = repo;
        }

        public IList<UserDTO> GetAllTechUsers() {

            var technicians = (from u in _repo.Query<ApplicationUser>()
                               orderby u.FirstName
                               where u.IsTechnician
                               select new {
                                   TechId = u.Id,
                                   TechName = (u.FirstName + " " + u.LastName),
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   DayOff = u.DayOff,
                                   BeginLunchBreak = u.BeginLunchBreak,
                                   EndLunchBreak = u.EndLunchBreak,
                                   TechBio = u.TechBio,
                                   TechImage = u.TechImage,
                                   IsStylist = u.IsStylist,
                                   IsNailTech = u.IsNailTech,
                                   IsEstician = u.IsEstician
                               }).ToList();

            foreach(var dataitem in technicians) {
                Console.WriteLine("technicians dataitem: " + dataitem);
            }

            return technicians as IList<UserDTO>;
        }

    }    
        
}

