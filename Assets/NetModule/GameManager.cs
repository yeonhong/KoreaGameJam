using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public enum eGameMode : int
{
	Lobby = 0,
	SetName,
	Start,
	Talking,
	Batting,
	Result,
	End
}

public class GameManager : Singleton<GameManager> 
{
	public const bool isDealerMode = true;
	public const bool isLowerPlayer = true;

	static eGameMode _gameMode = eGameMode.Lobby;
	public static eGameMode GameMode { get { return _gameMode; } }

	// network module manage.
	public NetClient myNetClient = null;
	public NetManager myNetManager = null;

	// netClinet manage.
	public List<NetClient> listNetClient = new List<NetClient>();
	public Dictionary<NetworkInstanceId, NetClient> lookupNetClient = new Dictionary<NetworkInstanceId, NetClient>();

	protected override void Start() {
		base.Start ();
		myNetManager = NetManager.instance;
	}

	public void initGame()
	{
		listNetClient.Clear ();
		lookupNetClient.Clear ();
		_gameMode = eGameMode.Lobby;
	}

	public void ChangeMode(eGameMode next)
	{
		switch (next) {
		case eGameMode.SetName:
			// my nickname transfer.
			UIManager.instance.SetUI_SetName ();

			break;
		case eGameMode.Start:
			// server all ready message.
			UIManager.instance.SetUI_Start ();
			break;

		case eGameMode.Talking:
			UIManager.instance.SetUI_Talking ();
			break;
		case eGameMode.Batting:
			UIManager.instance.SetUI_Battle ();
			break;
		case eGameMode.Result:
			UIManager.instance.SetUI_Result ();
			break;
		case eGameMode.End:
			UIManager.instance.SetUI_End ();
			break;
		}

		UIManager.instance.SetText (next.ToString ());

		_gameMode = next;
	}

	public bool IsAllNicknameSetting()
	{
		for(int f=0; f<listNetClient.Count; ++f) {
			if (!listNetClient [f].bNicknameConfirm)
				return false;
		}

		return true;
	}

	public void SetSyncData(NetworkInstanceId netid)
	{
		for (int f = 0; f < listNetClient.Count; f++) {
			if (listNetClient [f].netId != netid) {
			}
		}
	}
}
