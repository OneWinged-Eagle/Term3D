using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

//classe créée pour simplifier XMLDeserializer et XMLSerializer, elle est appellé dans ces deux classes
public class XMLOp {
	
	public static void Serialize(object item, string path) {
		XmlSerializer serializer = new XmlSerializer(item.GetType());
		StreamWriter writer = new StreamWriter(path);
		serializer.Serialize(writer.BaseStream, item);
		writer.Close();
	}

	public static T Deserialize<T>(string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		StreamReader reader = new StreamReader(path);
		T deserialized = (T)serializer.Deserialize(reader.BaseStream);
		reader.Close();
		return deserialized;
	}
}