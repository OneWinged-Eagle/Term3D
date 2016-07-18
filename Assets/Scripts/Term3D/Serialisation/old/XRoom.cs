using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//class room qui est constituée d'une liste des modeles object qui l'a compose

public class XRoom {

	//nom de la salle
	public string name;

	//taile de la salle
	public int sizex;
	public int sizey;
	public int sizez;

	//liste des modèles qui composent la salle
	public List<XModel> models = new List<XModel>();
}
