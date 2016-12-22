using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonSavvy.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string DayOff { get; set; }
        public string BeginLunchBreak { get; set; }
        public string EndLunchBreak { get; set; }
        public string TechBio { get; set; }
        public string TechImage { get; set; }
        public bool IsTechnician { get; set; }
        public bool IsStylist { get; set; }
        public bool IsNailTech { get; set; }
        public bool IsEstician { get; set; }
        [InverseProperty("TechUser")]
        public ICollection<Appointment> TechAppointments { get; set; }
        [InverseProperty("CustomerUser")]
        public ICollection<Appointment> CustomerAppointments { get; set; }
    }
}
