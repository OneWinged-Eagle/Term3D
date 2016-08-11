using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// l'ultime qui class qui contient toutes les rooms
public class Smap
{
	//public int Coord { get; set; }
	public string Name;
	public List<XRoom> Rooms = new List<XRoom>();
}
