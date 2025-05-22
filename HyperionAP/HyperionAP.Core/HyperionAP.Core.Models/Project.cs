using System.Text.Json.Serialization;
using HyperionAP.Core.Models.EntitiesBase;

namespace HyperionAP.Core.Models;

public class Project() : Entity
{
    private readonly List<Pipeline> pipelines = [];
    private readonly List<Dataset> datasets = [];

    public Project(string name, User owner) : this()
    {
        Name = name;
        Owner = owner;
    }

    [JsonIgnore]
    public User Owner { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Pipeline> Pipelines => pipelines.AsReadOnly();
    public IReadOnlyCollection<Dataset> Datasets => datasets.AsReadOnly();
}
