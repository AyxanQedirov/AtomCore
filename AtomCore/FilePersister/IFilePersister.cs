using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.FilePersister;

public interface IFilePersister
{
    void Save(string path, string fileName, string base64File);
    string ReadAsBase64(string path);
    string ReadInnerText(string path);
}
