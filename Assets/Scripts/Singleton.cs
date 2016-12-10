using UnityEngine;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour
{ 
	public static T instance;

	protected virtual void Start()
	{
		if (instance == null) {
			instance = this.GetComponent<T>();
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}
	}
}