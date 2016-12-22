using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalonSavvy.Models {
    public class Appointment {
        public int Id { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser CustomerUser { get; set; }

        public string TechId { get; set; }
        [ForeignKey("TechId")]
        public ApplicationUser TechUser { get; set; }

        public int AppointmentTypeId { get; set; }
        [ForeignKey("AppointmentTypeId")]
        public AppointmentType TechAppointments { get; set; }
        
        public DateTime AppointmentDateTime { get; set; }
    }
}
