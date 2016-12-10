using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
	None = -1,
	Ready_First = 0,
	Ready,
	Market,
	Invest,
	Fluctuate,
	Result,
	Result_Total,
	GameEnd,
}

public enum PlayerType
{
	None = -1,
	Dealer = 0,
	Player,
}

public class GameManager_Script : MonoBehaviour
{
	public PlayerType playerType = PlayerType.None;
	public GameState gameState = GameState.None;

	public float timeValue = 0f;
	public Text timeText = null;
	public int today = 0;
	public Text dayText = null;

	public void Init_Func(PlayerType _playerType)
	{
		// 현재 이 프로그램이 딜러인지, 플레이어인지.

		playerType = _playerType;
	}

	protected virtual void ReadyFirstState_Func()
	{
		gameState = GameState.Ready_First;

		NextDay_Func();
	}

	protected virtual void ReadyState_Func()
	{
		gameState = GameState.Ready;

		NextDay_Func();
	}

	protected virtual void NextDay_Func()
	{
		today++;
		dayText.text = today.ToString();
	}

	protected virtual void MarketState_Func()
	{
		gameState = GameState.Market;

		StartCoroutine(TimeCount_Cor());
	}

	IEnumerator TimeCount_Cor()
	{
		float _timeValue_InterpolateValue = Time.time;

		while(gameState == GameState.Market)
		{
			timeValue = Time.time - _timeValue_InterpolateValue;
			timeText.text = ((int)timeValue).ToString();
			yield return null;
		}
	}

	protected virtual void InvestState_Func()
	{
		gameState = GameState.Invest;
	}

	protected virtual void FluctuateState_Func()
	{
		gameState = GameState.Fluctuate;
	}

	protected virtual void ResultState_Func()
	{
		gameState = GameState.Result;
	}

	protected virtual void ResultTotalState_Func()
	{
		gameState = GameState.Result_Total;
	}

	public virtual void SetState_Func(GameState _setState)
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
				ResultTotalState_Func();
			}
			else
			{
				ResultState_Func();
			}
			break;
		case GameState.GameEnd:
			GameEnd_Func();
			break;
		}
	}

	void GameEnd_Func()
	{

	}
}
