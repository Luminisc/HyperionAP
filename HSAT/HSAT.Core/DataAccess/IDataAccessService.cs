using HSAT.Core.Imaging;

namespace HSAT.Core.DataAccess
{
    public interface IDataAccessService
    {
        IDataset LoadDataset(string datasetPath);
    }
}
