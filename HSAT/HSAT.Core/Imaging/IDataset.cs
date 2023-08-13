namespace HSAT.Core.Imaging
{
    public interface IDataset
    {
        IBand GetBand(int band);

        int BandsCount { get; }

        int Width { get; }

        int Height { get; }
    }
}
