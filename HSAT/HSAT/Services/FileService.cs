namespace HSAT.Services
{
    /// <summary>Service to work with files</summary>
    public interface IFileService
    {
        /// <summary>Open file dialog to select file</summary>
        /// <param name="path">Default path</param>
        /// <returns>Path to selected file, otherwise null </returns>
        Task<FileResult> OpenFileDialog(string path = null);
        /// <summary>Open file</summary>
        /// <param name="path">Path to file</param>
        /// <returns>File stream</returns>
        Stream OpenFile(string path);
        /// <summary>Saves file in system</summary>
        /// <param name="path">Path to file</param>
        /// <param name="data">Stream of data</param>
        /// <returns>True if saving is successful</returns>
        bool SaveFile(string path, Stream data);
    }


    public partial class FileService : IFileService
    {
        public partial Task<FileResult> OpenFileDialog(string path = null);
        public partial Stream OpenFile(string path);
        public partial bool SaveFile(string path, Stream data);
    }
}
