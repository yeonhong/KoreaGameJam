using UnityEngine;
using System.Collections;

namespace GameJam
{
	public class Unit : MonoBehaviour 
	{
		protected Transform t;
		public float moveSpeed = 1f;

		protected virtual void Awake()
		{
			t = transform;

			Debug.Log ("Unit Awake (" + t.name + ")");
		}
	}
}
