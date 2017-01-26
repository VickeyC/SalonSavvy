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
        public timeSlot;
        public scheduleDay;
        public techAppts;
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
        
        public selectedAppt;
        public toastMsg;


        //open the Add modal for Appointment------------------------------------------------------------------------>>>
        //-- Open the modal and pre-populate with selection criteria for a new appointment --------------------------
        public openAddModal(techName, timeSlot, scheduleDay, isStylist, isNailTech, isEstician, techId) {
            var addApptInfo;
            let modalInstance = this.$uibModal.open({
                templateUrl: 'addModalAppointment.html',
                bindToController: true,
                controller: AddAppointmentController,
                controllerAs: 'modal',
                resolve: {

                    // extract the selection info and return it to the modal
                    addApptInfo: function () {

                        addApptInfo = {
                            techName: techName,
                            timeSlot: timeSlot,
                            scheduleDay: scheduleDay,
                            isStylist: isStylist,
                            isNailTech: isNailTech,
                            isEstician: isEstician,
                            techId: techId
                        };

                        return addApptInfo;
                            
                    },

                } // end resolve for openAddModal
            })  //end $uibModal.open

        }  // end openAddModal


        


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



        // Constructor for Appointment  (list of technicians and openings)  -------------------------------------------------------------------------------
        constructor(private $http: ng.IHttpService, private $uibModal: ng.ui.bootstrap.IModalService, private $stateParams, private $state: ng.ui.IStateService, private $mdToast: ng.material.IToastService, private accountService: SalonSavvy.Services.AccountService) {
            var count = 0;
            var dayCount = 0;
            var scheduleDates = [];

            if (!accountService.isLoggedIn()) {
                this.$state.go('home');
                this.toastMsg = "Please log in before scheduling an appointment";
                this.displayToast(this.$mdToast);
            }

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
                            this.isStylist = techAppts[i].isStylist;
                            this.isNailTech = techAppts[i].isNailTech;
                            this.isEstician = techAppts[i].isEstician;
                            this.techId = techAppts[i].techId;
                            this.nineAm = buildTimeSlot(scheduleDate, techAppts, i, "9:00 am");
                            this.tenAm = buildTimeSlot(scheduleDate, techAppts, i, "10:00 am");
                            this.elevenAm = buildTimeSlot(scheduleDate, techAppts, i, "11:00 am");
                            this.noon = buildTimeSlot(scheduleDate, techAppts, i, "12:00 pm");
                            this.onePm = buildTimeSlot(scheduleDate, techAppts, i, "1:00 pm");
                            this.twoPm = buildTimeSlot(scheduleDate, techAppts, i, "2:00 pm");
                            this.threePm = buildTimeSlot(scheduleDate, techAppts, i, "3:00 pm");
                            this.fourPm = buildTimeSlot(scheduleDate, techAppts, i, "4:00 pm");
                            this.fivePm = buildTimeSlot(scheduleDate, techAppts, i, "5:00 pm");
                            scheduleArray.push({ scheduleDate: this.scheduleDate, techName: this.techName, isStylist: this.isStylist, isNailTech: this.isNailTech, isEstician: this.isEstician, techId: this.techId, nineAm: this.nineAm, tenAm: this.tenAm, elevenAm: this.elevenAm, noon: this.noon, onePm: this.onePm, twoPm: this.twoPm, threePm: this.threePm, fourPm: this.fourPm, fivePm: this.fivePm });
                            ++i;
                        };
                        return scheduleArray;
                    }

                    function buildTimeSlot(scheduleDate, techAppts, i, timeSlot) {
                        let sched = "open";
                        let timeSlotMatch = timeSlot.slice(0, 5);
                        let compareDate = scheduleDate.concat(" ", timeSlot);

                        if (techAppts[i].dayOff == moment(scheduleDate).format("dddd")) {
                            sched = "Unavailable"
                        } else {
                            if (techAppts[i].beginLunchBreak == timeSlotMatch) {
                                sched = "Lunch"
                            } else {
                                let j = 0;
                                while (j < techAppts[i].appointments.length) {
                                    console.log(`compareDate: ${compareDate}`);
                                    let apptDateTime = moment(techAppts[i].appointments[j].appointmentDateTime).format("YYYY-MM-DD h:mm a")
                                    console.log(`found an appointment for ${techAppts[i].techName} for ${apptDateTime}`);
                                    if (apptDateTime == compareDate) {
                                        console.log(`**found a match!`);
                                        sched = "Booked"
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

        public displayToast($mdToast) {
            var toast = $mdToast.simple()
                .textContent(this.toastMsg)
                .position('bottom left')
                .hideDelay(5000);

            $mdToast.show(toast);
        };

    }  //end AppointmentController



    // Add appointment Controller ---------------------------------------------------------------------------------->
    // Create the customer's selected appointment record
    export class AddAppointmentController {
        public allApptTypes;
        public validApptTypes = [];
        public selectedType;
        private toastMsg;

        public addAppointment(addApptInfo, selectedType) {
            let apptDate = moment(addApptInfo.scheduleDay, "dddd MMM Do").format("MM/DD/YYYY");
            let apptTime = moment(addApptInfo.timeSlot, "hh:mm a").format("hh:mm a");
            let apptDateTime = (apptDate + " " + apptTime);
            console.log(`Adding appointment for: ${apptDateTime}`);
            let appointmentToAdd = {
                techId: addApptInfo.techId,
                customerName: this.accountService.getUserName(),
                appointmentDateTime: apptDateTime,
                appointmentTypeName: selectedType.typeName
            };
            this.$http.post(`/api/appointment`, appointmentToAdd)
                .then((results) => {
                    this.$uibModalInstance.close(appointmentToAdd);
                    this.$state.go('appointment');
                    this.toastMsg = "Your appointment has been successfully added";
                    this.displayToast(this.$mdToast);
                })
                .catch((reason) => {
                })
        };

        public displayToast($mdToast) {
            var toast = $mdToast.simple()
                .textContent(this.toastMsg)
                .position('bottom left')
                .hideDelay(5000);

            $mdToast.show(toast);
        };

        // constructor to populate the modal
        constructor(private $http: ng.IHttpService, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, private $stateParams, private $state: ng.ui.IStateService, private $mdToast: ng.material.IToastService, private accountService: SalonSavvy.Services.AccountService, public addApptInfo) {

            $http.get('/api/appointmentType')
                .then((results) => {
                    this.allApptTypes = results.data;
                    let i = 0;
                    let j = 0;
                    while (j < this.allApptTypes.length) {
                        if (this.allApptTypes[j].typeSkill == "Stylist" && this.addApptInfo.isStylist) {
                            this.validApptTypes[i] = this.allApptTypes[j];
                            i++;
                        }; 
                        if (this.allApptTypes[j].typeSkill == "Nail Tech" && this.addApptInfo.isNailTech) {
                            this.validApptTypes[i] = this.allApptTypes[j];
                            i++;
                        };
                        if (this.allApptTypes[j].typeSkill == "Estician" && this.addApptInfo.isEstician) {
                            this.validApptTypes[i] = this.allApptTypes[j];
                            i++;
                        };
                        j++;
                    }
                    return this.validApptTypes;
                    })
                .catch((reason) => {
                });
        }  //end constructor
        
    }; // end class AddAppointmentController


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }
};
