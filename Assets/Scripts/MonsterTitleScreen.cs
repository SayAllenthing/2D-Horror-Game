using UnityEngine;
using System.Collections;

public class MonsterTitleScreen : MonoBehaviour {

	public AnimationCurve Curve;

	public float MoveTime = 2.0f;
	public float MoveInterval = 3.0f;
	public float Speed = 25.0f;

	float NextMoveTime = 5.0f;
	float CurrentMoveStart = 0.0f;
	float CurrentMoveEnd = 0.0f;

	bool bMoving = false;

	Vector3 BasePos;

	void Start()
	{
		BasePos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float t = Time.timeSinceLevelLoad;

		if(t > NextMoveTime && !bMoving)
		{
			CurrentMoveStart = t;
			CurrentMoveEnd = t + MoveTime;
			bMoving = true;
		}

		if(bMoving)
		{
			float delta = Mathf.Clamp01((MoveTime - (CurrentMoveEnd - t)) / MoveTime);

			transform.Translate(Vector3.left * Speed * Time.deltaTime);

			float curveY = BasePos.y + (Curve.Evaluate(delta) * 15);


			transform.localPosition = new Vector3(transform.localPosition.x, curveY, transform.localPosition.z);

			if(t >= CurrentMoveEnd)
			{
				bMoving = false;
				NextMoveTime = t + MoveInterval;
			}
		}

		if(transform.localPosition.x < -480)
		{
			transform.localPosition = BasePos;
		}
	}
}
