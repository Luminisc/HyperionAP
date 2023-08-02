namespace HSAT.Services
{
    public partial class FileService
    {
        public partial async Task<FileResult> OpenFileDialog(string path = null)
        {
            return await FilePicker.Default.PickAsync();
        }

        public partial Stream OpenFile(string path)
        {
            return null;
        }

        public partial bool SaveFile(string path, Stream data)
        {
            return true;
        }
    }
}
