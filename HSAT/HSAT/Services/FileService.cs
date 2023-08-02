using CommunityToolkit.Maui.Storage;

namespace HSAT.Services
{
    /// <summary>Service to work with files</summary>
    public interface IFileService
    {
        /// <summary>Open file dialog to select file</summary>
        /// <param name="path">Initial path</param>
        /// <returns>Path to selected file, otherwise null </returns>
        Task<FileResult> OpenFileDialog();
        /// <summary>Open folder dialog to select folder</summary>
        /// <param name="path">Initial path</param>
        /// <returns>Path to selected folder, otherwise null</returns>
        Task<FolderPickerResult> OpenFolderDialog(string path = null);
        ///// <summary>Open file</summary>
        ///// <param name="path">Path to file</param>
        ///// <returns>File stream</returns>
        //Task<Stream> OpenFile(string path);
        /// <summary>Saves file in system</summary>
        /// <param name="path">Path to file</param>
        /// <param name="data">Stream of data</param>
        /// <returns>True if saving is successful</returns>
        Task<bool> SaveFile(string path, Stream data);
    }


    public partial class FileService : IFileService
    {
        public partial Task<FileResult> OpenFileDialog();
        public partial Task<FolderPickerResult> OpenFolderDialog(string path = null);
        public partial Task<Stream> OpenFile(string path);
        public partial Task<bool> SaveFile(string path, Stream data);
    }
}
