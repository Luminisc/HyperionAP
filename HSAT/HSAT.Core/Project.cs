namespace HSAT.Core
{
    public class Project
    {
        /// <summary>Name of project</summary>
        public string Name { get; protected set; }

        /// <summary>Path to project file</summary>
        public string ProjectPath { get; protected set; }

        /// <summary>Path to main image</summary>
        public string ImagePath { get; protected set; }

        /// <summary>Path to images data</summary>
        public List<string> ImagePaths { get; protected set; }

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

        internal class ProjectSaveModel
        {
            public string Name { get; set; }

            public string ProjectPath { get; set; }

            public string ImagePath { get; set; }

            public List<string> ImagePaths { get; set; }

            public Project ToModel() => new()
            {
                Name = Name,
                ProjectPath = ProjectPath,
                ImagePath = ImagePath,
                ImagePaths = ImagePaths,
            };

            public static ProjectSaveModel FromModel(Project project) => new()
            {
                Name = project.Name,
                ProjectPath = project.ProjectPath,
                ImagePath = project.ImagePath,
                ImagePaths = project.ImagePaths,
            };
        }
    }
}
