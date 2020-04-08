using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesarPDF
{

    
    public static class FileDictionary
    {

        private static string file = @"~\..\PagosDictionary\Dictionary.bat";
        public static Dictionary<string, string> Read()
        {
            var result = new Dictionary<string, string>();
            using (FileStream fs = File.OpenRead(file))
            using (BinaryReader reader = new BinaryReader(fs))
            {

                if (reader.BaseStream.Length > 0)
                {
                    // Get count.
                    int count = reader.ReadInt32();
                    // Read in all pairs.
                    for (int i = 0; i < count; i++)
                    {
                        string key = reader.ReadString();
                        string value = reader.ReadString();
                        result[key] = value;
                    }
                }
            }
            return result;
        }



        public static void Write(Dictionary<string, string> dictionary)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Put count.
                writer.Write(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key);
                    writer.Write(pair.Value);
                }
            }
        }
    }
}
