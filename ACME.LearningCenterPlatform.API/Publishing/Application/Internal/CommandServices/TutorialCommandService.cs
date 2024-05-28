using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Repositories;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
using ACME.LearningCenterPlatform.API.Shared.Domain.Repositories;

namespace ACME.LearningCenterPlatform.API.Publishing.Application.Internal.CommandServices;

public class TutorialCommandService(ITutorialRepository tutorialRepository, ICategoryRepository categoryRepository,IUnitOfWork unitOfWork) : ITutorialCommandService
{
    public async Task<Tutorial?> Handle(CreateTutorialCommand command)
    {
        var tutorial = new Tutorial(command.Title, command.Summary, command.CategoryId);
        await tutorialRepository.AddAsync(tutorial);
        await unitOfWork.CompleteAsync();
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        tutorial.Category = category;
        return tutorial;
    }

    public async Task<Tutorial?> Handle(AddVideoAssetToTutorialCommand command)
    {
        var tutorial = await tutorialRepository.FindByIdAsync(command.TutorialId);
        if (tutorial is null) throw new Exception("Tutorial not found");
        try
        {
            tutorial.AddVideo(command.VideoUrl);
            await unitOfWork.CompleteAsync();
            return tutorial;
        }
        catch
        {
            return null;
        }
    }
}