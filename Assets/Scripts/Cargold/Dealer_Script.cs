using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dealer_Script : GameManager_Script
{
	public Hint_Script HintClass = null;
	public bool isReadyAllPlayer = false;

	public int investCount = 0;

	public class cPlayerInvestData
	{
		public float[] playerInvestAmount  = new float[3];
		public float[] playerGoldAmount = new float[3];
		public float playerGoldAmount_Total;
	}

	public Dictionary<NetworkInstanceId, cPlayerInvestData> dicInvestData = new Dictionary<NetworkInstanceId, cPlayerInvestData>();

	public GameObject investClearObj = null;

	public Text[] companyNameText;
	public Text[] companyGrowValueText;

//	public Text[,] playerInvestAmountText;
//	public Text[,] playerGoldAmountText;
	public Text winnerText = null;

	public void StartGame_Func()
	{
		// 호스트가 게임 시작 버튼을 눌러서, 이 함수를 호출

		base.Init_Func(PlayerType.Dealer);

//		if( isReadyAllPlayer == false )
//		{
//			Debug.Log("Host : 아직 4명의 플레이어가 준비되지 않았습니다.");
////			Debug.Log("현재 접속 인원 : " + );
//		}
		if (true == GameManager.instance.IsAllNicknameSetting()) {
			SetState_Func(GameState.Ready_First);
		}
	}

	protected override void ReadyFirstState_Func ()                                                    
	{
		base.ReadyFirstState_Func();

		for(int i=0; 3>i; i++)
		{
			companyNameText[i].text = Common_Data.Instance().CompanyData[i].name;
		}

		SetState_Func(GameState.Market);
	}

	protected override void ReadyState_Func ()
	{
		base.ReadyState_Func ();

		SetState_Func(GameState.Market);
	}

	protected override void MarketState_Func ()
	{
		base.MarketState_Func ();

		MarketOpen_Func();
	}

	protected override void NextDay_Func()
	{
		base.NextDay_Func();

		investClearObj.SetActive(false);
		investCount = 0;
//		playerInvestAmount = new float[4,3];
//		playerGoldAmount = new float[4,3];
	}

	void MarketOpen_Func()
	{
		// Call : 

		// 회사의 주식 변동을 기록

		int _companyNum = Common_Data.Instance().GetCompanyNum_Func();
		float minValue = Common_Data.Instance().marketFluctuateValue_Min;
		float maxValue = Common_Data.Instance().marketFluctuateValue_Max;

		for(int i=0; _companyNum>i; i++)
		{
			float randValue = Random.Range(minValue, maxValue);

			Common_Data.Instance().CompanyData[i].fluctuateValue_RecentDay = randValue;
			Common_Data.Instance().CompanyData[i].fluctuateValue_Total += randValue;
		}
	}

	//!!!!!!!!!!
	public float[] Invest_Func(NetworkInstanceId pid, float[] _investCoinNum)
	{
//		int _playerID = 0; //!!!

		Debug.Log ("Invest_Func " + pid);

		if (false == dicInvestData.ContainsKey (pid)) {
			dicInvestData.Add (pid, new cPlayerInvestData ());
		}

		for(int companyID=0; 3>companyID; companyID++)
		{
			dicInvestData[pid].playerInvestAmount[companyID] = _investCoinNum[companyID];
			dicInvestData[pid].playerGoldAmount[companyID] = dicInvestData[pid].playerInvestAmount[companyID]
				* Common_Data.Instance().CompanyData[companyID].fluctuateValue_RecentDay;

//			playerInvestAmount[_playerID, companyID] = _investCoinNum[companyID];
//			playerGoldAmount[_playerID, companyID] += playerInvestAmount[_playerID, companyID]
//				* Common_Data.Instance().CompanyData[companyID].fluctuateValue_RecentDay;
		}

		float[] _returnValue = new float[Common_Data.Instance().GetCompanyNum_Func()];

//		for(int i=0; 3>i; i++) {
//			dicInvestData[pid].playerInvestAmount[i];
//		}

		investCount++;

		if( investCount >= GameManager.instance.listNetClient.Count - 1 ) {
			Debug.Log ("invest End!");
			investClearObj.SetActive(true);
		}

		return _returnValue;
	}

	public void InvestClear_Func()
	{
		// 투자 종료 버튼 클릭 시, 위 함수가 호출된다.

		SetState_Func(GameState.Fluctuate);
	}

	protected override void FluctuateState_Func ()
	{
		base.FluctuateState_Func ();

		StartCoroutine(Fluctuate_Cor());
	}


	IEnumerator Fluctuate_Cor()
	{
		for(int companyID=0; 3>companyID; companyID++)
		{
			companyGrowValueText[companyID].text = ((int)Common_Data.Instance().CompanyData[companyID].fluctuateValue_RecentDay).ToString();
		}

//		for(int playerID=0; 4>playerID; playerID++)
//		{
//			//Debug.Log("플레이어 " + playerID + "의 투자 코인 : " +
////			((int)playerInvestAmount[playerID, 0]).ToString() + ", " +
////			((int)playerInvestAmount[playerID, 1]).ToString() + ", " +
////			((int)playerInvestAmount[playerID, 2]).ToString() + ", ");
//		}
			
		yield return null;
	}

	public void FluctuateClear_Func()
	{
		SetState_Func(GameState.Result);
	}

	protected override void ResultState_Func ()
	{
		base.ResultState_Func ();

		foreach (KeyValuePair<NetworkInstanceId, cPlayerInvestData> target in dicInvestData) {
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [0];
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [1];
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [2];

			Debug.Log ("플레이어 " + target.Key + "의 재산 : " + target.Value.playerGoldAmount_Total);
		}

//		for(int playerID=0; 4>playerID; playerID++)
//		{
////			Debug.Log("플레이어 " + playerID + "의 재산 : " +
////				((int)playerGoldAmount[playerID, 0]).ToString() + ", " +
////				((int)playerGoldAmount[playerID, 1]).ToString() + ", " +
////				((int)playerGoldAmount[playerID, 2]).ToString() + ", ");
//
//			playerGoldAmount_Total[playerID] += playerGoldAmount[playerID, 0];
//			playerGoldAmount_Total[playerID] += playerGoldAmount[playerID, 1];
//			playerGoldAmount_Total[playerID] += 
//				playerGoldAmount[playerID, 2];
//		}
	}

	public void ResultClear_Func()
	{
		SetState_Func(GameState.Ready);
	}
		
	protected override void ResultTotalState_Func ()
	{
		base.ResultTotalState_Func ();

		foreach (KeyValuePair<NetworkInstanceId, cPlayerInvestData> target in dicInvestData) {
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [0];
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [1];
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [2];

			Debug.Log ("플레이어 " + target.Key + "의 재산 : " + target.Value.playerGoldAmount_Total);
		}

//		for(int playerID=0; 4>playerID; playerID++)
//		{
//			Debug.Log("플레이어 " + playerID + "의 재산 : " +
//				((int)playerGoldAmount[playerID, 0]).ToString() + ", " +
//				((int)playerGoldAmount[playerID, 1]).ToString() + ", " +
//				((int)playerGoldAmount[playerID, 2]).ToString() + ", ");
//
//			playerGoldAmount_Total[playerID] += playerGoldAmount[playerID, 0];
//			playerGoldAmount_Total[playerID] += playerGoldAmount[playerID, 1];
//			playerGoldAmount_Total[playerID] += playerGoldAmount[playerID, 2];
//		}

		winnerText.text = "승자는 누구누구입니다!";
	}

	public string GetHint_Func()
	{
		return HintClass.GetHint_Func();
	}

	public override void SetState_Func (GameState _setState)
	{
		base.SetState_Func (_setState);

//		for(int i=0; 4>i; i++)
//		{
//			PlayerClass[i].SetState_Func(_setState);
//		}

		transform.parent.GetComponent<NetClient>().CmdSetStatePlayer (_setState);
	}
}


