using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.ViewModels.Home
{
    public class AppointmentTypeViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeDuration { get; set; }
        public string TypeSkill { get; set; }
    }
}
