using System.Text.Json.Serialization;
using HyperionAP.Core.Models.EntitiesBase;

namespace HyperionAP.Core.Models;

public class Dataset(string name, DatasetType datasetType, string filename, string? description = null) : Entity
{
    [JsonIgnore]
    public Project Project { get; protected set; }
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;
    public DatasetType DatasetType { get; private set; } = datasetType;
    public string Filename { get; private set; } = filename;
}

public enum DatasetType
{
    Unknown = 0,
    ENVI = 10,
}
