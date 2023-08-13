namespace HSAT.Core.Imaging
{
    public interface IBand
    {
        ImageDataType DataType { get; }

        OpResult ReadRaster(int xOffset, int yOffset, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace);

        OpResult ComputeStatistics(bool approx_ok, out double min, out double max, out double mean, out double stddev);
    }
}
