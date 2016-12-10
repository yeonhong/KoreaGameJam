using UnityEngine;
using System.Collections;

public class CoinIconGrid_Script : MonoBehaviour
{
	public GameObject[] coinObj;

	void Awake()
	{
		for(int i=0; 5>i; i++)
		{
			coinObj[i].SetActive(false);
		}
	}

	public void SetCoint_Func(int _num)
	{
		for(int i=0, activeNum=0; 5>i; i++)
		{
			if( _num > activeNum )
			{
				coinObj[i].SetActive(true);
				activeNum++;
			}
			else
			{
				coinObj[i].SetActive(false);
			}
		}
	}
}
