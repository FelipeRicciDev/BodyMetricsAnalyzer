namespace API.Services.Pdf;

public sealed class PdfImageExtractor
{
    #region PDF Extraction
    [SupportedOSPlatform("windows")]
    public List<Bitmap> ExtractImages(Stream pdfStream)
    {
        using var memory = new MemoryStream();
        pdfStream.CopyTo(memory);

        var images = new List<Bitmap>();

        using var reader = DocLib.Instance.GetDocReader(
            memory.ToArray(),
            new PageDimensions(3000, 3000));

        for (int i = 0; i < reader.GetPageCount(); i++)
        {
            using var page = reader.GetPageReader(i);
            images.Add(CreateBitmap(page));
        }

        return images;
    }
    #endregion

    #region Bitmap Creation
    [SupportedOSPlatform("windows")]
    private static Bitmap CreateBitmap(IPageReader page)
    {
        var bitmap = new Bitmap(
            page.GetPageWidth(),
            page.GetPageHeight(),
            PixelFormat.Format32bppArgb);

        var raw = page.GetImage();

        var data = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.WriteOnly,
            bitmap.PixelFormat);

        Marshal.Copy(raw, 0, data.Scan0, raw.Length);
        bitmap.UnlockBits(data);

        return bitmap;
    }
    #endregion
}