using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Data
{
    public class UserDTO
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
    }

}
