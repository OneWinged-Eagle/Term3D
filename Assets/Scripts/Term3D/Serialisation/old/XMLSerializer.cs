using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

// class qui est notre "main" pour serialiser notre class dans un fichier "base.xml" qui sera appellé si on veut sauvegarder notre map
public class XMLSerializer : MonoBehaviour
{
	void Start()
	{
		// initialisation de nos class pour tester la sérialisation
		Smap map = new Smap(); // SMAP CONTIENT TOUT
		XRoom room = new XRoom();
		XModel model1 = new XModel();

		model1.name = "salut je suis l'object"; // valeur en dur pour valider la déserialisation

		// on "emboite" nos class : model -> room -> map
		room.models.Add(model1);
		map.Rooms.Add(room);
		map.Name = "base";

		// A noter que le code ci-dessus est un exemple en dur de ce à quoi la class Smap pourrait etre composé

		XMLOp.Serialize(map, "base.xml"); // on crée/modifier le fichier base.xml pour y écrire le contenu de la class Smap
	}

	void Update () {}
}
