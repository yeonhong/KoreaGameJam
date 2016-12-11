using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> 
{
	// network module manage.
	public NetClient myNetClient = null;
	public NetManager myNetManager = null;

	// netClinet manage.
	public List<NetClient> listNetClient = new List<NetClient>();
	public Dictionary<NetworkInstanceId, NetClient> lookupNetClient = new Dictionary<NetworkInstanceId, NetClient>();

	public GameObject objDealer;
	public GameObject objPlayer;

	public List<string> listHint = new List<string>();

	protected override void Start() {
		base.Start ();
		myNetManager = NetManager.instance;
	}

	public void initGame()
	{
		listNetClient.Clear ();
		lookupNetClient.Clear ();
	}

	public bool IsAllNicknameSetting()
	{
		Debug.Log ("listNetClient.Count " + listNetClient.Count);

		if (listNetClient.Count == 1) // dealer...
			return false;

		for(int f=0; f<listNetClient.Count; ++f) {

			//Debug.Log ("listNetClient[" + f + "]" + listNetClient [f].tag + "," +listNetClient [f].bNicknameConfirm);

			if (listNetClient [f].transform.childCount == 0 && !listNetClient [f].bNicknameConfirm) {
				return false;
			}
		}

		return true;
	}

	public void SetDealerUI(GameObject target)
	{
		GameObject objUI = Instantiate (objDealer, target.transform) as GameObject;
		objUI.transform.localPosition = Vector3.zero;
		//objUI.transform.localPosition = new Vector3(0,342,0);
	}

	public void SetPlayerUI(GameObject target)
	{
		GameObject objUI = Instantiate (objPlayer, target.transform) as GameObject;
		objUI.transform.localPosition = Vector3.zero;
		//objUI.transform.localPosition = new Vector3(0,169,0);
	}

	public void Give_Hint()
	{
		Debug.Log ("Give_Hint message recive");

		listHint.Clear ();

		for (int f = 0; f < 8; ++f)
			listHint.Add (PlayerPrefs.GetString ("Hint" + f));

		for (int f = 0; f < listHint.Count; ++f) {
			Debug.Log (listHint [f]);
		}
			
	}
}
