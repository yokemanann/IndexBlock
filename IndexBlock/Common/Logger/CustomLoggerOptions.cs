namespace IndexBlock.Common.Logger
{
    public class CustomLoggerOptions
    {
        public virtual string FilePath { get; set; }

        public virtual string FolderPath { get; init; } = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\logs";
    }
}
