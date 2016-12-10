using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace NetTestSpace {
public class NetManager : NetworkManager 
{
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		Debug.Log ("OnServerAddPlayer " + playerControllerId);

		var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		player.name = "NetClient" + playerControllerId;

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}

}