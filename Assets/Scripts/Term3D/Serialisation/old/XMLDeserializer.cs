using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

//class qui est notre "main" pour déserialiser notre class d'un fichier "base.xml"
public class XMLDeserializer : MonoBehaviour {
	
	void Start() {
		
		Smap map = XMLOp.Deserialize<Smap>("base.xml");

		//ici on affiche sur le console le nom du premier modèle, de la première salle, de notre map
		Debug.Log(map.rooms[0].models[0].name);
		//si le nom s'affiche, la désérialisation s'est bien effectué
		
	}
}