using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum FluctuateDataState
{
	None = -1,
	Up = 0,
	Steady,
	Down,
}

public class CompanyFluctuateData_Script : MonoBehaviour
{
	public Text valueText;
	public GameObject[] fluctuateStateObj;

	public void Init_Func()
	{
		for(int i=0; fluctuateStateObj.Length>i; i++)
		{
			fluctuateStateObj[i].SetActive(false);
		}

		valueText.text = "?";
	}

	public void SetData_Func(FluctuateDataState _state, int _value)
	{
		for(int i=0; fluctuateStateObj.Length>i; i++)
		{
			fluctuateStateObj[i].SetActive(false);
		}

		fluctuateStateObj[(int)_state].SetActive(true);
		valueText.text = _value.ToString() + "%";
	}
}
