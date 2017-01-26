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


            // Ensure Nancy (IsAdmin)
            var nancy = await userManager.FindByNameAsync("nancyhall427@gmail.com");
            if(nancy == null) {
                // create user
                nancy = new ApplicationUser {
                    UserName = "nancyhall427@gmail.com",
                    Email = "nancyhall427@gmail.com",
                    BeginLunchBreak = "12:00",
                    EndLunchBreak = "1:00",
                    DayOff = "Wednesday",
                    FirstName = "Nancy",
                    LastName = "Hall",
                    IsTechnician = true,
                    IsStylist = true,
                    IsNailTech = false,
                    IsEstician = false,
                    TechBio = "Having 26 years of experience as a professional hair stylist, I bring a passion for the industry and emphasis on family values that make Salon Savvy a success. As a certified master Schwarzkopf colorist, I think it's important to keep current by attending hair shows and seminars. My precision scissor cutting with freestyle texture techniques lets you decide the amount of maintenance for your lifestyle. It's all about having fun, so I am looking forward to see you at Salon Savvy!"
                };
                await userManager.CreateAsync(nancy, "Secret123!");

                // add claims to designate Nancy as an admin and as a stylist
                await userManager.AddClaimAsync(nancy, new Claim("IsAdmin", "true"));
                await userManager.AddClaimAsync(nancy, new Claim("IsTechnician", "true"));
            }

            
            // Ensure Karissia (IsStylist and not IsAdmin)
            var karissia = await userManager.FindByNameAsync("karissiahall97@gmail.com");
            if(karissia == null) {
                // create user
                karissia = new ApplicationUser {
                    UserName = "karissiahall97@gmail.com",
                    Email = "karissiahall97@gmail.com",
                    BeginLunchBreak = "12:00",
                    EndLunchBreak = "1:00",
                    DayOff = "Thursday",
                    FirstName = "Karissia",
                    LastName = "Hall",
                    IsTechnician = true,
                    IsStylist = true,
                    IsNailTech = false,
                    IsEstician = false,
                    TechBio = "I have been a cosmetologist for over 5 years. The health and appearance of your hair is my passion! I love creative cutting and working with color in a way that compliments your individual style. I am confident and proficient with all textures of hair from fine to curly and in-between. Come visit me at Salon Savvy and give me the chance to make you happy with your hair again!"
                };
                await userManager.CreateAsync(karissia, "Secret123!");
                // designate as a stylist
                await userManager.AddClaimAsync(karissia, new Claim("IsTechnician", "true"));
            }


            // Ensure Ashley (IsStylist and not IsAdmin)
            var ashley = await userManager.FindByNameAsync("ashleycox016@gmail.com");
            if(ashley == null) {
                // create user
                ashley = new ApplicationUser {
                    UserName = "ashleycox016@gmail.com",
                    Email = "ashleycox016@gmail.com",
                    BeginLunchBreak = "12:00",
                    EndLunchBreak = "1:00",
                    DayOff = "Monday",
                    FirstName = "Ashley",
                    LastName = "Cox",
                    IsTechnician = true,
                    IsStylist = true,
                    IsNailTech = true,
                    IsEstician = false,
                    TechBio = "I love everything about being a stylist, it has been my passion since early teens! I ensure every client leaves happy, feeling beautiful, and confident! I have been a stylist for 7 years and stay up to date on new trends in fashion and beauty. Specialize in fashion colors, razor cuts, rockabilly looks, and skilled in every aspect of the beauty industry. I'm looking forward to getting her hands in your hair at Salon Savvy!"
                };
                await userManager.CreateAsync(ashley, "Secret123!");
                // designate as a stylist
                await userManager.AddClaimAsync(ashley, new Claim("IsTechnician", "true"));

            }


            // Ensure Alison (IsStylist and not IsAdmin)
            var alison = await userManager.FindByNameAsync("alisonhess48@gmail.com");
            if(alison == null) {
                // create user
                alison = new ApplicationUser {
                    UserName = "alisonhess48@gmail.com",
                    Email = "alisonhess48@gmail.com",
                    BeginLunchBreak = "12:00",
                    EndLunchBreak = "1:00",
                    DayOff = "Tuesday",
                    FirstName = "Alison",
                    LastName = "Hess",
                    IsTechnician = true,
                    IsStylist = true,
                    IsNailTech = true,
                    IsEstician = false,
                    TechBio = "Cuts, color, new styles, manicures, pedicures, come see me! I am excited to offer my clients, new and my faithful following, a family friendly, professional and progressive environment. I love every aspect of my job. I am patient and a great listener. I would love to find your perfect look, from the latest trends to timeless looks. I am here to create your Savvy masterpiece!"
                };
                await userManager.CreateAsync(alison, "Secret123!");
                // designate as a stylist
                await userManager.AddClaimAsync(alison, new Claim("IsTechnician", "true"));

            }

            // Add a couple of Appointment Types
            if(!context.AppointmentTypes.Any()) {

                context.AppointmentTypes.AddRange(
                    new AppointmentType { TypeName = "Haircut", TypeDuration = "30", TypeSkill = "Stylist" },
                    new AppointmentType { TypeName = "Highlight", TypeDuration = "60", TypeSkill = "Stylist" },
                    new AppointmentType { TypeName = "Facial", TypeDuration = "60", TypeSkill = "Estician" },
                    new AppointmentType { TypeName = "Pedicure", TypeDuration = "60", TypeSkill = "NailTech" }
                );

                context.SaveChanges();
            }

            //Add a few default appointments
            //if(!context.Appointments.Any()) {

            //    var Monday1 = new DateTime(2016, 09, 19, 09, 00, 00);
            //    var Monday2 = new DateTime(2016, 09, 19, 14, 00, 00);
            //    var Monday3 = new DateTime(2016, 09, 19, 09, 00, 00);
            //    var Tuesday1 = new DateTime(2016, 09, 20, 11, 00, 00);
            //    var Tuesday2 = new DateTime(2016, 09, 20, 09, 00, 00);
            //    var Tuesday3 = new DateTime(2016, 09, 20, 16, 00, 00);
            //    var Thursday1 = new DateTime(2016, 09, 15, 09, 00, 00);
            //    var Thursday2 = new DateTime(2016, 09, 15, 15, 00, 00);
            //    var Thursday3 = new DateTime(2016, 09, 15, 10, 00, 00);
            //    var Friday1 = new DateTime(2016, 09, 16, 16, 00, 00);
            //    var Friday2 = new DateTime(2016, 09, 16, 11, 00, 00);
            //    var Friday3 = new DateTime(2016, 09, 16, 13, 00, 00);
            //    var Saturday1 = new DateTime(2016, 09, 17, 17, 00, 00);
            //    var Saturday2 = new DateTime(2016, 09, 17, 10, 00, 00);
            //    var Saturday3 = new DateTime(2016, 09, 17, 09, 00, 00);
            //    var NancyId = "3f43f1f9-741f-46fd-bd65-f0e72d21b970";
            //    var AshleyId = "2d62d5b5-5a33-4d19-8170-4cd11e19d3f9";
            //    var AlisonId = "46c91e41-4bc7-4ec7-b87b-12f893e93bca";
            //    var VickeyId = "cab28b15-2c6f-489a-bbe2-0ebf0aeade27";

                
            //    context.Appointments.AddRange(
            //        new Appointment { CustomerId = "crouchvickey@gmail.com", TechId = "NancyHall@SalonSavvyShawnee.com", AppointmentDateTime = Monday1, AppointmentTypeId = 1 }
                    //new Appointment { CustomerId = VickeyId, TechId = NancyId, AppointmentDateTime = Monday1, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = NancyId, AppointmentDateTime = Tuesday1, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = NancyId, AppointmentDateTime = Thursday1, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = NancyId, AppointmentDateTime = Monday1, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = NancyId, AppointmentDateTime = Friday1, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = NancyId, AppointmentDateTime = Saturday1, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = AshleyId, AppointmentDateTime = Monday2, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = AshleyId, AppointmentDateTime = Tuesday2, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = AshleyId, AppointmentDateTime = Thursday2, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = AshleyId, AppointmentDateTime = Monday2, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = AshleyId, AppointmentDateTime = Friday2, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = AshleyId, AppointmentDateTime = Saturday2, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = AlisonId, AppointmentDateTime = Monday3, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = AlisonId, AppointmentDateTime = Tuesday3, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = AlisonId, AppointmentDateTime = Thursday3, AppointmentTypeId = 1 },
                    //new Appointment { CustomerId = VickeyId, TechId = AlisonId, AppointmentDateTime = Friday3, AppointmentTypeId = 2 },
                    //new Appointment { CustomerId = VickeyId, TechId = AlisonId, AppointmentDateTime = Saturday3, AppointmentTypeId = 1 }
                    //);

               
                    //context.SaveChanges();
                              
                
            //}
        }
    }
}
