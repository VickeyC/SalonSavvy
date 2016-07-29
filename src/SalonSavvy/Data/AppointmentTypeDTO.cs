using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Data
{
    public class AppointmentTypeDTO
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string TypeDuration { get; set; }
    }
}
