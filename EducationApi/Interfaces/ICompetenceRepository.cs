using EducationApi.ViewModels.Competence;

namespace EducationApi.Interfaces
{
    public interface ICompetenceRepository
    {
        public Task<List<CompetenceViewModel>> ListAllCompetenciesAsync();
    }
}