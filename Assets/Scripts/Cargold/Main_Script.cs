using UnityEngine;
using System.Collections;

public class Main_Script : MonoBehaviour
{
	public void Dealer_Func()
	{
		NetManager.instance.OnClickStartDealer ();
		this.gameObject.SetActive(false);
	}

	public void Player_Func()
	{
		NetManager.instance.OnClickStartPlayer ();
		this.gameObject.SetActive(false);
	}
}
