using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonSavvy.Data;
using SalonSavvy.Services;
using SalonSavvy.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SalonSavvy.Controllers 
{
    [Route("api/[controller]")]
    public class AppointmentController:Controller {

        private AppointmentService _appointmentService;
        private _userService _userService;
        private AppointmentTypeService _appointmentTypeService;
        public AppointmentController(AppointmentService a, _userService u, AppointmentTypeService at) {
            _appointmentService = a;
            _userService = u;
            _appointmentTypeService = at;
        }

        
        [HttpGet("techAppointments")]

        // /api/appointment/techAppointments
        public IEnumerable<TechAppointment> Get() {

            return _appointmentService.GetUpcomingAppointments();

        }

        [HttpGet]

        //  /api/appointmentType
        public IList<AppointmentTypeDTO> GetAllAppointmentTypes() {

            return _appointmentTypeService.GetAllAppointmentTypes();

            }


        //[HttpGet ("users")]
        //public IList<UserDTO> GetAllTechUsers() {

        //    return _userService.GetAllTechUsers();
        //}

        // GET /api/appointment/schedule
        //[HttpGet("schedule")]
        //public IDictionary<DateTime, IList<AppointmentDTO>> GetWeekSchedule() {
        //    return _appointmentService.GetUpcommingAppointments();
        //}

        //[HttpGet("{id}")]
        //public AppointmentDTO Get(int id) {

        //    return _appointmentService.FindAppointment();
        //}

        //// get the list of appointment types
        //[HttpGet]
        //public IList<AppointmentTypeDTO> Get() {
        //    return _appointmentTypeService.GetAllAppointmentTypes();
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
        //[HttpPost("{id}")]
        //public IActionResult UpdateAppointment([FromBody]AppointmentDTO appointment) {
        //    if(!ModelState.IsValid) {
        //        return BadRequest(this.ModelState);
        //    }


        //    _appointmentService.UpdateAppointment(appointment);

        //    return Ok();
        //}


        // delete an appointment
        //[HttpDelete("{id}")]
        //public IActionResult DeleteAppointment(int id, AppointmentDTO appointment) {
        //    if(!ModelState.IsValid) {
        //        return BadRequest(this.ModelState);
        //    }


        //    _appointmentService.DeleteAppointment(appointment);


        //    return Ok();

        //}
        }
}
