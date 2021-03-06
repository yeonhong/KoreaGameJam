﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Script : GameManager_Script
{
	public Animation slideAni = null;
	public bool isSlideClear = false;
	public Text[] hintText = null;
	public Text goldAmount = null;
	public Text goldAmount_Total = null;

	public int coinNum = 0;
	public int[] investCoin;
	public GameObject investedCheckObj = null;
	public bool isPlayerInvested = false;

	public GameObject screenChange_Invest;
	public GameObject screenChange_Main;
	public bool isSlidingOn = false;

	public int lockHintID = 0;

	public GameObject[] investCount_A;
	public GameObject[] investCount_B;
	public GameObject[] investCount_C;

	public GameObject[,] investCount;
	public CompanyFluctuateData_Script[] companyFlucData;

	public GameObject holdScreenObj = null;
	public GameObject StartButtonObj = null;

	void Awake()
	{
		slideAni = this.GetComponent<Animation>();

		investCount = new GameObject[3,5];

		for(int i=0; 5>i; i++)
		{
			investCount[0,i] = investCount_A[i];
			investCount[1,i] = investCount_B[i];
			investCount[2,i] = investCount_C[i];
		}

		goldAmount.text = "0";
		goldAmount_Total.text = "0";
	}

	public void Init_Func()
	{
		// 자신의 접속 여부를 알리며, 해당 함수로부터 ID를 리턴 받음

		base.Init_Func(PlayerType.Player);

		StartButtonObj.SetActive(false);

		transform.parent.GetComponent<NetClient>().SendNickName ("init");
	}

	protected override void ReadyFirstState_Func ()
	{
		base.ReadyFirstState_Func ();

		for(int i=0; 3>i; i++)
		{
			companyFlucData[i].Init_Func();
		}
	}

	protected override void NextDay_Func()
	{
		base.NextDay_Func();

		coinNum = 5;
		isPlayerInvested = false;
		investedCheckObj.SetActive(false);
		investCoin = new int[Common_Data.Instance().GetCompanyNum_Func()];
		slideAni.Play("HintUnlock");
		lockHintID = -1;
		isSlideClear = true;
		holdScreenObj.SetActive(false);

		for(int i=0; 3>i; i++)
		{
			for(int j=0; 5>j; j++)
			{
				investCount[i, j].SetActive(false);
			}
		}
	}

	protected override void MarketState_Func ()
	{
		base.MarketState_Func ();

		transform.parent.GetComponent<NetClient> ().ReqGetHint ();
	}

	public void RecvHint(string hint1, string hint2)
	{
		hintText[0].text = hint1;
		hintText[1].text = hint2;

		if( isSlidingOn == true )
		{
			Slide_Func();
		}
	}

	public void InvestIncrease_Func(int id)
	{
		if( isPlayerInvested == true ) return;

		if( coinNum > 0 )
		{
			coinNum--;
			investCoin[id]++;
			goldAmount.text = ((5-coinNum) * Common_Data.Instance().coinMeasure).ToString();

			Debug.Log ("InvestIncrease_Func : " + coinNum);

			for(int i=0; 5>i; i++)
			{
				if( investCount[id, i].activeInHierarchy == false )
				{
					investCount[id, i].SetActive(true);
					break;
				}
			}
		}
	}

	public void InvestDecrease_Func(int id)
	{
		if( isPlayerInvested == true ) return;

		if (investCoin [id] > 0)
		{
			coinNum++;
			investCoin [id]--;
			goldAmount.text = ((5-coinNum) * Common_Data.Instance().coinMeasure).ToString();

			Debug.Log ("InvestDecrease_Func : " + coinNum);

			for(int i=0; 5>i; i++)
			{
				if( investCount[id, i].activeInHierarchy == true )
				{
					investCount[id, i].SetActive(false);
					break;
				}
			}
		}
	}

	public void InvestComfirm_Func()
	{
		// Called : Dealer_Script . Invest_Func()

		if( isPlayerInvested == false )
		{
			isPlayerInvested = true;
			investedCheckObj.SetActive(true);

			transform.parent.GetComponent<NetClient> ().ReqInvestCoin (investCoin);
		}
	}

	public void LockHint_Func(int _id)
	{
		if( lockHintID == -1 || lockHintID == _id )
		{
			lockHintID = _id;

			if( _id == 0 )
			{
				slideAni["LockHint_0"].speed = 1;
				slideAni.Play("LockHint_0");
			}
			else if( _id == 1 )
			{
				slideAni["LockHint_1"].speed = 1;
				slideAni.Play("LockHint_1");
			}
		}
	}

	public void UnlockHint_Func(int _id)
	{
		if( lockHintID == _id )
		{
			if( _id == 0 )
			{
				slideAni["LockHint_0"].time = slideAni["LockHint_0"].length;
				slideAni["LockHint_0"].speed = -1;
				slideAni.Play("LockHint_0");
			}
			else if( _id == 1 )
			{
				slideAni["LockHint_1"].time = slideAni["LockHint_1"].length;
				slideAni["LockHint_1"].speed = -1;
				slideAni.Play("LockHint_1");
			}
		}
	}

	public void Slide_Func()
	{
		if( isSlideClear == true )
		{
			isSlideClear = false;

			if( isSlidingOn == false )
			{
				isSlidingOn = true;

				slideAni.Play("Invest");

				screenChange_Main.SetActive(true);
				screenChange_Invest.SetActive(false);
			}
			else if( isSlidingOn == true )
			{
				isSlidingOn = false;

				slideAni.Play("Main");

				screenChange_Main.SetActive(false);
				screenChange_Invest.SetActive(true);
			}
		}
	}

	void SlideClear_Func()
	{
		isSlideClear = true;
	}
		
	public void ReciveStockInfo(int company1, int company2, int company3) 
	{
		int[] company = new int[]{company1, company2, company3};

		for(int i=0; 3>i; i++)
		{
			if( company[i] > 0f )
			{
				companyFlucData[i].SetData_Func(FluctuateDataState.Up, company[i]);
			}
			else if( company[i] == 0f )
			{
				companyFlucData[i].SetData_Func(FluctuateDataState.Steady, company[i]);
			}
			else if( 0f > company[i] )
			{
				companyFlucData[i].SetData_Func(FluctuateDataState.Down, company[i]);
			}
		}
	}

	protected override void FluctuateState_Func ()
	{
		base.FluctuateState_Func ();

		holdScreenObj.SetActive(true);
	}

	public void RecvScore(int score)
	{
		goldAmount_Total.text = score.ToString();
	}
}
