using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalonSavvy.ViewModels;

namespace SalonSavvy.ViewModels.Home
{
    public class TechAppointment
    {
        public string TechId { get; set; }
        public string TechName { get; set; }
        public string DayOff { get; set; }
        public string BeginLunchBreak { get; set; }
        public string EndLunchBreak { get; set; }
        public bool IsStylist { get; set; }
        public bool IsNailTech { get; set; }
        public bool IsEstician { get; set; }
        public ICollection<AppointmentViewModel> Appointments { get; set; }
        
        
    }
}
