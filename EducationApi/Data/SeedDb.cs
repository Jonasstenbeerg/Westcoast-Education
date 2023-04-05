using EducationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Data
{
    public static class SeedDb
    {
       
        public static async Task PopulateDb(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EducationContext>();

                if (!await context.Courses.AnyAsync())
                {
                    await RecreateDb(context);
                    await context.Courses.AddRangeAsync(GetCoursesAndTeachers());
                    await context.Students.AddRangeAsync(GetStudents());
                    await context.SaveChangesAsync();
                }
            }
        }
        private static List<Course> GetCoursesAndTeachers()
        {
            var category = new Category{CategoryName = ".NET"};
            return new List<Course>{
                new Course
                {
                    Category = category,
                    CourseNumber = 3452,
                    CourseTitle = "Utveckling med .NET",
                    LenghtInHouers = 140,
                    Description = "En mycket givande kurs som lär dig utveckling med .NET platformen",
                    CourseDetails = "I Denna kurs kollar vi närmare på C# och Unity",
                    
                    Teacher = new Teacher
                    {
                        UserInfo =
                        {
                            FirstName = "Sanna",
                            LastName = "Svensson",
                            Email = "Sanna.Svensson@WC-Education.com",
                            PhoneNumber = "0762348832", 
                            Address = new Address {
                                StreetName = "Vallavägen",
                                StreetNumber = 77,
                                Zipcode = 11345
                            }   
                        },
                        
                        TeacherCompetencies =
                        {
                            new TeacherCompetencies{Competence = new Competence{Name="Grundläggande programmering"}},
                            new TeacherCompetencies{Competence = new Competence{Name="Avancerad databas hantering"}},
                            new TeacherCompetencies{Competence = new Competence{Name="spel programmering"}}
                        }

                    }

                },
                new Course
                {
                    Category = new Category{CategoryName = "Java"},
                    CourseNumber = 3561,
                    CourseTitle = "Utveckling med Java",
                    LenghtInHouers = 75,
                    Description = "En mycket givande kurs som lär dig utveckling med Java",
                    CourseDetails = "Kursen fokuserar endast på utveckling med java i jGRASP",
                    
                    Teacher = new Teacher
                    {
                        UserInfo =
                        {
                            FirstName = "Stig",
                            LastName = "Svensson",
                            Email = "Stig.Svensson@WC-Education.com",
                            PhoneNumber = "0772893332",
                            Address = new Address {
                                StreetName = "Vallavägen",
                                StreetNumber = 75,
                                Zipcode = 11345
                            }           
                        },
                        
                        TeacherCompetencies = 
                        {
                            new TeacherCompetencies{Competence = new Competence{Name="Java"}}
                        }
                    }
                },
                new Course
                {
                    Category = category,
                    CourseNumber = 3450,
                    CourseTitle = ".NET web",
                    LenghtInHouers = 75,
                    Description = "En av dom mest omtyckta kurserna på skolan",
                    CourseDetails = "I Denna kurs kollar vi närmare på Razor pages och MVC med ASP .NET",
                    
                    Teacher = new Teacher
                    {
                        UserInfo = new UserInfo
                        {
                            FirstName = "Ulla",
                            LastName = "Helgesson",
                            Email = "Ulla.Helgesson@WC-Education.com",
                            PhoneNumber = "0773333332",
                            Address = new Address {
                                StreetName = "Smålandsgatan",
                                StreetNumber = 3,
                                Zipcode = 31876
                            }                    
                        },
                        
                        TeacherCompetencies = 
                        {
                            new TeacherCompetencies{Competence = new Competence{Name="Webbutveckling"}
                        }
                    }
                }
                
            }};
        }
        private static List<Student> GetStudents()

        {
            return new List<Student>
            {
                new Student{
                   UserInfo = new UserInfo
                {
                    Email = "Emil.Hansson@Mail.com",
                    FirstName = "Emil",
                    LastName = "Hansson",
                    PhoneNumber = "0767428461",
                    Address = new Address {
                                StreetName = "Vallrundan",
                                StreetNumber = 95,
                                Zipcode = 55833
                    }                 

                },
                },
                new Student{
                   UserInfo = new UserInfo
                {
                    
                    Email = "Linn.Olofsson@Mail.com",
                    FirstName = "Linn",
                    LastName = "Olofsson",
                    PhoneNumber = "0706668731",
                    Address = new Address {
                        StreetName = "Von obens väg",
                        StreetNumber = 1,
                        Zipcode = 33133
                    }              

                },
                },
                new Student{
                   UserInfo = new UserInfo
                {
                    Email = "Aron.Henriksson@Mail.com",
                    FirstName = "Aron",
                    LastName = "Henriksson",
                    PhoneNumber = "0735937130",
                    Address = new Address {
                                StreetName = "Ekvägen",
                                StreetNumber = 43,
                                Zipcode = 64365
                            }              

                },
                },
                new Student{
                   UserInfo = new UserInfo
                {
                    Email = "Berit.Oskarsson@Mail.com",
                    FirstName = "Berit",
                    LastName = "Oskarsson",
                    PhoneNumber = "0709623465",
                    Address = new Address {
                        StreetName = "Kungsgatan",
                        StreetNumber = 3,
                        Zipcode = 65743
                    }              

                },
                }

            };
        }
        private static async Task RecreateDb(EducationContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }   
}