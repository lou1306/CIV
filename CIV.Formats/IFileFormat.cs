namespace CIV.Formats
{
    public interface IFileFormat
    {
        string Extension { get; }

        void Load(string path);

    }
}
