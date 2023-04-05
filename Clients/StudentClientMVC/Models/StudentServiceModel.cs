using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentClientMVC.ViewModels;

namespace StudentClientMVC.Models
{
    public class StudentServiceModel
    {
         private readonly string _baseUrl;
        private readonly IConfiguration _config;
        public StudentServiceModel(IConfiguration config)
        {
            _config = config;

            _baseUrl = $"{_config.GetValue<string>("baseUrl")}/students";
            
        }

        public async Task<bool> SignupStudentAsync(CourseSignupViewModel model,int courseId)
        {
            using var client = new HttpClient();
            var url = $"{_baseUrl}/signupjoin/{courseId}";

            var respons = await client.PostAsJsonAsync(url,model);

            if (respons.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}