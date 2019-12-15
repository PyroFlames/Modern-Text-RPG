using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public class DataFile
    {
        public string Path;
        public FileManager.FileType Type;

        public DataFile(string path, FileManager.FileType ft)
        {
            Path = path;
            Type = ft;
        }
    }
}
