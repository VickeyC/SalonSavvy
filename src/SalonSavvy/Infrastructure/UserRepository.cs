using SalonSavvy.Data;
using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Infrastructure
{
    public class UserRepository
    {
        private ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) {
            _db = db;
        }

        //get the stylist info
        public IQueryable<ApplicationUser> GetAllTechUsers() 
            {
            return from u in _db.Users
                   select u;

        }
    }
}
