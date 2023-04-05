using AdministratorClientMVC.ViewModels;
using AdministratorClientMVC.ViewModels.Course;
using AdministratorClientMVC.ViewModels.Student;

namespace AdministratorClientMVC.Models
{
    public class StudentServiceModel
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _config;
         private readonly CourseServiceModel _courseServiceModel;
        public StudentServiceModel(IConfiguration config,CourseServiceModel courseServiceModel)
        {
            _config = config;

            _baseUrl = $"{_config.GetValue<string>("baseUrl")}/students";

            _courseServiceModel = courseServiceModel;
            
        }

        public async Task<StudentIndexViewModel> LoadIndexPageAsync()
        {
            var students = await ListAllStudentsAsync();

            var courses = await _courseServiceModel.ListAllCoursesAsync();

            var indexPage = new StudentIndexViewModel{
                Courses = courses,
                Students = students
            };

            return indexPage;
        }

        public async Task<StudentIndexViewModel> UpdateIndexPageAsync(string input)
        {
            var courses = await _courseServiceModel.ListAllCoursesAsync();

            var students = new List<StudentViewModelLessInfo>();

            if (input is "alla")
            {
                students = await ListAllStudentsAsync();
            }
            else
            {
                students = await GetStudentsByCourseNumberAsync(input);
            }
            

            var indexPage = new StudentIndexViewModel{
                Courses = courses,
                Students = students!
            };

            return indexPage;
        }

        public async Task<List<StudentViewModelLessInfo>> ListAllStudentsAsync()
        {
            var url = $"{_baseUrl}/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när studenterna skulle hämtas");
            }
            
            var students = await respons.Content.ReadFromJsonAsync<List<StudentViewModelLessInfo>>();

            return students ?? new List<StudentViewModelLessInfo>();
        }

        public async Task<bool> NochangesMadeAsync(int id, CreateStudentViewModel model)
        {
            var teacher = await GetStudentToUpdateByIdAsync(id);

            if (teacher == model)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<List<AsignCourseViewModel>> PointAtStudentCoursesAsync(int id)
        {
            var url = $"{_config.GetValue<string>("baseUrl")}/courses/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kurserna skulle hämtas");
            }
            
            var courses = await respons.Content.ReadFromJsonAsync<List<CourseViewModelLessInfo>>();

            var listToReturn = new List<AsignCourseViewModel>();

            foreach (var course in courses!)
            {
                
                var asignCourseViewModel = new AsignCourseViewModel{
                   CourseNumber = course.CourseNumber,
                   CourseTitle = course.CourseTitle
                   
                };

                if (await StudentIsInCourseAsync(id,course.CourseNumber))
                {
                   asignCourseViewModel.HasCourse = true;
                }
                else
                {
                    asignCourseViewModel.HasCourse = false;
                }
                
                listToReturn.Add(asignCourseViewModel);
            }

            return listToReturn;
        }
        public async Task EditCoursesForStudentAsync(int id,List<AsignCourseViewModel> model)
        {
            var url = _baseUrl;
            using var client = new HttpClient();

            foreach (var course in model)
            {
                if (course.HasCourse&& !await StudentIsInCourseAsync(id,course.CourseNumber))
                {
                    var addCourse = new CourseSearchViewModel
                    {
                      CourseNumber = course.CourseNumber
                    };
                    var respons = await client.PostAsJsonAsync(url+$"/addcourse/{id}",addCourse);
                    if (!respons.IsSuccessStatusCode)
                    {
                        throw new Exception("Något gick fel när kursen skulle läggas till för studenten");
                    }
                }
                else if(!course.HasCourse&&await StudentIsInCourseAsync(id,course.CourseNumber))
                {
                   var removeCourse = new CourseSearchViewModel
                    {
                      CourseNumber = course.CourseNumber
                    };
                    var respons = await client.PostAsJsonAsync(url+$"/removecourse/{id}",removeCourse);  
                    if (!respons.IsSuccessStatusCode)
                    {
                        throw new Exception("Något gick fel när kursen skulle tas bort från studenten");
                    }   
                }
            }
        }

        

        public async Task AddStudentAsync(CreateStudentViewModel model)
        {
             var url = $"{_baseUrl}";
            
            using var client = new HttpClient();

            var respons = await client.PostAsJsonAsync(url,model);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när studenten skulle skapas");
            }
        }

        public async Task<StudentViewModel> GetStudentByIdAsync(int id)
        {
            var url = $"{_baseUrl}/byid/{id}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när studenten skulle hämtas");
            }
            
            var course = await respons.Content.ReadFromJsonAsync<StudentViewModel>();

            return course ?? new StudentViewModel();
        }

        public async Task<CreateStudentViewModel> GetStudentToUpdateByIdAsync(int id)
        {
            var student = await GetStudentByIdAsync(id);

             var studentToSend = new CreateStudentViewModel {
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                StreetName = student.StreetName,
                StreetNumber = student.StreetNumber.ToString(),
                Zipcode = student.Zipcode.ToString()
            };

            return studentToSend;
        }

        public async Task UpdateStudentAsync(int id,CreateStudentViewModel model)
        {
            var url = $"{_baseUrl}/{id}";
            
            using var client = new HttpClient();

            var respons = await client.PutAsJsonAsync(url,model);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när studenten skulle uppdateras");
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            var url = $"{_baseUrl}/{id}";
            
            using var client = new HttpClient();

            var respons = await client.DeleteAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när studenten skulle raderas");
            }
            
        }

        private async Task<List<StudentViewModelLessInfo>> GetStudentsByCourseNumberAsync(string input)
        {
            var url = $"{_baseUrl}/bycoursenumber/{input}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);


            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när studenterna skulle hämtas");
            }

            var students = await respons.Content.ReadFromJsonAsync<List<StudentViewModelLessInfo>>();

            return students ?? new List<StudentViewModelLessInfo>();
        }

        private async Task<bool> StudentIsInCourseAsync(int studentId, int courseId)
        {
            var url = $"{_config.GetValue<string>("baseUrl")}/courses/bystudentid/{studentId}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                return false;
            }

            var courses = await respons.Content.ReadFromJsonAsync<List<CourseViewModelLessInfo>>();
            

            foreach (var course in courses!)
            {
                if (course.CourseNumber==courseId)
                {
                    return true;
                }
            }

            return false;
        }

    }
}