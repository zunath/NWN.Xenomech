using Avalonia.Media.Imaging;

namespace XM.App.Editor.Models;

public class IconOption : IDisposable
{
    public required string Name { get; init; }
    public required Bitmap Image { get; init; }

    public void Dispose()
    {
        Image.Dispose();
    }
}


