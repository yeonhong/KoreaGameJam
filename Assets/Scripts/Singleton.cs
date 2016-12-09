using UnityEngine;
using System.Collections.Generic;

namespace GameJam
{
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
}