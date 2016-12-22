namespace SalonSavvy.Controllers {

    export class HomeController {
        public salon;

    }

    export class AboutController {
        public message = "Hello from the about page!";
    }

    export class OurTeamController {
        public message = "Hello from our team!";
    }

    export class LocationController {

        public message = "Hello from the location page!";
    }

    // appointment Type Controller ----------------------------------------------------------------------------------
    export class AppointmentTypeController {
        public appointmentTypes;
        public appointmentToUpdate;

        //open the Add modal 
        public openAddModal() {
            let modalInstance = this.$uibModal.open({
                templateUrl: 'addModalAppointmentType.html'
            });

            modalInstance.result
                .then((apptType) => {
                    this.$http.post('/api/appointmentType', apptType)
                        .then((response) => {
                        })
                        .catch((reason) => {
                        })
                })
                .catch((reason) => {
                });

        }  //end openAddModal


        //open the update modal 
        public openUpdateModal(appointmentToUpdate) {

            let modalInstance = this.$uibModal.open({
                templateUrl: 'updateModalAppointmentType.html',
                controller: UpdateAppointmentTypeController,
                controllerAs: 'modal',
                resolve: {
                    appointmentToUpdate: function () {
                        return appointmentToUpdate;
                    }
                }
            });


            modalInstance.result.then((result) => {
                appointmentToUpdate.typeName = result.typeName;
                appointmentToUpdate.typeDuration = result.typeDuration;
            });


        }  // end openUpdateModal


        // open the delete modal 
        public openDeleteModal(appointmentToDelete) {

            let modalInstance = this.$uibModal.open({
                templateUrl: 'deleteModalAppointmentType.html',
                controller: DeleteAppointmentTypeController,
                controllerAs: 'modal',
                resolve: {
                    apptToDelete: function () {
                        return appointmentToDelete;
                    }
                }
            });


            modalInstance.result.then((result) => {
                appointmentToDelete.typeName = result.typeName;
                appointmentToDelete.typeDuration = result.typeDuration;
            });

        } // end openDeleteModal

        constructor(private $http: ng.IHttpService, private $uibModal: ng.ui.bootstrap.IModalService) {
            $http.get('/api/appointmentType')
                .then((results) => {
                    this.appointmentTypes = results.data;
                    return this.appointmentTypes;
                });
        } //end constructor for AppointmentTypeController
    }  // end AppointmentTypeController

    // Update appointment type Controller ----------------------------------------------------------------------
    export class UpdateAppointmentTypeController {

        public updateAppointmentType() {
            this.$http.post(`/api/appointmentType/${this.appointmentToUpdate.id}`, this.appointmentToUpdate)
                .then((results) => {
                    this.$uibModalInstance.close(this.appointmentToUpdate);
                });
        }

        constructor(private $http: ng.IHttpService, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, public appointmentToUpdate) {
            this.appointmentToUpdate = JSON.parse(JSON.stringify(appointmentToUpdate));
        }
    }   //end UpdateAppointmentTypeController

    // Delete Appointment Type Controller ------------------------------------------------------------------------
    export class DeleteAppointmentTypeController {


        public deleteAppointmentType() {

            this.$http.delete(`/api/appointmentType/${this.apptToDelete.id}`, this.apptToDelete)
                .then((results) => {
                    this.$uibModalInstance.close(this.apptToDelete);
                });
        }


        constructor(private $http: ng.IHttpService, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, public apptToDelete) {
            this.apptToDelete = JSON.parse(JSON.stringify(apptToDelete));
        }

    } //end DeleteAppointmentTypeController

    //-------------------------------------------------------------------------------------------------------------->>>
    // Appointment Controller starts here -------------------------------------------------------------------------->>>
    //-------------------------------------------------------------------------------------------------------------->>>
    export class AppointmentController {

        public schedule;
        public appointmentSelection;
        public appointmentToAdd;
        public appointmentToUpdate;
        public appointmentTypes;
        public appointmentInfo;
        public techAppts;
        public scheduleArray;
        public scheduleDay1;
        public scheduleDay2;
        public scheduleDay3;
        public scheduleDay4;
        public scheduleDay5;
        public scheduleDay6;
        public schedule1;
        public schedule2;
        public schedule3;
        public schedule4;
        public schedule5;
        public schedule6;
                
        //open the Add modal for Appointment------------------------------------------------------------------------>>>
        //-- Open the modal and pre-populate with selection criteria for a new appointment --------------------------
        public openAddModal(appointmentSelection) {
            let modalInstance = this.$uibModal.open({
                templateUrl: 'addModalAppointment.html',
                controller: AddAppointmentController,
                controllerAs: 'modal',
                resolve: {

                    // extract the stylist name and return it to the modal
                    stylist: function () {
                        var str = JSON.stringify(appointmentSelection);
                        var str2 = str.match(/stylistName":"[a-zA-Z]+\s[a-zA-Z]+"/g);
                        var str3 = JSON.stringify(str2);
                        console.log("str3 is: " + str3);
                        var str4 = str3.match(/[A-Za-z]+\s[A-Za-z]+/g)
                        var stylist = JSON.stringify(str4);
                        console.log("stylist is: " + stylist);
                        return stylist;
                    }, //end stylist function

                    // extract the available slots and return them to the modal
                    availableSlots: function () {

                        var str = JSON.stringify(appointmentSelection);
                        console.log(str);

                        // build the available time slots for the selected stylist and day
                        var availableSlots = ["9:00 am", "10:00 am", "11:00 am", "12:00 pm", "1:00 pm", "2:00 pm", "3:00 pm", "4:00 pm", "5:00 pm"];
                        var fullSlots = new Array();
                        fullSlots = str.match(/9:00 am|10:00 am|11:00 am|12:00 pm|1:00 pm|2:00 pm|3:00 pm|4:00 pm|5:00 pm/g);

                        for (var i = 0; i < fullSlots.length; ++i) {
                            for (var k = 0; k < availableSlots.length; k++) {
                                if (availableSlots[k] == fullSlots[i]) {
                                    availableSlots.splice(k, 1);
                                }
                            }  // end second for
                        }  //end first for
                        console.log("availableSlots is: " + availableSlots);

                        return availableSlots;
                    },  //end availableSlots function

                    // get a list of appointment types for the pulldown
                    getAllAppointmentTypes: function () {
                        var appointmentTypes = appointmentTypes;
                        return appointmentTypes;
                    }


                    } // end resolve for openAddModal
                })  //end $uibModal.open


        }  // end openAddModal
        

            //modalInstance.result
            //    .then((appointmentToAdd) => {
            //        this.$http.post('/api/appointment', appointmentToAdd)
            //            .then((response) => {
            //            })
            //            .catch((reason) => {
            //            })
            //    })
            //    .catch((reason) => {
            //    });

        //}


        //open the update modal for Appointment ---------------------------------------------------------------
        //public openUpdateModal(appointmentToUpdate) {

        //    let modalInstance = this.$uibModal.open({
        //        templateUrl: 'updateModalAppointment.html',
        //        controller: UpdateAppointmentController,
        //        controllerAs: 'modal',
        //        resolve: {
        //            appointmentToUpdate: function () {
        //                return appointmentToUpdate;
        //            }
        //        }
        //    });



        //    modalInstance.result.then((result) => {
        //        appointmentToUpdate.typeName = result.typeName;
        //        appointmentToUpdate.typeDuration = result.typeDuration;
        //    });


        //}


        // open the delete modal for appointment ----------------------------------------------------------------
        //public openDeleteModal(appointmentToDelete) {

        //    let modalInstance = this.$uibModal.open({
        //        templateUrl: 'deleteModalAppointment.html',
        //        controller: DeleteAppointmentController,
        //        controllerAs: 'modal',
        //        resolve: {
        //            apptToDelete: function () {
        //                return appointmentToDelete;
        //            }
        //        }
        //    });


        //    modalInstance.result.then((result) => {
        //        appointmentToDelete.typeName = result.typeName;
        //        appointmentToDelete.typeDuration = result.typeDuration;
        //    });

        //}

        

        // Constructor for Appointment -------------------------------------------------------------------------------
        constructor(private $http: ng.IHttpService, private $uibModal: ng.ui.bootstrap.IModalService) {

            var count = 0;
            var dayCount = 0;
            var scheduleDates = [];
            
            while (count < 6) {
                var dayOfWeek = moment().add(dayCount, 'days').format("ddd");
                if (dayOfWeek == "Sun") {
                    dayCount++;
                }
                var scheduleDate = moment().add(dayCount, 'days').format("YYYY-MM-DD");
                scheduleDates.push(scheduleDate);
                dayCount++;
                count++;
            }

            this.scheduleDay1 = moment(scheduleDates[0]).format("dddd MMM Do");
            this.scheduleDay2 = moment(scheduleDates[1]).format("dddd MMM Do");
            this.scheduleDay3 = moment(scheduleDates[2]).format("dddd MMM Do");
            this.scheduleDay4 = moment(scheduleDates[3]).format("dddd MMM Do");
            this.scheduleDay5 = moment(scheduleDates[4]).format("dddd MMM Do");
            this.scheduleDay6 = moment(scheduleDates[5]).format("dddd MMM Do");

            // get the technician info along with their existing appointments
            this.$http.get('/api/appointment/techAppointments')
                .then((results) => {
                    this.techAppts = results.data;

                    // build the display table for each schedule object
                    function buildSchedule(scheduleDate, techAppts) {   //define a constructor
                        let i = 0;
                        // let j = 0;
                        let scheduleArray = [];
                        while (i < techAppts.length) {
                            this.techName = techAppts[i].techName;
                            this.nineAm = buildTimeSlot(scheduleDate, techAppts, i, "9:00 am");
                            this.tenAm = buildTimeSlot(scheduleDate, techAppts, i, "10:00 am");
                            this.elevenAm = buildTimeSlot(scheduleDate, techAppts, i, "11:00 am");
                            this.noon = buildTimeSlot(scheduleDate, techAppts, i, "12:00 pm");
                            this.onePm = buildTimeSlot(scheduleDate, techAppts, i, "1:00 pm");
                            this.twoPm = buildTimeSlot(scheduleDate, techAppts, i, "2:00 pm");
                            this.threePm = buildTimeSlot(scheduleDate, techAppts, i, "3:00 pm");
                            this.fourPm = buildTimeSlot(scheduleDate, techAppts, i, "4:00 pm");
                            this.fivePm = buildTimeSlot(scheduleDate, techAppts, i, "5:00 pm");
                            scheduleArray.push({ scheduleDate: this.scheduleDate, techName: this.techName, nineAm: this.nineAm, tenAm: this.tenAm, elevenAm: this.elevenAm, noon: this.noon, onePm: this.onePm, twoPm: this.twoPm, threePm: this.threePm, fourPm: this.fourPm, fivePm: this.fivePm });
                            ++i;
                        };
                        return scheduleArray;
                    }

                    function buildTimeSlot(scheduleDate, techAppts, i, timeSlot) {
                        let sched = "open";
                        if (techAppts[i].dayOff == moment(scheduleDate).format("dddd")) {
                            sched = "unavailable"
                        } else {
                            if (techAppts[i].beginLunchBreak == timeSlot) {
                                sched = "lunch"
                            } else {
                                let j = 0;
                                let compareDate = (scheduleDate + " " + timeSlot);
                                while (j < techAppts[i].appointments.length) {
                                    let apptDateTime = moment(techAppts[i].appointments[j].appointmentDateTime).format("YYYY-MM-DD hh:mm a")
                                    if (apptDateTime == compareDate) {
                                        sched = "booked"
                                    }
                                    j++;
                                }
                            }
                        }
                        return sched;
                     }; // end buildTimeSlot

                        this.schedule1 = buildSchedule(scheduleDates[0], this.techAppts);

                        this.schedule2 = buildSchedule(scheduleDates[1], this.techAppts);

                        this.schedule3 = buildSchedule(scheduleDates[2], this.techAppts);

                        this.schedule4 = buildSchedule(scheduleDates[3], this.techAppts);

                        this.schedule5 = buildSchedule(scheduleDates[4], this.techAppts);

                        this.schedule6 = buildSchedule(scheduleDates[5], this.techAppts);

                    }); // end the .then in the http call
            }   //end constructor for AppointmentController
        
        }  //end AppointmentController

    

    // Add appointment Controller ---------------------------------------------------------------------------------->
    export class AddAppointmentController {

        //public addAppointment() {
        //    this.$http.post(`/api/appointment/${this.appointmentToAdd.id}`, this.appointmentToAdd)
        //        .then((results) => {
        //            this.$uibModalInstance.close(this.appointmentToAdd);
        //        });
        //}



        // Note that the fields used to pre-populate the modal are included in this constructor
        constructor(private $http: ng.IHttpService, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, public stylist, public availableSlots, public appointmentTypes) {
        }
    }

    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }




}
