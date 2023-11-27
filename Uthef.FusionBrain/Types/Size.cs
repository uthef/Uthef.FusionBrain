namespace Uthef.FusionBrain.Types
{
    public struct Size
    {
        public int Width { get; set; } = 1024;
        public int Height { get; set; } = 1024;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static Size FromWidth(int width, double ratio = 1) => new(width, (int)(width / ratio));
        public static Size FromHeight(int height, double ratio = 1) => new((int)(height * ratio), height);
    }
}
