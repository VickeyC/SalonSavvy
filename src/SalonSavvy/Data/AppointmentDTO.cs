using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Data {
    public class AppointmentDTO {
        public int Id { get; set; }
        
        public string CustomerName { get; set; }

        public string TechId { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        
        public string AppointmentTypeName  { get; set; }
        }
}