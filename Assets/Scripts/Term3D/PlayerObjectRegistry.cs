using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using System.Collections;

public static class PlayerObjectRegistry
{
	static List<PlayerObject> players = new List<PlayerObject>();

	static PlayerObject CreatePlayer(BoltConnection connection)
	{
		PlayerObject player;

		player = new PlayerObject ();
		player.connection = connection;

		Debug.Log("ça passe ici lol");

		if (player.connection != null)
			player.connection.UserData = player;

		players.Add (player);

		return(player);
	}
		
	// this simply returns the 'players' list cast to
	// an IEnumerable<T> so that we hide the ability
	// to modify the player list from the outside.
	public static IEnumerable<PlayerObject> allPlayers {
		get { return players; }
	}

	// finds the server player by checking the
	// .isServer property for every player object.
	public static PlayerObject serverPlayer {
		get { return players.First(x => x.isServer); }
	}

	// utility function which creates a server player
	public static PlayerObject CreateServerPlayer() {
		return CreatePlayer(null);
	}

	// utility that creates a client player object.
	public static PlayerObject CreateClientPlayer(BoltConnection connection) {
		return CreatePlayer(connection);
	}

	// utility function which lets us pass in a
	// BoltConnection object (even a null) and have
	// it return the proper player object for it.
	public static PlayerObject GetPlayer(BoltConnection connection) {
		if (connection == null) {
			return serverPlayer;
		}

		return (PlayerObject)connection.UserData;
	}
}