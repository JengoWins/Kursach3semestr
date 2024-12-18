using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasangerRilot.Classes
{
    class ImageConvert
    {
        public ImageConvert() { }

        public byte[] IsConvert(string FileNamePath)
        {
            byte[] ImageData = null;
            FileStream fs = new FileStream(FileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            ImageData = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return ImageData;
        }
    }
}
