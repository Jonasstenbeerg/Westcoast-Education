using AutoMapper;
using EducationApi.Models;
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
            CreateMap<Course,CourseViewModel>()
            .ForMember(c => c.TeacherFullName, options => options.MapFrom(src => string.Concat(src.Teacher.UserInfo.FirstName," ",src.Teacher.UserInfo.LastName)))
            .ForMember(c => c.CategoryName, options => options.MapFrom(src => src.Category.CategoryName));
            CreateMap<Course,CourseViewModelLessInfo>();
            CreateMap<Student,StudentViewModel>();
            CreateMap<PostStudentViewModel, Student>()
            .ForMember(dest => dest.UserInfo.FirstName, options => options.MapFrom(src=>src.FirstName))
            .ForMember(dest => dest.UserInfo.LastName, options => options.MapFrom(src=>src.LastName))
            .ForMember(dest => dest.UserInfo.Email, options => options.MapFrom(src=>src.Email))
            .ForMember(dest => dest.UserInfo.PhoneNumber, options => options.MapFrom(src=>src.PhoneNumber))
            .ForMember(dest => dest.UserInfo.Address, options => options.MapFrom(src=>src.Adress));
            CreateMap<PostTeacherViewModel, Teacher>()
            .ForMember(dest => dest.UserInfo.FirstName, options => options.MapFrom(src=>src.FirstName))
            .ForMember(dest => dest.UserInfo.LastName, options => options.MapFrom(src=>src.LastName))
            .ForMember(dest => dest.UserInfo.Email, options => options.MapFrom(src=>src.Email))
            .ForMember(dest => dest.UserInfo.PhoneNumber, options => options.MapFrom(src=>src.PhoneNumber))
            .ForMember(dest => dest.UserInfo.Address, options => options.MapFrom(src=>src.Adress));
            
            
            
        }
    }
}