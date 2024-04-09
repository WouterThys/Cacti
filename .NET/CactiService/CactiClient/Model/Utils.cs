using System;
using System.Collections.Generic;
using System.IO;

namespace CactiClient.Model
{
    public static class Utils
    {
        public static readonly List<string> ImageExtensions = new() { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public static bool IsImage(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return false;

            var extension = Path.GetExtension(filename).ToUpperInvariant();

            return ImageExtensions.Contains(extension);
        }
    }
}
