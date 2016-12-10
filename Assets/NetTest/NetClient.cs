using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace NetTestSpace {

public class NetClient : NetworkBehaviour 
{
	//[SyncVar(hook = "OnChangeSyncVar")]
	[SyncVar]
	public int nSyncVar = 100;



	void Update()
	{
		if (isLocalPlayer) {
			//net test key bind.
			if (Input.GetKeyDown (KeyCode.Q)) {
				CmdDoSomething (); // client to server message.
			} else if (Input.GetKeyDown (KeyCode.E)) {
				RpcDoSomething (); // send to client message.
			} else if (Input.GetKeyDown (KeyCode.W)) {
				NetClientTestManager.instance.InitLogString ();
			}
		}
	}

	// [Command] 속성은 다음 함수가 클라이언트에 의해 호출되지만 서버에서 실행됨을 나타냅니다. 함수의 모든 인수는 명령과 함께 자동으로 서버로 전달됩니다. 
	// 명령은 로컬 플레이어 개체에서만 보낼 수 있습니다. 
	[Command]
	void CmdDoSomething()
	{
		Debug.Log ("CmdDoSomething " + isLocalPlayer);
		NetClientTestManager.instance.AddLogString ("CmdDoSomething " + isLocalPlayer);
	}

	//isLocalPlayer << is mine identifier.

	//ClientRpc 호출은 NetworkIdentity를 사용하여 서버의 생성 된 모든 개체에서 보낼 수 있습니다. 
	//이 함수는 서버에서 호출 되더라도 클라이언트에서 실행됩니다. ClientRpc는 Command의 반대입니다. 
	//명령은 클라이언트에서 호출되지만 서버에서 실행됩니다. ClientRpc는 서버에서 호출되지만 클라이언트에서 실행됩니다.
	[ClientRpc]
	void RpcDoSomething()
	{
		Debug.Log("RpcDoSomething : " + isLocalPlayer);
		NetClientTestManager.instance.AddLogString ("RpcDoSomething : " + isLocalPlayer);
	}
}
}
