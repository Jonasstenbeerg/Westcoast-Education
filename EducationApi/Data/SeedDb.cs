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
            return new List<Course>{
                new Course
                {
                    Category = new Category{CategoryName = ".NET"},
                    CourseNumber = 3452,
                    CourseTitle = "Utveckling med .NET",
                    LenghtInHouers = 140,
                    Description = "En mycket givande kurs som lär dig utveckling med .NET platformen",
                    CourseDetails = 
                    {
                       new CourseDetail{Module="C#"},
                       new CourseDetail{Module="Unity"},
                       new CourseDetail{Module="Xamarin"}     
                    },
                    Teacher = new Teacher
                    {
                        UserInfo =
                        {
                            FirstName = "Sanna",
                            LastName = "Svensson",
                            Email = "Sanna.Svensson@WC-Education.com",
                            PhoneNumber = "076-2348832", 
                            Address = new Address {
                                StreetName = "Vallavägen",
                                StreetNumber = 77,
                                Zipcode = 11345
                            }   
                        },
                        
                        TeacherCompetencies =
                        {
                            new TeacherCompetencies{Competence = new Competence{Name="Grundläggande programmering",Description="Grundläggande kunskap inom VB och C#"}},
                            new TeacherCompetencies{Competence = new Competence{Name="Avancerad databas hantering",Description="Avancerad använding dokument och relations databaser"}},
                            new TeacherCompetencies{Competence = new Competence{Name="spel programmering",Description="Grundläggande utbildning i hur en spelmotor fungerar"}},
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
                    CourseDetails = 
                    {
                       new CourseDetail{Module="Java"}
                    },
                    Teacher = new Teacher
                    {
                        UserInfo =
                        {
                            FirstName = "Stig",
                            LastName = "Svensson",
                            Email = "Stig.Svensson@WC-Education.com",
                            PhoneNumber = "077-2893332",
                            Address = new Address {
                                StreetName = "Vallavägen",
                                StreetNumber = 75,
                                Zipcode = 11345
                            }           
                        },
                        
                        TeacherCompetencies = 
                        {
                            new TeacherCompetencies{Competence = new Competence{Name="Java",Description="Java.....tråkigt"}}
                        }
                    }
                },
                new Course
                {
                    Category = new Category{CategoryName = ".NET"},
                    CourseNumber = 3450,
                    CourseTitle = ".NET web",
                    LenghtInHouers = 75,
                    Description = "I denna kurs går vi in på ASP .NET ",
                    CourseDetails = 
                    {
                       new CourseDetail{Module="MVC"},
                       new CourseDetail{Module="Blazor"},
                       new CourseDetail{Module="MVVM"}
                    },
                    Teacher = new Teacher
                    {
                        UserInfo = new UserInfo
                        {
                            FirstName = "Ulla",
                            LastName = "Helgesson",
                            Email = "Ulla.Helgesson@WC-Education.com",
                            PhoneNumber = "077-3333332",
                            Address = new Address {
                                StreetName = "Smålandsgatan",
                                StreetNumber = 3,
                                Zipcode = 31876
                            }                    
                        },
                        
                        TeacherCompetencies = 
                        {
                            new TeacherCompetencies{Competence = new Competence{Name="Webbutveckling",Description="Kompetens inom utveckling av hemsidor"}}
                        }
                    }
                }
                
            };
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
                    PhoneNumber = "076-7428461",
                    Address = new Address {
                                StreetName = "Vallrundan",
                                StreetNumber = 95,
                                Zipcode = 81833
                    }                 

                },
                },
                new Student{
                   UserInfo = new UserInfo
                {
                    
                    Email = "Linn.Olofsson@Mail.com",
                    FirstName = "Linn",
                    LastName = "Olofsson",
                    PhoneNumber = "070-6668731",
                    Address = new Address {
                        StreetName = "Von obens väg",
                        StreetNumber = 1,
                        Zipcode = 79133
                    }              

                },
                },
                new Student{
                   UserInfo = new UserInfo
                {
                    Email = "Aron.Henriksson@Mail.com",
                    FirstName = "Aron",
                    LastName = "Henriksson",
                    PhoneNumber = "073-5937130",
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
                    PhoneNumber = "070-9623465",
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