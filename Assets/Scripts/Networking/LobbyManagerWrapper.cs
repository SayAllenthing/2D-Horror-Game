using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class LobbyManagerWrapper : NetworkLobbyManager
{
	public Dictionary<int, LobbyPlayer> LobbyPlayers = new Dictionary<int, LobbyPlayer>();

	void PreStartConnect()
	{
		NetworkServer.SetAllClientsNotReady();
		ClientScene.DestroyAllClientObjects();
	}

	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	//UI Button Commands
	public void OnHostClicked()
	{
		PreStartConnect();
		this.StartHost();
	}

	public void OnJoinClicked()
	{
		PreStartConnect();
		NetworkManager.singleton.networkAddress = "localhost";
		this.StartClient();
	}

	//Events
	public override void OnStartHost()
	{
		base.OnStartHost();

		Debug.Log("Host started");
	}

	public override void OnStartServer()
	{
		Debug.Log("On Start Server");
		base.OnStartServer();
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
		//conn.SetChannelOption(0, ChannelOption.MaxPendingBuffers, 24);
	}

	public virtual void OnClientEnterLobby()
	{
		Debug.Log("Client Entered");
	}

	//Server Callbacks
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer (conn, playerControllerId);
	}

	public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;

		//LobbyPlayers.Add(conn.connectionId, obj.GetComponent<LobbyPlayer>());

		return obj;
	}

	public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player = (GameObject) Instantiate(gamePlayerPrefab, Vector3.zero, Quaternion.identity);

		//LobbyPlayer lp = LobbyPlayers[conn.connectionId];
		return player; 
	}

	public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
	{
		//gamePlayer.GetComponent<PlayerSync>().SetLobbyPlayer(lobbyPlayer.GetComponent<LobbyPlayer>());

		return true;
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		Debug.Log("Removing Player - Connection: " + conn.connectionId);
		//LobbyPlayers.Remove(conn.connectionId);
	}
}
