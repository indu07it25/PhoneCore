using System.IO;

namespace PhoneCore.Framework.Storage
{
    /// <summary>
    /// Provides the access to file storage
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// Opens file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        Stream OpenFile(string path, FileMode mode);

        /// <summary>
        /// Creates directory
        /// </summary>
        /// <param name="path"></param>
        void CreateDirectory(string path);

        /// <summary>
        /// Checks whether the directory exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DirectoryExists(string path);
    }
}
