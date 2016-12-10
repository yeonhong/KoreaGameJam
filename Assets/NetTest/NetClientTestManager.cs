using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using GameJam;

namespace NetTestSpace {

public class NetClientTestManager : Singleton<NetClientTestManager> 
{
	public Text uiText;
	public Text uiSyncVar;
	private string logText;

	public void SetSyncVarText(int value)
	{
		if (uiSyncVar != null) {
			uiSyncVar.text = value.ToString ();
		}
	}

	public void AddLogString(string log)
	{
		logText += log;
		logText += "\n";
		if (uiText != null)
			uiText.text = logText;
	}

	public void InitLogString()
	{
		logText = "";
		if (uiText != null)
			uiText.text = "";
	}

}
}