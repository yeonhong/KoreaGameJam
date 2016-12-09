using UnityEngine;
using System.Collections;

namespace GameJam
{
	public class Player : Unit
	{
		protected override void Awake()
		{
			base.Awake ();

			Debug.Log ("Player Awake");
		}

		void Update()
		{
			float horizon = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");

//			if (horizon != 0f)
//				vertical = 0f;

			Vector3 vMov = new Vector3 (horizon, vertical, 0f);

			if( vMov != Vector3.zero )
				t.Translate (moveSpeed * vMov * Time.deltaTime);

			if (Input.GetMouseButtonUp (0)) {
				Debug.Log (Input.mousePosition);
				if (Camera.main != null) {
					Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Debug.Log (mouseWorldPoint);

					t.position = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, 0f);
				}
			}

			if (Input.touchCount > 0)
			{
				//Store the first touch detected.
				Touch myTouch = Input.touches[0];

				//Check if the phase of that touch equals Began
				if (myTouch.phase == TouchPhase.Ended) {
					Debug.Log (myTouch.position);

					if (Camera.main != null) {
						Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (myTouch.position);
						Debug.Log (mouseWorldPoint);
					}
				}
			}
		}
	}
}
