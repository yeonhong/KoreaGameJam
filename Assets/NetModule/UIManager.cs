using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> 
{
	public Text screenText;
	public GameObject objSetNameBtn;
	public GameObject objStartBtn;

	public void SetText(string text)
	{
		if (screenText != null)
			screenText.text = text;
	}

	public void SetUI_SetName()
	{
		objSetNameBtn.SetActive (true);
	}

	public void SetUI_Start()
	{
		objSetNameBtn.SetActive (false);
		objStartBtn.SetActive (true);
	}

	public void SetUI_Talking()
	{
		objStartBtn.SetActive (false);
	}

	public void SetUI_Battle()
	{
	}

	public void SetUI_Result()
	{
	}

	public void SetUI_End()
	{
	}
		
	public void OnClickSetNameButton()
	{
		GameManager.instance.myNetClient.CmdSendNickName ("nick!", GameManager.instance.myNetClient.netId);
	}
}
