using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace mvvm.Model
{
    public class Serializer
    {
        public Serializer()
        { 
        }

        public void SerializeObject(string filename, Map map)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, map);
            stream.Close();
        }
        public Map DeSerializeObject(string filename)
        {
            Map map;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            map = (Map)bFormatter.Deserialize(stream);
            stream.Close();
            return map;
        }

    }
}
