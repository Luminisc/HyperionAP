using System.Text.Json.Serialization;
using HyperionAP.Core.Models.EntitiesBase;

namespace HyperionAP.Core.Models;

public class Pipeline(string name, string? description = null) : Entity
{
    [JsonIgnore]
    public Project Project { get; protected set; }
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;

    public class ProcessingGraphNode(Guid id) : Entity(id)
    {

    }

    public class ProcessingGraphEdge(Guid id) : Entity(id)
    {

    }
}
