using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApi.ViewModels;

namespace EducationApi.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryViewModel>> ListAllCategoriesAsync();
    }
}