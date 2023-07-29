namespace HSAT.Core
{
    public class Project
    {
        /// <summary>Name of project</summary>
        public string Name { get; set; }

        /// <summary>Path to project file</summary>
        public string ProjectPath { get; }

        /// <summary>Path to images data</summary>
        public List<string> ImagePaths { get; }
    }
}
