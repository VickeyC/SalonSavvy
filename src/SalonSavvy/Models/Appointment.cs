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

        public ApplicationUser Customer { get; set; }

        public string StylistName { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public int AppointmentTypeId { get; set; }
        [ForeignKey("AppointmentTypeId")]
        public AppointmentType AppointmentType { get; set; }
    }
}
