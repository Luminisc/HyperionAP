using System.IO.Compression;

namespace HyperionAP.UI.Server.Services
{
    internal class FilesService
    {
        static Dictionary<Guid, FileInfo> Files = new();

        private const string UploadFolder = "Uploads";
        private string GetDatasetFolderPath(Guid fileId) => Path.Combine(UploadFolder, fileId.ToString().ToUpperInvariant());

        public FileInfo AddFile(Guid fileId, string filename)
        {
            var fileInfo = new FileInfo() { FileId = fileId, FileName = filename };
            Files.Add(fileId, fileInfo);
            return fileInfo;
        }

        public IEnumerable<(Guid fileId, string filename)> GetFiles()
        {
            return Files.Select(x => (x.Key, x.Value.FileName));
        }

        internal async Task<FileInfo> AddFile(IFormFile file)
        {
            var fileId = Guid.NewGuid();
            var filename = file.FileName;

            var folderPath = GetDatasetFolderPath(fileId);
            if (Directory.Exists(folderPath))
                throw new ArgumentException("Id already present!");

            Directory.CreateDirectory(folderPath);
            using var fileStream = file.OpenReadStream();
            var archiveSignature = GetArchiveSignature(fileStream);
            if (archiveSignature != KnownArchives.Zip)
                throw new ArgumentException($"Unsupported archive type! {archiveSignature}");

            ZipFile.ExtractToDirectory(fileStream, folderPath);
            
            var fileInfo = AddFile(fileId, filename);

            return fileInfo;
        }

        public KnownArchives GetArchiveSignature(Stream stream)
        {
            using var reader = new BinaryReader(stream);

            var header = reader.ReadBytes(8); // Читаем первые 8 байт

            // ZIP: PK (50 4B 03 04 или 50 4B 05 06 или 50 4B 07 08)
            if (header.Length >= 4 && header[0] == 0x50 && header[1] == 0x4B)
            {
                return KnownArchives.Zip;
            }

            // RAR: Rar! (52 61 72 21 1A 07 00)
            if (header.Length >= 7 && header[0] == 0x52 && header[1] == 0x61 &&
                header[2] == 0x72 && header[3] == 0x21 && header[4] == 0x1A &&
                header[5] == 0x07 && header[6] == 0x00)
            {
                return KnownArchives.Rar;
            }

            // 7z: 7z (37 7A BC AF 27 1C)
            if (header.Length >= 6 && header[0] == 0x37 && header[1] == 0x7A &&
                header[2] == 0xBC && header[3] == 0xAF && header[4] == 0x27 &&
                header[5] == 0x1C)
            {
                return KnownArchives._7z;
            }

            stream.Seek(-header.Length, SeekOrigin.Current);

            return KnownArchives.Unknown;
        }

        internal string GetDatasetPath(Guid fileId)
        {
            var imgFilepath = Directory.EnumerateFiles(GetDatasetFolderPath(fileId)).Where(x => x.EndsWith("img")).First();
            return imgFilepath;
        }
    }

    public struct FileInfo
    { 
        public string FileName { get; set; }
        public Guid FileId { get; set; }
        public string DatasetType { get; set; }
    }

    internal enum KnownArchives
    {
        Unknown = 0,
        Zip = 1,
        Rar = 2,
        _7z = 3,
        Tar = 4,
        TarGz = 5,
    }
}
