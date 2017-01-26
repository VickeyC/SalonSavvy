using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.ViewModels.Home
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string AppointmentTypeName { get; set; }
        public string CustomerName { get; set; }
    }
}
