using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EducationContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(EducationContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task<List<CategoryViewModel>> ListAllCategoriesAsync()
        {
            return await _context.Categories
            .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }
    }
}