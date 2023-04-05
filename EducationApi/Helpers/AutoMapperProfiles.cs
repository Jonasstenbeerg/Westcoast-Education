using AutoMapper;
using EducationApi.Models;
using EducationApi.ViewModels;
using EducationApi.ViewModels.Competence;
using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Student;
using EducationApi.ViewModels.Teacher;

namespace EducationApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Från ---> Till
            CreateMap<PostCourseViewModel,Course>()
            .ForPath(dest => dest.Category.CategoryName, options => options.MapFrom(src=>src.CategoryName));
            CreateMap<Course,CourseViewModel>()
            .ForMember(c => c.TeacherFullName, options => options.MapFrom(src => (src.Teacher==null? "Denna kurs har ingen lärare": string.Concat(src.Teacher.UserInfo.FirstName+" "+src.Teacher.UserInfo.LastName))))
            .ForMember(c => c.CategoryName, options => options.MapFrom(src => src.Category.CategoryName));
            CreateMap<Course,CourseViewModelLessInfo>();
            CreateMap<Student,StudentViewModel>()
            .ForMember(dest => dest.StudentId, options => options.MapFrom(src=>src.Id))
            .ForMember(dest => dest.FirstName, options => options.MapFrom(src=>src.UserInfo.FirstName))
            .ForMember(dest => dest.LastName, options => options.MapFrom(src=>src.UserInfo.LastName))
            .ForMember(dest => dest.Email, options => options.MapFrom(src=>src.UserInfo.Email))
            .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src=>src.UserInfo.PhoneNumber))
            .ForMember(dest => dest.StreetName, options => options.MapFrom(src=>src.UserInfo.Address.StreetName))
            .ForMember(dest => dest.StreetNumber, options => options.MapFrom(src=>src.UserInfo.Address.StreetNumber))
            .ForMember(dest => dest.Zipcode, options => options.MapFrom(src=>src.UserInfo.Address.Zipcode));
            CreateMap<Student, StudentViewModelLessInfo>()
            .ForMember(dest => dest.StudentId, options => options.MapFrom(src=>src.Id))
            .ForMember(dest=>dest.FirstName,options => options.MapFrom(src=>src.UserInfo.FirstName))
            .ForMember(dest=>dest.LastName,options => options.MapFrom(src=>src.UserInfo.LastName))
            .ForMember(dest=>dest.Email,options => options.MapFrom(src=>src.UserInfo.Email))
            .ForMember(dest=>dest.PhoneNumber,options => options.MapFrom(src=>src.UserInfo.PhoneNumber));
            CreateMap<PostStudentViewModel, Student>()
            .ForPath(dest => dest.UserInfo.FirstName, options => options.MapFrom(src=>src.FirstName))
            .ForPath(dest => dest.UserInfo.LastName, options => options.MapFrom(src=>src.LastName))
            .ForPath(dest => dest.UserInfo.Email, options => options.MapFrom(src=>src.Email))
            .ForPath(dest => dest.UserInfo.PhoneNumber, options => options.MapFrom(src=>src.PhoneNumber))
            .ForPath(dest => dest.UserInfo.Address.StreetName, options => options.MapFrom(src=>src.StreetName))
            .ForPath(dest => dest.UserInfo.Address.StreetNumber, options => options.MapFrom(src=>src.StreetNumber))
            .ForPath(dest => dest.UserInfo.Address.Zipcode, options => options.MapFrom(src=>src.Zipcode));
            CreateMap<Teacher,TeacherViewModel>()
            .ForMember(dest => dest.TeacherId, options => options.MapFrom(src=> src.Id))
            .ForMember(dest => dest.FirstName, options => options.MapFrom(src=>src.UserInfo.FirstName))
            .ForMember(dest => dest.LastName, options => options.MapFrom(src=>src.UserInfo.LastName))
            .ForMember(dest => dest.Email, options => options.MapFrom(src=>src.UserInfo.Email))
            .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src=>src.UserInfo.PhoneNumber))
            .ForMember(dest => dest.StreetName, options => options.MapFrom(src=>src.UserInfo.Address.StreetName))
            .ForMember(dest => dest.StreetNumber, options => options.MapFrom(src=>src.UserInfo.Address.StreetNumber))
            .ForMember(dest => dest.Zipcode, options => options.MapFrom(src=>src.UserInfo.Address.Zipcode));
            CreateMap<Teacher,TeacherViewModelLessInfo>()
            .ForMember(dest => dest.TeacherId, options => options.MapFrom(src=> src.Id))
            .ForMember(dest=>dest.FirstName,options => options.MapFrom(src=>src.UserInfo.FirstName))
            .ForMember(dest=>dest.LastName,options => options.MapFrom(src=>src.UserInfo.LastName))
            .ForMember(dest=>dest.Email,options => options.MapFrom(src=>src.UserInfo.Email))
            .ForMember(dest=>dest.PhoneNumber,options => options.MapFrom(src=>src.UserInfo.PhoneNumber));
            CreateMap<PostTeacherViewModel, Teacher>()
            .ForPath(dest => dest.UserInfo.FirstName, options => options.MapFrom(src=>src.FirstName))
            .ForPath(dest => dest.UserInfo.LastName, options => options.MapFrom(src=>src.LastName))
            .ForPath(dest => dest.UserInfo.Email, options => options.MapFrom(src=>src.Email))
            .ForPath(dest => dest.UserInfo.PhoneNumber, options => options.MapFrom(src=>src.PhoneNumber))
            .ForPath(dest => dest.UserInfo.Address.StreetName, options => options.MapFrom(src=>src.StreetName))
            .ForPath(dest => dest.UserInfo.Address.StreetNumber, options => options.MapFrom(src=>src.StreetNumber))
            .ForPath(dest => dest.UserInfo.Address.Zipcode, options => options.MapFrom(src=>src.Zipcode));
            CreateMap<PostCompetenceViewModel, Competence>()
            .ForMember(dest => dest.Name, options => options.MapFrom(src=>src.CompetenceName));
            CreateMap<Competence,CompetenceViewModel>()
            .ForMember(dest => dest.CompetenceName, options => options.MapFrom(src=>src.Name));      
            CreateMap<Category,CategoryViewModel>();
           
            
            
            
        }
    }
}