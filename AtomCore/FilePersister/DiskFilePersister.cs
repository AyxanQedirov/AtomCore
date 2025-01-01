using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.FilePersister;

public class DiskFilePersister : IFilePersister
{
    public string ReadAsBase64(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        string file = Convert.ToBase64String(bytes);

        return file;
    }

    public string ReadInnerText(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        string file = streamReader.ReadToEnd();
        streamReader.Close();

        return file;
    }

    public void Save(string path, string fileName, string base64File)
    {
        string savePath = $"{path}\\{fileName}";

        Directory.CreateDirectory(path);

        File.WriteAllBytes(savePath, Convert.FromBase64String(base64File));
    }
}
