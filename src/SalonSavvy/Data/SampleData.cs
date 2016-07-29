using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using SalonSavvy.Models;
using Microsoft.AspNetCore.Mvc;
using SalonSavvy.Infrastructure;
using SalonSavvy.Services;


namespace SalonSavvy.Data {
    public class SampleData {
        public async static Task Initialize(IServiceProvider serviceProvider) {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var appointmentRepo = serviceProvider.GetService<AppointmentRepository>();

            // Ensure db
            context.Database.EnsureCreated();


            //// Ensure Nancy (IsAdmin)
            //var nancy = await userManager.FindByNameAsync("Nancy.Hall@SalonSavvyShawnee.com");
            //if(nancy == null) {
            //    // create user
            //    nancy = new ApplicationUser {
            //        UserName = "NancyHall@SalonSavvyShawnee.com",
            //        Email = "NancyHall@SalonSavvyShawnee.com"
            //    };
            //    await userManager.CreateAsync(nancy, "Secret123!");

            //    // add claims to designate Nancy as an admin and as a stylist
            //    await userManager.AddClaimAsync(nancy, new Claim("IsAdmin", "true"));
            //    await userManager.AddClaimAsync(nancy, new Claim("IsStylist", "true"));
            //}

            //// Ensure Megan (IsStylist and not IsAdmin)
            //var megan = await userManager.FindByNameAsync("MeganVanRensburg@SalonSavvyShawnee.com");
            //if(megan == null) {
            //    // create user
            //    megan = new ApplicationUser {
            //        UserName = "MeganVanRensburg@SalonSavvyShawnee.com",
            //        Email = "MeganVanRensburg@SalonSavvyShawnee.com"
            //    };
            //    await userManager.CreateAsync(megan, "Secret123!");
            //    // designate as a stylist
            //    await userManager.AddClaimAsync(megan, new Claim("IsStylist", "true"));
            //}


            //// Ensure Trisha (IsStylist and not IsAdmin)
            //var trisha = await userManager.FindByNameAsync("TrishaStephens@SalonSavvyShawnee.com");
            //if(trisha == null) {
            //    // create user
            //    trisha = new ApplicationUser {
            //        UserName = "TrishaStephens@SalonSavvyShawnee.com",
            //        Email = "TrishaStephens@SalonSavvyShawnee.com"
            //    };
            //    await userManager.CreateAsync(trisha, "Secret123!");
            //    // designate as a stylist
            //    await userManager.AddClaimAsync(trisha, new Claim("IsStylist", "true"));
            //}


            //// Ensure Karissia (IsStylist and not IsAdmin)
            //var karissia = await userManager.FindByNameAsync("KarissiaHall@SalonSavvyShawnee.com");
            //if(karissia == null) {
            //    // create user
            //    karissia = new ApplicationUser {
            //        UserName = "KarissiaHall@SalonSavvyShawnee.com",
            //        Email = "KarissiaHall@SalonSavvyShawnee.com"
            //    };
            //    await userManager.CreateAsync(karissia, "Secret123!");
            //    // designate as a stylist
            //    await userManager.AddClaimAsync(karissia, new Claim("IsStylist", "true"));
            //}


            //// Ensure Ashley (IsStylist and not IsAdmin)
            //var ashley = await userManager.FindByNameAsync("AshleyCox@SalonSavvyShawnee.com");
            //if(ashley == null) {
            //    // create user
            //    ashley = new ApplicationUser {
            //        UserName = "AshleyCox@SalonSavvyShawnee.com",
            //        Email = "AshleyCox@SalonSavvyShawnee.com"
            //    };
            //    await userManager.CreateAsync(ashley, "Secret123!");
            //    // designate as a stylist
            //    await userManager.AddClaimAsync(ashley, new Claim("IsStylist", "true"));

            //}


            //// Ensure Alison (IsStylist and not IsAdmin)
            //var alison = await userManager.FindByNameAsync("AlisonHess@SalonSavvyShawnee.com");
            //if(alison == null) {
            //    // create user
            //    alison = new ApplicationUser {
            //        UserName = "AlisonHess@SalonSavvyShawnee.com",
            //        Email = "AlisonHess@SalonSavvyShawnee.com"
            //    };
            //    await userManager.CreateAsync(alison, "Secret123!");
            //    // designate as a stylist
            //    await userManager.AddClaimAsync(alison, new Claim("IsStylist", "true"));

            //}

            //// Add a couple of Appointment Types
            if(!context.AppointmentTypes.Any()) {

                context.AppointmentTypes.AddRange(
                    new AppointmentType { TypeName = "Haircut", TypeDuration = "30" },
                    new AppointmentType { TypeName = "Highlight", TypeDuration = "120" }

                );

                context.SaveChanges();
            }

            //Add a few default appointments
            if(!context.Appointments.Any()) {

                var Thursday = new DateTime(2016, 07, 28, 09, 00, 00);
                var Friday = new DateTime(2016, 07, 29, 11, 00, 00);
                var Saturday = new DateTime(2016, 07, 30, 13, 00, 00);
                var Monday = new DateTime(2016, 07, 31, 09, 00, 00);
                var Tuesday = new DateTime(2016, 08, 01, 16, 00, 00);
                var Wednesday = new DateTime(2016, 08, 02, 17, 00, 00);



                context.Appointments.AddRange(
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Nancy Hall", AppointmentDateTime = Monday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Nancy Hall", AppointmentDateTime = Tuesday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Nancy Hall", AppointmentDateTime = Wednesday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Nancy Hall", AppointmentDateTime = Thursday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Nancy Hall", AppointmentDateTime = Friday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Nancy Hall", AppointmentDateTime = Saturday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Alison Hess", AppointmentDateTime = Monday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Alison Hess", AppointmentDateTime = Tuesday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Alison Hess", AppointmentDateTime = Wednesday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Alison Hess", AppointmentDateTime = Thursday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Alison Hess", AppointmentDateTime = Friday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Alison Hess", AppointmentDateTime = Saturday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Ashley Cox", AppointmentDateTime = Monday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Ashley Cox", AppointmentDateTime = Tuesday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Ashley Cox", AppointmentDateTime = Wednesday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Ashley Cox", AppointmentDateTime = Thursday, AppointmentTypeId = 1 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Ashley Cox", AppointmentDateTime = Friday, AppointmentTypeId = 2 },
                    new Appointment { CustomerId = "754b1969-0875-44d9-96c5-11fb367701c6", StylistName = "Ashley Cox", AppointmentDateTime = Saturday, AppointmentTypeId = 1 }
                    );


                context.SaveChanges();
            }
        }
   }
}
