using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentClientMVC.ViewModels;

namespace StudentClientMVC.Models
{
    public class CourseServiceModel
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _config;
        public CourseServiceModel(IConfiguration config)
        {
            _config = config;

            _baseUrl = $"{_config.GetValue<string>("baseUrl")}/courses";
            
        }
        public async Task<CourseIndexViewModel> LoadIndexPageAsync()
        {
            var categories = await GetCategoriesAsync();

            var course = await ListAllCoursesAsync();

            var indexPage = new CourseIndexViewModel{
                Courses = course,
                Categories = categories
            };

            return indexPage;
        }

        public async Task<CourseIndexViewModel> UpdateIndexPageAsync(string input)
        {
            var categories = await GetCategoriesAsync();

            var courses = new List<CourseViewModelLessInfo>();

            if (input is "alla")
            {
                courses = await ListAllCoursesAsync();
            }
            else
            {
                courses = await GetCourseByCategoryAsync(input);
            }
            

            var indexPage = new CourseIndexViewModel{
                Courses = courses,
                Categories = categories!
            };

            return indexPage;
        }
        public async Task<List<CourseViewModelLessInfo>> ListAllCoursesAsync()
        {
            var url = $"{_baseUrl}/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kurserna skulle hämtas");
            }
            
            var courses = await respons.Content.ReadFromJsonAsync<List<CourseViewModelLessInfo>>();

            return courses ?? new List<CourseViewModelLessInfo>();
        }

        

        public async Task<CourseViewModel> GetCourseByIdAsync(int Id)
        {
            var url = $"{_baseUrl}/byid/{Id}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när kursen skulle hämtas");
            }
            
            var course = await respons.Content.ReadFromJsonAsync<CourseViewModel>();

            return course ?? new CourseViewModel();
        }

        private async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            var url = $"{_config.GetValue<string>("baseUrl")}/categories/list";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            var competencies = await respons.Content.ReadFromJsonAsync<List<CategoryViewModel>>();

            if (!respons.IsSuccessStatusCode)
            {
                throw new Exception("Det gick fel när alla kategorierna skulle hämtas");
            }

            return competencies ?? new List<CategoryViewModel>();
        }

        private async Task<List<CourseViewModelLessInfo>> GetCourseByCategoryAsync(string input)
        {
            var url = $"{_baseUrl}/bycategory/{input}";
            
            using var client = new HttpClient();

            var respons = await client.GetAsync(url);

            if(!respons.IsSuccessStatusCode)
            {
                throw new Exception("Något gick fel vid hämtning av kurser");
            }
            
            var courses = await respons.Content.ReadFromJsonAsync<List<CourseViewModelLessInfo>>();

            return courses ?? new List<CourseViewModelLessInfo>();
        }
    }
}