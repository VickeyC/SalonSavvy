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

    //export class ProductsController {
    //    public message = "Hello from the products page!";
    //}

    export class LocationController {

        public message = "Hello from the location page!";
    }


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

        }


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


        }


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

        }



        constructor(private $http: ng.IHttpService, private $uibModal: ng.ui.bootstrap.IModalService) {
            $http.get('/api/appointmentType')
                .then((results) => {
                    this.appointmentTypes = results.data;
                });
        }
    }


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
    }


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

    }




    export class AppointmentController {

        public schedule;
        public appointmentToUpdate;

        constructor($http: ng.IHttpService) {
            $http.get('/api/appointment/schedule').then((results) => {
                this.schedule = {}; // days -> (stylist -> (time -> appt))
                for (let rawDay in results.data) {
                    let day = moment(rawDay).format("dddd");
                    this.schedule[day] = {}; // stylist -> (time -> appt)
                    for (let appt of results.data[day]) { // of because the index in the array doesn't matter
                        this.schedule[day][appt.stylistName] = this.schedule[day][appt.stylistName] || { // either set the stylist's day to itself OR an empty object
                            "9:00 am": undefined,
                            "10:00 am": undefined,
                            "11:00 am": undefined,
                            "12:00 pm": undefined,
                            "1:00 pm": undefined,
                            "2:00 pm": undefined,
                            "3:00 pm": undefined,
                            "4:00 pm": undefined,
                            "5:00 pm": undefined
                        };
                        this.schedule[day][appt.stylistName][moment(appt.appointmentDateTime).format("h:mm a")] = appt;               }
                }
            });
            
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
