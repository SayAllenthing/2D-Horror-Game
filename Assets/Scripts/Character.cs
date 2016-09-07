using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Transform sprite;

	public Vector2 LastAngle = Vector2.right;

	bool bIsLocal = false;

	Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	public void InitLocalPlayer()
	{
		bIsLocal = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(bIsLocal)
			HandleInput();
	}

	void HandleInput()
	{
		Vector2 WantMove = Vector2.zero;

		WantMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		WantMove = WantMove.normalized;

		if(WantMove.magnitude > 0.1f)
		{		
			SetLookDirection(WantMove);
		}

		rigidbody.velocity = WantMove * 5;
	}

	public void SetLookDirection(Vector2 look)
	{
		LastAngle = look;
		SetDirection(look.x);
	}

	void SetDirection(float dir)
	{
		if(dir > 0)
			sprite.transform.localScale = new Vector3(1, 1, 1);
		else if(dir < 0)
			sprite.transform.localScale = new Vector3(-1, 1, 1);
	}
}
