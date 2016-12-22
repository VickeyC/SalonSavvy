using Microsoft.AspNetCore.Authorization;
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
    public class AppointmentTypeController:Controller {
        private AppointmentTypeService _appointmentTypeService;
        public AppointmentTypeController(AppointmentTypeService ats) {
            _appointmentTypeService = ats;
        }


        // get a list of appointment types
        [HttpGet]
        public IList<AppointmentTypeDTO> Get() {

            return _appointmentTypeService.GetAllAppointmentTypes();

        }

        
        // add a new appointment type
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult PostAppointmentType([FromBody]AppointmentTypeDTO appointmentType) {
            if(!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }

            _appointmentTypeService.AddAppointmentType(appointmentType);

            return Ok();
        }

        // update the appointment type
        [HttpPost("{id}")]
        [Authorize(Policy = "AdminOnly")]
        
        public IActionResult UpdateAppointmentType([FromBody]AppointmentTypeDTO appointmentType) {
            if(!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }


            _appointmentTypeService.UpdateAppointmentType(appointmentType);

            return Ok();
        }


        // delete an appointment type
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteAppointmentType(int id, AppointmentTypeDTO appointmentType) {
            if(!ModelState.IsValid) {
                return BadRequest(this.ModelState);
            }


            _appointmentTypeService.DeleteAppointmentType(appointmentType);


            return Ok();

        }
    }
}
    