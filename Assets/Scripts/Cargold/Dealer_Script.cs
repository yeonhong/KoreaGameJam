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

	public GameObject StartButtonObj = null;

	public GameObject logoObj = null;
	public GameObject resultTextGroupObj = null;
	public Text resultText = null;
	public Text[] resultValueText = null;
	public GameObject gameClearObj = null;
	public Text gameClearText = null;

	public Text[] playerNickname;

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

		StartButtonObj.transform.localPosition = showPos;
	}

	public void StartGame_Func()
	{
		// 호스트가 게임 시작 버튼을 눌러서, 이 함수를 호출

		base.Init_Func(PlayerType.Dealer);

		if (true == GameManager.instance.IsAllNicknameSetting())
		{
			Common_Data.Instance().CompanyData = new CompanyDataStr[3];
			for(int companyID=0; 3>companyID; companyID++)
			{
				Common_Data.Instance().CompanyData[companyID].Init_Func();

				companyFlucData[companyID].SetData_Func(FluctuateDataState.Steady, 0);
			}

			SetState_Func(GameState.Ready_First);
		}
	}
		
	protected override void ReadyFirstState_Func ()                                                    
	{
		base.ReadyFirstState_Func();

		StartCoroutine(Start_Cor());

		SetState_Func(GameState.Market);
	}

	IEnumerator Start_Cor()
	{
		AudioSource audioPlay = this.GetComponent<AudioSource>();
		audioPlay.Play();
		audioPlay.volume = 0f;

		float time = 1f;
		while(time>0f)
		{
			audioPlay.volume = (1f - time);

			StartButtonObj.GetComponent<Image>().color = new Color(1f, 1f, 1f, time);
			time -= 0.01f;
			yield return new WaitForFixedUpdate();
		}

		StartButtonObj.SetActive(false);
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

		fluctuateTrf.localPosition = showPos;
		resultTrf.localPosition = hidePos;

		investClearObj.SetActive(false);
		investCount = 0;

		logoObj.SetActive(true);
		resultTextGroupObj.SetActive(false);
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
			float randValue = Random.Range(minValue, maxValue) * 5;
			if( randValue >= 99f ) randValue = 99;

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
		fluctuateTrf.localPosition = showPos;
		resultTrf.localPosition = hidePos;
	

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
		
	protected override void ResultState_Func (bool isLast)
	{
		if( isLast == false ) base.ResultState_Func (false);
		else if( isLast == true ) base.ResultTotalState_Func();

		fluctuateTrf.localPosition = hidePos;
		resultTrf.localPosition = showPos;

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

		for(int playerID=0;arrids.Length>playerID;playerID++)
		{
			playerNickname[playerID].text = arrids[playerID] + " 님";
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
				dicInvestData[arrids[playerID]].playerGoldAmount[companyID] = (int)(investCount *
					Common_Data.Instance().coinMeasure *
					(1 + Common_Data.Instance().CompanyData[companyID].fluctuateValue[today-1]/100f)
				);
			}
		}

		foreach (KeyValuePair<NetworkInstanceId, cPlayerInvestData> target in dicInvestData)
		{
//			Debug.Log ("플레이어의 재산 : " + 

			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [0];
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [1];
			target.Value.playerGoldAmount_Total += target.Value.playerGoldAmount [2];
		}

		for(int playerID=0; dicInvestData.Count>playerID; playerID++)
		{
			Debug.Log ("플레이어 " + playerID.ToString() + "의 재산 : " + dicInvestData[arrids[playerID]].playerGoldAmount_Total.ToString());
			transform.parent.GetComponent<NetClient> ().RpcSendScore(arrids[playerID], dicInvestData[arrids[playerID]].playerGoldAmount_Total);
		}

		StartCoroutine( ResultText_Func(isLast));
	}

	IEnumerator ResultText_Func(bool isLast)
	{
		yield return new WaitForSeconds(3f);
		for(int playerID=0; dicInvestData.Count>playerID; playerID++)
		{
			for(int companyID=0; 3>companyID; companyID++)
			{
				coinGrid[playerID, companyID].SetCoint_Func(0);
			}
		}

		logoObj.SetActive(false);
		resultTextGroupObj.SetActive(true);

		if( isLast == true )
		{
			resultText.text = "최종 정산";
		}
		else
		{
			resultText.text = "중간 정산";
		}
		yield return new WaitForSeconds(0.5f);

		NetworkInstanceId[] arrids = new NetworkInstanceId[dicInvestData.Count];
		int i = 0;
		foreach (KeyValuePair<NetworkInstanceId, cPlayerInvestData> target in dicInvestData)
		{
			arrids[i++] = target.Key;
		}

		for(int playerID=0; dicInvestData.Count>playerID; playerID++)
		{
			resultValueText[playerID].text = (dicInvestData[arrids[playerID]].playerGoldAmount_Total * 10000).ToString();
			yield return new WaitForSeconds(0.25f);
		}

		if( isLast == true )
		{
			yield return new WaitForSeconds(0.5f);

			int victoryPlayerID = 0;
			for(int playerID=1; dicInvestData.Count>playerID; playerID++)
			{
				if( dicInvestData[arrids[playerID]].playerGoldAmount_Total > dicInvestData[arrids[victoryPlayerID]].playerGoldAmount_Total )
				{
					victoryPlayerID = playerID;
				}
			}

			gameClearObj.SetActive(true);

			gameClearText.text = "우승은 " + arrids[victoryPlayerID].ToString() + "번 플레이어";
		}
	}

	public void ResultClear_Func()
	{
		SetState_Func(GameState.Ready);
	}

	public void GameClear_Func()
	{
		Application.LoadLevel("Ingame");
	}

	public override void SetState_Func(GameState _setState)
	{
		// 위 함수는 오직 딜러(호스트)만이 호출함
		// 따라서 호출 시 모든 플레이어(클라이언트)들도 전원 호출시켜야 함.

		switch(_setState)
		{
		case GameState.None:

			break;
		case GameState.Ready_First:
			ReadyFirstState_Func();
			break;

		case GameState.Ready:
			ReadyState_Func();
			break;

		case GameState.Market:
			MarketState_Func();

			break;
		case GameState.Invest:
			InvestState_Func();

			break;
		case GameState.Fluctuate:
			FluctuateState_Func();
			break;
		case GameState.Result:
			Debug.Log("Test, today : " + today);
			if( today >= Common_Data.Instance().dayNum )
			{
				ResultState_Func(true);
			}
			else
			{
				ResultState_Func(false);
			}
			break;
		case GameState.GameEnd:
//			GameEnd_Func();
			break;
		}

		Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!");
		transform.parent.GetComponent<NetClient>().CmdSetStatePlayer (_setState);
	}
}