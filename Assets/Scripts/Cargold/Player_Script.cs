using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Script : GameManager_Script
{
	public Dealer_Script DealerClass = null;

	public int coinNum = 0;
	public float[] investCoin;
	public Text goldAmount = null;
	public Text[] hintText = null;
	public bool isPlayerInvested = false;

	public Text[] companyNameText = null;

	public void Init_Func()
	{
		// 자신의 접속 여부를 알리며, 해당 함수로부터 ID를 리턴 받음

		base.Init_Func(PlayerType.Player);

		transform.parent.GetComponent<NetClient>().SendNickName ("init");

	}

	protected override void ReadyFirstState_Func ()
	{
		base.ReadyFirstState_Func ();

		for(int i=0; 3>i; i++)
		{
			companyNameText[i].text = Common_Data.Instance().CompanyData[i].name;
		}
	}

	protected override void NextDay_Func()
	{
		base.NextDay_Func();

		coinNum = 5;
		isPlayerInvested = false;
		investCoin = new float[Common_Data.Instance().GetCompanyNum_Func()];
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
	}

	public void InvestIncrease_Func(int id)
	{
		Debug.Log ("InvestIncrease_Func : " + coinNum);

		if( coinNum > 0 )
		{
			coinNum--;
			investCoin[id] += Common_Data.Instance().coinMeasure;
		}
	}

	public void InvestDecrease_Func(int id)
	{
		Debug.Log ("InvestDecrease_Func : " + coinNum);

		coinNum++;
		investCoin[id] -= Common_Data.Instance().coinMeasure;
	}

	public void SetGoldAmount_Func(float _value)
	{
		goldAmount.text = ((int)_value).ToString();
	}

	public void InvestComfirm_Func()
	{
		// Called : Dealer_Script . Invest_Func()

		if( isPlayerInvested == false )
		{
			isPlayerInvested = true;

			transform.parent.GetComponent<NetClient> ().ReqInvestCoin (investCoin);
			//investCoin = DealerClass.Invest_Func(transform.parent.GetComponent<NetClient>().netId, investCoin);
		}
	}
}
