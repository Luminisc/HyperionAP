using HSAT.Core.DataAccess;
using HSAT.Core.DI;
using HSAT.Core.Imaging;

namespace HSAT.Core
{
    /// <summary>Context holding current project</summary>
    public class ProjectContext
    {
        /// <summary>Instance of project context</summary>
        public static ProjectContext Instance = new();

        /// <summary>Fired when project changed (via <see cref="LoadProject(Project)"/>)</summary>
        public static event EventHandler ProjectChanged;

        public Project Project { get; private set; }

        private Lazy<IDataset> Dataset;

        private IDataAccessService dataAccessService;

        private ProjectContext()
        {
            dataAccessService = DIResolver.Resolve<IDataAccessService>();
            Dataset = new Lazy<IDataset>(() => this.dataAccessService.LoadDataset(Project.ImagePath), true);
        }

        /// <summary>Loads project in context</summary>
        public void LoadProject(Project project)
        {
            Project = project;
            ProjectChanged?.Invoke(this, EventArgs.Empty);
        }

        public IDataset GetDataset() => Dataset.Value;
    }
}
