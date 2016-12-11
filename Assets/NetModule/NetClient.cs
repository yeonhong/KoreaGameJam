using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections;

public struct stUserData
{
	public int int1;
	public int int2;
	public int int3;
}



public class NetClient : NetworkBehaviour 
{
	//[SyncVar(hook = "OnChangeSyncVar")]
	[SyncVar]
	public string m_nickName = "Guest";

//	[SyncVar(hook = "OnChangeSyncVar")]
//	public int nSyncVar = 100;
//	// onchange test.
//	void OnChangeSyncVar(int value)
//	{
//		Debug.Log ("OnChangeSyncVar " + value);
//	}
	// Client Data Struct.

//	public class cUserData : SyncListStruct<stUserData> {}
//	public cUserData m_bufs = new cUserData();

	[SyncVar]
	public stUserData userData;

	public bool bNicknameConfirm = false;

	void Update()
	{
		// init...
		if (GameManager.instance.myNetClient == null && isLocalPlayer) {
			GameManager.instance.myNetClient = this;

			gameObject.transform.parent = GameObject.Find ("Canvas").transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.name = "NetClient" + netId;

			if (this.isServer) {
				GameManager.instance.SetDealerUI (gameObject);
			} else {
				GameManager.instance.SetPlayerUI (gameObject);
			}
		}
	}

//	void Update()
//	{
//		if (isLocalPlayer) {
//			//net test key bind.
//			if (Input.GetKeyDown (KeyCode.Q)) {
//				CmdDoSomething (); // client to server message.
//			} else if (Input.GetKeyDown (KeyCode.E)) {
//				RpcDoSomething (); // send to client message.
//			} else if (Input.GetKeyDown (KeyCode.W)) {
//				//NetClientTestManager.instance.InitLogString ();
//			}
//		}
//	}

	#region Command Region
	// [Command] 속성은 다음 함수가 클라이언트에 의해 호출되지만 서버에서 실행됨을 나타냅니다. 함수의 모든 인수는 명령과 함께 자동으로 서버로 전달됩니다. 
	// 명령은 로컬 플레이어 개체에서만 보낼 수 있습니다. 
	[Command]
	public void CmdDoSomething()
	{
		Debug.Log ("CmdDoSomething " + isLocalPlayer);
		//NetClientTestManager.instance.AddLogString ("CmdDoSomething " + isLocalPlayer);
	}
	#endregion //Comman Region

	//isLocalPlayer << is mine identifier.

	#region RPC Region

	//ClientRpc 호출은 NetworkIdentity를 사용하여 서버의 생성 된 모든 개체에서 보낼 수 있습니다. 
	//이 함수는 서버에서 호출 되더라도 클라이언트에서 실행됩니다. ClientRpc는 Command의 반대입니다. 
	//명령은 클라이언트에서 호출되지만 서버에서 실행됩니다. ClientRpc는 서버에서 호출되지만 클라이언트에서 실행됩니다.
	[ClientRpc]
	public void RpcDoSomething()
	{
		Debug.Log("RpcDoSomething : " + isLocalPlayer);
		//NetClientTestManager.instance.AddLogString ("RpcDoSomething : " + isLocalPlayer);
	}

	#endregion //RPC Region

	public void SendNickName(string nickname)
	{
		m_nickName = nickname;

		CmdSendNickName (m_nickName, netId);
	}

	[Command]
	public void CmdSendNickName(string nickname, NetworkInstanceId netID)
	{
		// all client count need!
		int allClientCount = GameManager.instance.listNetClient.Count;

		if (GameManager.instance.lookupNetClient.ContainsKey (netId)) {
			GameManager.instance.lookupNetClient [netId].m_nickName = nickname;
			GameManager.instance.lookupNetClient [netId].bNicknameConfirm = true;
		}
		else
			Debug.LogError ("???? unknown id " + netId);

		RpcSetPlayerNickName (nickname, netID);

		if (true == GameManager.instance.IsAllNicknameSetting()) {
			RpcPlayerAllReady ();
		}
	}

	[ClientRpc]
	public void RpcSetPlayerNickName(string nickname, NetworkInstanceId netID)
	{
		Debug.Log ("Change NickName : " + netID + " : " + nickname);
	}

	[Command]
	public void CmdSetStatePlayer(GameState _setState) {
		RpcSetStatePlayer (_setState);
	}

	[ClientRpc]
	public void RpcPlayerAllReady()
	{
		Debug.Log ("RpcPlayerAllReady gameObject " + gameObject.name);
		//if (gameObject.tag == "GameController")
		if(isServer && gameObject.transform.childCount > 0)
			gameObject.transform.GetChild (0).GetComponent<Dealer_Script> ().StartGame_Func ();
	}
		
	[ClientRpc]
	public void RpcSetStatePlayer(GameState _setState)
	{
//		Debug.Log ("RpcSetStatePlayer gameObject " + gameObject.name);
//		Debug.Log ("RpcSetStatePlayer gameObject " + gameObject.transform.childCount);
//		Debug.Log (".. " + GameObject.Find ("Player(Clone)"));
//
//		if (gameObject.transform.childCount > 0)
//			gameObject.transform.GetChild(0).GetComponent<Player_Script>().SetState_Func (_setState);

		GameObject objPlayer = GameObject.Find ("Player(Clone)");
		if (objPlayer != null) {
			objPlayer.GetComponent<Player_Script> ().SetState_Func (_setState);
		}
	}

	[Command]
	public void CmdGenerateHint()
	{
		GameManager.instance.gameObject.SendMessage ("SG");
	}

	public void ReqGetHint()
	{
		string str = netId.ToString ();
		CmdGetHint (netId, (int.Parse (str))%4);
	}

	[Command]
	public void CmdGetHint(NetworkInstanceId netid, int idxHint)
	{
		idxHint *= 2;

		string hint1 = GameManager.instance.listHint[idxHint + 0];
		string hint2 = GameManager.instance.listHint[idxHint + 1];

		RpcGetHint (netid, hint1, hint2);
	}

	[ClientRpc]
	public void RpcGetHint(NetworkInstanceId _netid, string hint1, string hint2)
	{
		GameObject objPlayer = GameObject.Find ("Player(Clone)");
		if (objPlayer != null && objPlayer.transform.parent.GetComponent<NetClient>().netId == _netid) {
			objPlayer.GetComponent<Player_Script> ().RecvHint (hint1, hint2);
		}
	}

	public void ReqInvestCoin(int[] arrCoin)
	{
		CmdInvestCoin (netId, arrCoin);
	}

	[Command]
	public void CmdInvestCoin(NetworkInstanceId netid, int[] arrCoin)
	{
		Debug.Log ("CmdInvestCoin");
		GameObject objDealer = GameObject.Find ("Dealer(Clone)");
		if (objDealer != null)
			objDealer.GetComponent<Dealer_Script> ().Invest_Func (netid, arrCoin);
	}

	[ClientRpc]
	public void RpcStockResultInYesterday(int company1, int company2, int company3)
	{
		GameObject objPlayer = GameObject.Find ("Player(Clone)");
		if (objPlayer != null) {
			objPlayer.GetComponent<Player_Script> ().ReciveStockInfo (company1, company2, company3);
		}
	}

	[ClientRpc]
	public void RpcSendScore(NetworkInstanceId _netid, int score)
	{
		GameObject objPlayer = GameObject.Find ("Player(Clone)");
		if (objPlayer != null && objPlayer.transform.parent.GetComponent<NetClient>().netId == _netid) {
			objPlayer.GetComponent<Player_Script> ().RecvScore (score);
		}
	}
}

