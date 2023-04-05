using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.ViewModels.Competence;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Repositories
{
    public class CompetenceRepository : ICompetenceRepository
    {
        private readonly EducationContext _context;
        private readonly IMapper _mapper;
        public CompetenceRepository(EducationContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task<List<CompetenceViewModel>> ListAllCompetenciesAsync()
        {
            return await _context.Competencies
            .ProjectTo<CompetenceViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }
    }
}