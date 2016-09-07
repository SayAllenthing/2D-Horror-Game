using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
	public GameObject MainCamera;
	public GameObject LightingCamera;

	public Character MyCharacter;
	Rigidbody2D rigidbody;

	[SyncVar]
	Vector2 SyncPos;
	[SyncVar]
	Vector2 SyncLook;


	public override void OnStartLocalPlayer()
	{
		Debug.Log("OnStartLocalPlayer");

		LightingCamera.SetActive(true);
		MainCamera.SetActive(true);

		MyCharacter.InitLocalPlayer();
	}

	public override void OnStartClient()
	{
		

		Debug.Log("OnStartClient");
	}


	// Use this for initialization
	void Start () 
	{
		if(!isLocalPlayer)
		{
			rigidbody = GetComponent<Rigidbody2D>();
			rigidbody.isKinematic = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(isLocalPlayer)
			SendPosition();

		if(!isLocalPlayer)
			LerpPosition();
	}

	//Position and direction
	void SendPosition()
	{
		CmdUpdatePosition(transform.position, MyCharacter.LastAngle);
	}

	[Command]
	void CmdUpdatePosition(Vector3 pos, Vector2 look)
	{
		SyncPos = pos;
		SyncLook = look;
	}

	void LerpPosition()
	{
		if(!isLocalPlayer)
		{
			transform.position = Vector3.Lerp(transform.position, SyncPos, 15 * Time.deltaTime);

			MyCharacter.SetLookDirection(SyncLook);

		}
	}

}
