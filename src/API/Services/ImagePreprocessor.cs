namespace API.Services;

public static class ImagePreprocessor
{
    public static Bitmap ToGrayscale(Bitmap original)
    {
        var gray = new Bitmap(
            original.Width,
            original.Height,
            PixelFormat.Format24bppRgb);

        using var graphics = Graphics.FromImage(gray);

        var matrix = new ColorMatrix(new[]
        {
            new float[] {0.3f, 0.3f, 0.3f, 0, 0},
            new float[] {0.59f, 0.59f, 0.59f, 0, 0},
            new float[] {0.11f, 0.11f, 0.11f, 0, 0},
            new float[] {0,    0,    0,    1, 0},
            new float[] {0,    0,    0,    0, 1}
        });

        using var attributes = new ImageAttributes();
        attributes.SetColorMatrix(matrix);

        graphics.DrawImage(
            original,
            new Rectangle(0, 0, gray.Width, gray.Height),
            0, 0, original.Width, original.Height,
            GraphicsUnit.Pixel,
            attributes);

        return gray;
    }
}