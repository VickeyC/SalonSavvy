using Microsoft.AspNetCore.Mvc;
using SalonSavvy.Data;
using SalonSavvy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalonSavvy.Controllers 
{
    [Route("api/[controller]")]
    public class AppointmentController:Controller {

        private AppointmentService _appointmentService;
        public AppointmentController(AppointmentService a) {
            _appointmentService = a;
        }


        // get a list of appointments
        //[HttpGet]
        //public IList<AppointmentDTO> Get() {

        //    return _appointmentService.GetAllAppointments();

        //}

        //{
        //    "2016-07-28T00:00:00": [
        //        {}, // appt dto
        //        {}, // appt dto
        //        {}, // appt dto
        //        {}  // appt dto
        //    ]
        //}


        // GET /api/appointment/schedule
        [HttpGet("schedule")]
        //[HttpGet]
        public IDictionary<DateTime, IList<AppointmentDTO>> GetWeekSchedule() {
            return _appointmentService.GetUpcommingAppointments();
        }

        //[HttpGet("{id}")]
        //public AppointmentDTO Get(int id) {

        //    return _appointmentService.FindAppointment();
        //}



        // add a new appointment
        [HttpPost]
        public IActionResult PostAppointment([FromBody]AppointmentDTO appointment) {
            if(!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }

            _appointmentService.AddAppointment(appointment);

            return Ok();
        }

        // update the appointment
        [HttpPost("{id}")]
        public IActionResult UpdateAppointment([FromBody]AppointmentDTO appointment) {
            if(!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }


            _appointmentService.UpdateAppointment(appointment);

            return Ok();
        }


        // delete an appointment
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id, AppointmentDTO appointment) {
            if(!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }


            _appointmentService.DeleteAppointment(appointment);


            return Ok();

        }
    }
}
