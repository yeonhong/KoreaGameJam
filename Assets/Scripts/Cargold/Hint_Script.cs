using UnityEngine;
using System.Collections;

public class Hint_Script : MonoBehaviour
{
	public string[] hintArr;

	public string GetHint_Func()
	{
		return hintArr[Random.Range(0, hintArr.Length)];
	}
}
