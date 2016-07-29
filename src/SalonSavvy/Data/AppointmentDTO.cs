using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Data {
    public class AppointmentDTO {
        public int Id { get; set; }
        
        public string CustomerName { get; set; }

        public string StylistName { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        
        public AppointmentTypeDTO AppointmentType { get; set; }
    }
}