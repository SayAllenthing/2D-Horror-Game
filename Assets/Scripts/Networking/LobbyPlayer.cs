using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer
{
	public override void OnClientEnterLobby()
	{
		base.OnClientEnterLobby();

		Debug.Log("OnClientEnterLobby");

		if (isLocalPlayer)
		{
			SetupLocalPlayer();
		}
		else
		{
			SetupOtherPlayer();
		}
	}

	public override void OnStartLocalPlayer()
	{
		Debug.Log("OnStartLocalPlayer " + Network.isClient + " " + Network.isServer);

		SetupLocalPlayer();
	}

	void SetupLocalPlayer()
	{		
		Debug.Log("Setting up local Player");

		if(!LobbyManagerWrapper.Instance.bIsHost)
		{
			//Ask for the seed
			Debug.Log("Asking for seed");
			CmdAskForSeed();
		}
	}

	void SetupOtherPlayer()
	{
		
	}

	[Command]
	void CmdAskForSeed()
	{
		Debug.Log("I want dis " + Network.isServer);
		RpcGetSeed(ApartmentGenerator.Instance.Seed);
	}

	[ClientRpc]
	void RpcGetSeed(int i)
	{
		if(isLocalPlayer)
		{
			Debug.Log("Seed? " + i);
			SetSeed(i);
		}
	}

	void SetSeed(int i)
	{
		ApartmentGenerator.Instance.SetSeed(i);
	}
}
