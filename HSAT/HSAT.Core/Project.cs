namespace HSAT.Core
{
    public class Project
    {
        /// <summary>Name of project</summary>
        public string Name { get; private set; }

        /// <summary>Path to project file</summary>
        public string ProjectPath { get; private set; }

        /// <summary>Path to main image</summary>
        public string ImagePath { get; private set; }

        /// <summary>Path to images data</summary>
        public List<string> ImagePaths { get; private set; }

        private Project() { }

        public static Project Create(string name, string projectPath, string imagePath)
        {
            return new Project
            { 
                Name = name,
                ProjectPath = projectPath,
                ImagePath = imagePath,
            };
        }
    }
}
