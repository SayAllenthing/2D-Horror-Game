using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Transform sprite;

	public Vector2 LastAngle = Vector2.right;

	bool bIsLocal = false;

	Rigidbody2D rigidbody;

	public float Speed = 250;

	public DynamicLight_Flashlight FlashLight;
	public MuzzleLight MuzzleLight;

	public NetworkPlayer NetPlayer;

	// Use this for initialization
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	public void InitLocalPlayer()
	{
		bIsLocal = true;

		DebugManager.Instance.Character = transform;

		GameMapData.Instance.AddActor(transform);

		transform.position = new Vector3(2,2,0);
		NetPlayer = GetComponent<NetworkPlayer>();
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

		if(WantMove.magnitude > 0.4f)
		{		
			WantMove = WantMove.normalized;
			SetLookDirection(WantMove);
		}

		rigidbody.velocity = WantMove * Speed * Time.deltaTime;

		if(Input.GetButtonDown("Fire1"))
		{
			HandleFire();
			NetPlayer.CmdFire();
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			FlashLight.renderer.enabled = !FlashLight.renderer.enabled;
		}
	}

	public void HandleFire()
	{
		MuzzleLight.Refresh();

		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero);

		for(int i = 0; i < hits.Length; i++)
		{
			if(hits[i].collider.gameObject.name == "Beast")
			{
				if(Fire(hits[i].collider.gameObject))
				{
					GameObject.Destroy(hits[i].collider.gameObject);
				}
			}
		}
	}

	bool Fire(GameObject target)
	{
		Vector3 diff = (target.transform.position - transform.position);
		float dot = Vector3.Dot(diff.normalized, LastAngle);

		float baseChance = dot * 50;
		float shot = Random.Range(0,100);

		if(baseChance > 0)
		{
			if(shot > (100 - baseChance))
			{
				Debug.Log("Killed " + baseChance + "%" + " " + shot);
				return true;
			}
		}

		Debug.Log("Miss " + baseChance + "%" + " " + shot);
		return false;
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
