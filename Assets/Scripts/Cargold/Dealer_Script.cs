using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dealer_Script : GameManager_Script
{
	public bool isReadyAllPlayer = false;
	public int investCount = 0;

	public class cPlayerInvestData
	{
		public int[] playerInvestAmount  = new int[3];
		public int[] playerGoldAmount = new int[3];
		public int playerGoldAmount_Total;
	}

	public Dictionary<NetworkInstanceId, cPlayerInvestData> dicInvestData = new Dictionary<NetworkInstanceId, cPlayerInvestData>();

	public GameObject investClearObj = null;

	public CompanyFluctuateData_Script[] companyFlucData;

//	public Text[,] playerInvestAmountText;
//	public Text[,] playerGoldAmountText;
	public Text winnerText = null;

	public CoinIconGrid_Script[] coinGrid_player0;
	public CoinIconGrid_Script[] coinGrid_player1;
	public CoinIconGrid_Script[] coinGrid_player2;
	public CoinIconGrid_Script[] coinGrid_player3;
	public CoinIconGrid_Script[,] coinGrid;

	public Transform fluctuateTrf = null;
	public Transform resultTrf = null;
	public Vector3 showPos;
	public Vector3 hidePos;

	void Awake()
	{
		coinGrid = new CoinIconGrid_Script[4,3];
		for(int i=0; 3>i; i++)
		{
			coinGrid[0,i] = coinGrid_player0[i];
			coinGrid[1,i] = coinGrid_player1[i];
			coinGrid[2,i] = coinGrid_player2[i];
			coinGrid[3,i] = coinGrid_player3[i];
		}
	}

	public void StartGame_Func()
	{
		// 호스트가 게임 시작 버튼을 눌러서, 이 함수를 호출

		base.Init_Func(PlayerType.Dealer);

//		if( isReadyAllPlayer == false )
//		{
//			Debug.Log("Host : 아직 4명의 플레이어가 준비되지 않았습니다.");
////			Debug.Log("현재 접속 인원 : " + );
//		}
		if (true == GameManager.instance.IsAllNicknameSetting())
		{
			SetState_Func(GameState.Ready_First);
		}
	}

	protected override void ReadyFirstState_Func ()                                                    
	{
		base.ReadyFirstState_Func();

		for(int companyID=0; 3>companyID; companyID++)
		{
			companyFlucData[companyID].SetData_Func(FluctuateDataState.Steady, 0);
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

			Common_Data.Instance().CompanyData[i].fluctuateValue[today-1] = randValue;
			Common_Data.Instance().CompanyData[i].fluctuateValue_Total += randValue;

			PlayerPrefs.SetInt ("Change" + i,(int)randValue);
		}

		transform.parent.GetComponent<NetClient> ().CmdGenerateHint ();
	}

	//!!!!!!!!!!
	public void Invest_Func(NetworkInstanceId pid, int[] _investCoinNum)
	{
//		int _playerID = 0; //!!!

		Debug.Log ("Invest_Func " + pid);

		if (false == dicInvestData.ContainsKey (pid)) {
			dicInvestData.Add (pid, new cPlayerInvestData ());
		}

		for(int companyID=0; 3>companyID; companyID++)
		{
			dicInvestData[pid].playerInvestAmount[companyID] = _investCoinNum[companyID];
		}
			
		investCount++;

		if( investCount >= GameManager.instance.listNetClient.Count - 1 ) {
			Debug.Log ("invest End!");
			investClearObj.SetActive(true);
		}
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
		fluctuateTrf.position = showPos;
		resultTrf.position = hidePos;

		for(int companyID=0; 3>companyID; companyID++)
		{
			if( Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1] > 0f )
			{
				companyFlucData[companyID].SetData_Func(FluctuateDataState.Up, (int)Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1]);
			}
			else if( Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1] == 0f )
			{
				companyFlucData[companyID].SetData_Func(FluctuateDataState.Steady, (int)Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1]);
			}
			else if( 0f > Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1] )
			{
				companyFlucData[companyID].SetData_Func(FluctuateDataState.Down, (int)Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1]);
			}
		}

		transform.parent.GetComponent<NetClient> ().RpcStockResultInYesterday (
			(int)Common_Data.Instance ().CompanyData [0].fluctuateValue [today - 1],
			(int)Common_Data.Instance ().CompanyData [1].fluctuateValue [today - 1],
			(int)Common_Data.Instance ().CompanyData [2].fluctuateValue [today - 1]);

		yield return null;
	}

	public void FluctuateClear_Func()
	{
		SetState_Func(GameState.Result);
	}
		
	protected override void ResultState_Func ()
	{
		base.ResultState_Func ();

		fluctuateTrf.position = hidePos;
		resultTrf.position = showPos;

		// 결과창에 코인 아이콘 개수 0개로 초기화
		for(int playerID=0; dicInvestData.Count>playerID; playerID++)
		{
			for(int companyID=0; 3>companyID; companyID++)
			{
				coinGrid[playerID, companyID].SetCoint_Func(0);
			}
		}

		#region 같은 투자 코인 검색
		NetworkInstanceId[] arrids = new NetworkInstanceId[dicInvestData.Count];
		int i = 0;
		foreach (KeyValuePair<NetworkInstanceId, cPlayerInvestData> target in dicInvestData)
		{
			arrids[i++] = target.Key;
		}

		// 각 회사 별로...
		for(int companyID=0; 3>companyID; companyID++)
		{

			// 플레이어들 중...
			for(int playerID=0; playerID<arrids.Length; ++playerID)
			{

				// 1~5의 투자횟수가...
				int sameInvestCheck_PlayerID = -1;
				for(int sameValue = 1; 5>=sameValue; sameValue++)
				{

					// 동일하다면은...
					if( dicInvestData[arrids[playerID]].playerInvestAmount[companyID] == sameValue )
					{

						// sameInvestCheck_PlayerID에 해당 플레이어 ID 기록
						// 만약 이미 기록되어 있는데 또 발생한다면...
						if( sameInvestCheck_PlayerID == -1 )
						{
							sameInvestCheck_PlayerID = playerID;
						}
						else
						{
							// 무효 처리!

							dicInvestData[arrids[playerID]].playerInvestAmount[companyID] = 0;
							dicInvestData[arrids[sameInvestCheck_PlayerID]].playerInvestAmount[companyID] = 0;
						}
					}
				}
			}
		}
		#endregion

		// 결과창에 코인 아이콘 개수 초기화
		// 돈 증가
		for(int playerID=0; dicInvestData.Count>playerID; playerID++)
		{
			for(int companyID=0; 3>companyID; companyID++)
			{
				int investCount = dicInvestData[arrids[playerID]].playerInvestAmount[companyID];

				coinGrid[playerID, companyID].SetCoint_Func(investCount);
				dicInvestData[arrids[playerID]].playerGoldAmount[companyID] = investCount *
					Common_Data.Instance().coinMeasure;
			}
		}

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

		winnerText.text = "승자는 누구누구입니다!";
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


