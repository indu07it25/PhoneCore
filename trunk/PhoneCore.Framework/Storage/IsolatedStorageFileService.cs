using System;
using System.IO;
using System.IO.IsolatedStorage;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;

namespace PhoneCore.Framework.Storage
{
    /// <summary>
    /// Isolated storage file service
    /// </summary>
    public class IsolatedStorageFileService: IFileSystemService
    {
        [Dependency]
        public ITrace Trace { get; set; }
        [Dependency("FileSystemService")]
        public ITraceCategory Category { get; set; }

        private static IsolatedStorageFile _store;

        public IsolatedStorageFileService(IConfigSection config)
        {
            _store = IsolatedStorageFile.GetUserStoreForApplication();
        }

        public Stream OpenFile(string path, FileMode mode)
        {
            Trace.Info(Category, String.Format("Open file: {0}, mode: {1}", path, mode));
            return _store.OpenFile(path, mode);
        }

        public void CreateDirectory(string path)
        {
            if (!_store.DirectoryExists(path))
            {
                Trace.Info(Category, String.Format("Create directory {0}", path));
                _store.CreateDirectory(path);
            }
        }


        public bool DirectoryExists(string path)
        {
            return _store.DirectoryExists(path);
        }
    }
}
