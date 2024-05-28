using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;

namespace ACME.LearningCenterPlatform.API.Publishing.Application.Internal.QueryServices;

public class TutorialQueryService(ITutorialRepository tutorialRepository) : ITutorialQueryService
{
    public async Task<Tutorial?> Handle(GetTutorialByIdQuery query)
    {
        return await tutorialRepository.FindByIdAsync(query.TutorialId);
    }

    public async Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsQuery query)
    {
        return await tutorialRepository.ListAsync();
    }

    public async Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsByCategoryIdQuery query)
    {
        return await tutorialRepository.FindByCategoryIdAsync(query.CategoryId);
    }
}