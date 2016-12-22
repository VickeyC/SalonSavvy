using SalonSavvy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Data {
    public class AppointmentDTO {
        public int Id { get; set; }
        
        public UserDTO CustomerUser { get; set; }

        public UserDTO TechUser { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        
        public AppointmentTypeDTO AppointmentType { get; set; }
    }
}