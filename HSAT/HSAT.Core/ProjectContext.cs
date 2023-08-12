namespace HSAT.Core
{
    /// <summary>Context holding current project</summary>
    public class ProjectContext
    {
        /// <summary>Instance of project context</summary>
        public static ProjectContext Instance { get; private set; } = new();

        /// <summary>Fired when project changed (via <see cref="LoadProject(Project)"/>)</summary>
        public static event EventHandler ProjectChanged;

        public Project Project { get; private set; }

        private ProjectContext() { }

        /// <summary>Loads project in context</summary>
        public void LoadProject(Project project)
        {
            Project = project;
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
