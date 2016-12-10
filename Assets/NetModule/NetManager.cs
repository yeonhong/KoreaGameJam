using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetManager : NetworkManager 
{
	public static NetManager instance {
		get {
			return _instance;
		}
	}
	public static NetManager _instance = null;

	void Awake() {
		_instance = this;
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		// inner NetClient Manage.
		NetClient newNetClient = player.GetComponent<NetClient> ();
		GameManager.instance.listNetClient.Add (newNetClient);
		GameManager.instance.lookupNetClient.Add (newNetClient.netId, newNetClient);
	}

	// called when connected to a server
	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect (conn);

		Debug.Log ("OnClientConnect");

		GameManager.instance.ChangeMode (eGameMode.SetName);
	}

//	// called when disconnected from a server
//	public virtual void OnClientDisconnect(NetworkConnection conn)
//	{
//		StopClient();
//	}



}
