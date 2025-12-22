using System.Diagnostics;

namespace API.Services.Ocr;

public sealed class OcrService
{
    public string ReadText(Stream imageStream)
    {
        var tempImage = Path.GetTempFileName() + ".png";
        var tempOut = Path.GetTempFileName();

        try
        {
            using (var fs = File.Create(tempImage))
                imageStream.CopyTo(fs);

            var psi = new ProcessStartInfo
            {
                FileName = "tesseract",
                Arguments = $"{tempImage} {tempOut} -l eng",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using var process = Process.Start(psi)!;
            process.WaitForExit();

            return File.Exists(tempOut + ".txt")
                ? File.ReadAllText(tempOut + ".txt")
                : string.Empty;
        }
        finally
        {
            if (File.Exists(tempImage)) File.Delete(tempImage);
            if (File.Exists(tempOut + ".txt")) File.Delete(tempOut + ".txt");
        }
    }
}