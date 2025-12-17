namespace API.Services;

public sealed class PdfImageExtractor
{
    public List<Bitmap> Extract(IFormFile pdfFile)
    {
        using var stream = new MemoryStream();
        pdfFile.CopyTo(stream);

        var images = new List<Bitmap>();

        using var reader = DocLib.Instance.GetDocReader(
            stream.ToArray(),
            new PageDimensions(3000, 3000));

        for (int i = 0; i < reader.GetPageCount(); i++)
        {
            using var page = reader.GetPageReader(i);
            images.Add(CreateBitmap(page));
        }

        return images;
    }

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
}