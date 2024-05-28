using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.VisualBasic;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

public class Category
{
    public Category(string name)
    {
        Name = name;
    }

    public Category(CreateCategoryCommand command)
    {
        Name = command.Name;
    }

    public Category()
    {
        Name = string.Empty;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Tutorial> Tutorials { get; }
}