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

    [SyncVar]
    public int Sprite = 0;


	public override void OnStartLocalPlayer()
	{
		LightingCamera.SetActive(true);
		MainCamera.SetActive(true);

		MyCharacter.InitLocalPlayer();
	}

	public override void OnStartClient()
	{
        MyCharacter.SetSprite(LobbyManagerWrapper.Instance.CharacterSprites[Sprite]);
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
	void Update () 
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
			transform.position = Vector3.Lerp(transform.position, SyncPos, 5* Time.deltaTime);
			MyCharacter.SetLookDirection(Vector2.Lerp(MyCharacter.LastAngle, SyncLook, 5 * Time.deltaTime));
		}
	}

	[Command]
	public void CmdFire()
	{
		RpcFire();
	}

	[ClientRpc]
	public void RpcFire()
	{
		if(!isLocalPlayer)
			MyCharacter.HandleFire();
	}
}
