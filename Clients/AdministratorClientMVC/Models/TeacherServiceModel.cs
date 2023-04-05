using AdministratorClientMVC.ViewModels;
using AdministratorClientMVC.ViewModels.Course;
using AdministratorClientMVC.ViewModels.Teacher;

namespace AdministratorClientMVC.Models
{
    public class TeacherServiceModel
    {
         private readonly string _baseUrl;
        private readonly IConfiguration _config;
        public TeacherServiceModel(IConfiguration config)
        {
            _config = config;

            _baseUrl = $"{_config.GetValue<string>("baseUrl")}/teachers";
            
        }

        public async Task<TeacherIndexViewModel> LoadIndexPageAsync()
        {
            var competencies = await GetCompetenciesAsync();

            var teachers = await ListAllTeachersAsync();

            var indexPage = new TeacherIndexViewModel{
                Teachers = teachers,
                Competencies = competencies
            };

            return indexPage;
        }

        public async Task<TeacherIndexViewModel> UpdateIndexPageAsync(string input)
        {
            var competencies = await GetCompetenciesAsync();

            var teachers = new List<TeacherViewModelLessInfo>();

            if (input is "alla")
            {
                teachers = await ListAllTeachersAsync();
            }
            else
            {
                teachers = await GetTeacherByCompetenceAsync(input);
            }
            

            var indexPage = new TeacherIndexViewModel{
                Teachers = teachers,
                Competencies = competencies
            };

            return indexPage;
        }

        public async Task<List<TeacherViewModelLessInfo>> ListAllTeachersAsync()
        {
            var url = $"{_baseUrl}/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när Lärarna skulle hämtas");
            }
            
            var teachers = await respons.Content.ReadFromJsonAsync<List<TeacherViewModelLessInfo>>();

            return teachers ?? new List<TeacherViewModelLessInfo>();
        }

        public async Task AddTeacherCompetenceAsync(int id,CreateCompetenceViewModel model)
        {
            var url = $"{_baseUrl}/addcomp/{id}";
            
            using var client = new HttpClient();

            var respons = await client.PostAsJsonAsync(url,model);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kompetensen skulle registreras för en lärare");
            }
            
        }

        public async Task<bool> NochangesMadeAsync(int id, CreateTeacherViewModel model)
        {
            var teacher = await GetTeacherToUpdateByIdAsync(id);

            if (teacher == model)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<TeacherInCourseViewModel>> PointAtTheacherInListAsync(int id)
        {
           
            var teacherToPointAt = await GetTeacherByCourseIdAsync(id);
            
            var teachers = await ListAllTeachersAsync();
            var listToReturn = new List<TeacherInCourseViewModel>();

            foreach (var teacher in teachers!)
            {
                
                var TeacherInCourseModel = new TeacherInCourseViewModel{
                    TeacherId = teacher.TeacherId,
                    TeacherFullName = string.Concat(teacher.FirstName+" "+teacher.LastName)
                   
                };

                if (teacher.TeacherId==teacherToPointAt.TeacherId)
                {
                    TeacherInCourseModel.IsInCourse = true;
                } 
                else
                {
                    TeacherInCourseModel.IsInCourse = false;
                }
                
                listToReturn.Add(TeacherInCourseModel);
            }

            return listToReturn;
        }

        

        public async Task<List<AsignCompetenceViewModel>> PointAtTeacherCompAsync(int id)
        {
            var url = $"{_config.GetValue<string>("baseUrl")}/competencies/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kompetenserna skulle hämtas");
            }
            
            var competencies = await respons.Content.ReadFromJsonAsync<List<CompetenceViewModel>>();

            var listToReturn = new List<AsignCompetenceViewModel>();

            foreach (var competence in competencies!)
            {
                
                var AsignCompetenceModel = new AsignCompetenceViewModel{
                   CompetenceName = competence.CompetenceName
                   
                   
                };

                if (await TeacherHasCompetenceAsync(id,competence.CompetenceName!))
                {
                   AsignCompetenceModel.HasCompetence = true;
                }
                else
                {
                    AsignCompetenceModel.HasCompetence = false;
                }
                
                listToReturn.Add(AsignCompetenceModel);
            }

            return listToReturn;
        }

        public async Task EditCompetenciesForTeacherAsync(int id,List<AsignCompetenceViewModel> model)
        {
            var url = _baseUrl;
            using var client = new HttpClient();

            foreach (var comp in model)
            {
                if (comp.HasCompetence&& !await TeacherHasCompetenceAsync(id,comp.CompetenceName!))
                {
                    var addcomp = new CreateCompetenceViewModel
                    {
                      CompetenceName = comp.CompetenceName
                      
                    };
                    var respons = await client.PostAsJsonAsync(url+$"/addcomp/{id}",addcomp);
                    if (!respons.IsSuccessStatusCode)
                    {
                        throw new Exception("Det gick fel när kompetensen skulle registreras för en lärare");
                    }
                }
                else if(!comp.HasCompetence&&await TeacherHasCompetenceAsync(id,comp.CompetenceName!))
                {
                   var addcomp = new CreateCompetenceViewModel
                    {
                      CompetenceName = comp.CompetenceName
                      
                    };
                    var respons = await client.PostAsJsonAsync(url+$"/removecomp/{id}",addcomp);  
                    if (!respons.IsSuccessStatusCode)
                    {
                        throw new Exception("Det gick fel när kompetensen skulle tas bort från läraren");
                    }   
                }
            }
        }

        public async Task SetCourseTeacherAsync(int teacherId,int courseId)
        {
            var url = $"{_baseUrl}/addcourse/{teacherId}";
            
            var courseSearchModel = new CourseSearchViewModel();

            courseSearchModel.CourseNumber=courseId;

            using var client = new HttpClient();
            
            var respons = await client.PostAsJsonAsync(url,courseSearchModel);
            
            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kursläraren skulle utses");
            }  
        }

        public async Task<List<TeacherViewModelLessInfo>> GetTeacherByCompetenceAsync(string input)
        {
             var url = $"{_baseUrl}/bycompetence/{input}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när lärarna skulle hämtas");
            }
            
            var teachers = await respons.Content.ReadFromJsonAsync<List<TeacherViewModelLessInfo>>();

            return teachers ?? new List<TeacherViewModelLessInfo>();
        }

        public async Task AddTeacherAsync(CreateTeacherViewModel model)
        {
             var url = $"{_baseUrl}";
            
            using var client = new HttpClient();

            var respons = await client.PostAsJsonAsync(url,model);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när läraren skulle skapas");
            }
        }

        public async Task<TeacherViewModel> GetTeacherByIdAsync(int id)
        {
            var url = $"{_baseUrl}/byid/{id}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när läraren skulle hämtas");
            }
            
            var teacher = await respons.Content.ReadFromJsonAsync<TeacherViewModel>();

            return teacher ?? new TeacherViewModel();
        }

        public async Task<CreateTeacherViewModel> GetTeacherToUpdateByIdAsync(int id)
        {
            var teacher = await GetTeacherByIdAsync(id);

            var teacherToSend = new CreateTeacherViewModel{
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                StreetName = teacher.StreetName,
                StreetNumber = teacher.StreetNumber.ToString(),
                Zipcode = teacher.Zipcode.ToString()
                
            };

            return teacherToSend;
        }
        public async Task UpdateTeacherAsync(int id,CreateTeacherViewModel model)
        {

            var url = $"{_baseUrl}/{id}";
            
            using var client = new HttpClient();

            var respons = await client.PutAsJsonAsync(url,model);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när läraren skulle uppdateras");
            }
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var url = $"{_baseUrl}/{id}";
            
            using var client = new HttpClient();

            var respons = await client.DeleteAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när läraren skulle raderas");
            }
            
        }

        private async Task<TeacherViewModelLessInfo> GetTeacherByCourseIdAsync(int id)
        {
            var url = $"{_baseUrl}/bycourseid/{id}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                return new TeacherViewModelLessInfo();
            }
           
           return await respons.Content.ReadFromJsonAsync<TeacherViewModelLessInfo>() ?? new TeacherViewModelLessInfo();
        }

        private async Task<bool> TeacherHasCompetenceAsync(int id, string compName)
        {
            var teachers = await GetTeacherByCompetenceAsync(compName);

            foreach (var teacher in teachers)
            {
                if (teacher.TeacherId==id)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<List<CompetenceViewModel>> GetCompetenciesAsync()
        {
            var url = $"{_config.GetValue<string>("baseUrl")}/competencies/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            var competencies = await respons.Content.ReadFromJsonAsync<List<CompetenceViewModel>>();

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kompetenserna skulle hämtas");
            }

            return competencies ?? new List<CompetenceViewModel>();
        }


    }
}