using HyperionAP.Core.Models.EntitiesBase;

namespace HyperionAP.Core.Models;
public class User() : Entity(Guid.Empty)
{
    private readonly List<Project> projects = [];

    public required string DisplayName { get; init; }
    public required string Login { get; init; }
    public IReadOnlyCollection<Project> Projects => projects.AsReadOnly();
}
