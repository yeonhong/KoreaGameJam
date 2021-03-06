﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public struct CompanyDataStr
{
	public int id;
	public string name;
	public Sprite Image;
	public float fluctuateValue_RecentDay;
	public float fluctuateValue_Total;

	public float[] fluctuateValue;

	public void Init_Func()
	{
		fluctuateValue = new float[Common_Data.Instance().dayNum];

		fluctuateValue_RecentDay = 0f;
		fluctuateValue_Total = 0f;
	}
}

public class Common_Data : MonoBehaviour
{
	private static Common_Data _instance = null;
	public static Common_Data Instance()
	{
		return _instance;
	}

	void Awake()
	{
		if( _instance == null ) _instance = this;
	}
		
	public int GetCompanyNum_Func() {return CompanyData.Length;}

	public int dayNum = 0;
	public int playerNum = 0;
	public float marketFluctuateValue_Min = 0;
	public float marketFluctuateValue_Max = 0;
	public int coinMeasure = 0;
	public CompanyDataStr[] CompanyData;
}
