using CommunityToolkit.Maui.Storage;

namespace HSAT.Services
{
    public partial class FileService
    {
        public partial async Task<FileResult> OpenFileDialog()
        {
            return await FilePicker.Default.PickAsync();
        }

        public partial async Task<FolderPickerResult> OpenFolderDialog(string path = null)
        {
            return await FolderPicker.Default.PickAsync(path, CancellationToken.None);
        }

        public partial async Task<Stream> OpenFile(string path)
        {
            return await Task.FromResult((Stream) null);
        }

        public partial async Task<bool> SaveFile(string path, Stream data)
        {
            await FileSaver.Default.SaveAsync("","",null, CancellationToken.None);
            return true;
        }
    }
}
