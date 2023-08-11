namespace HSAT.Core
{
    public class ProjectContext
    {
        public static ProjectContext Instance { get; private set; } = new();

        public Project Project { get; private set; }

        public void LoadProject(Project project)
        {
            Project = project;
        }
    }
}
