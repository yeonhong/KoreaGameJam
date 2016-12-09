using UnityEngine;
using System.Collections;

namespace GameJam
{
	using UnityEngine.SceneManagement;

	public class StateManager : Singleton<StateManager>
	{
		public void StartMainGame()
		{
			SceneManager.LoadScene ("Main", LoadSceneMode.Single);
		}
	}
}
