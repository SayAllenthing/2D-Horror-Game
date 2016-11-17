using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour 
{
	public static MainMenuManager Instance;


	public CanvasGroup TitleGroup;
	public CanvasGroup MenuGroup;
	public CanvasGroup HostGroup;
	public CanvasGroup JoinGroup;
	public CanvasGroup LobbyGroup;

	public Text TextMyIP;
	public Text Seed;

	string MyIP = "";
	public string Port = "7777";
	public string PlayerName = "Player";

	public enum eMenuState
	{
		TITLE,
		MENU,
		HOST,
		JOIN,
		LOBBY
	}

	public eMenuState State;
	CanvasGroup currentMenu;

	// Use this for initialization
	void Awake () 
	{
		if(Instance)
		{
			DestroyImmediate(Instance);
			Instance = null;
		}

		Instance = this;

		State = eMenuState.TITLE;
		currentMenu = TitleGroup;
	}
	
	void Update()
	{
		HandleInput();
	}

	void HandleInput()
	{
		if(State == eMenuState.TITLE)
		{
			if(Input.anyKeyDown)
			{
				LoadMenuState();

				if(MyIP == "")
					Network.Connect("www.allenadams.ca");
			}
		}
	}

	public void OnBackPressed()
	{
		if(State == eMenuState.HOST || State == eMenuState.JOIN)
		{
			LoadMenuState();
			TextMyIP.gameObject.SetActive(false);
		}
		else if(State == eMenuState.LOBBY)
		{
			LoadMenuState();
			LobbyManagerWrapper.Instance.OnBackPressed();
		}
	}

	public void OnHostClicked()
	{
		LoadHostState();

		//GetIP
		if(MyIP == "")
		{
			MyIP = Network.player.externalIP;
			Network.Disconnect();
		}
	}

	public void OnJoinClicked()
	{
		LoadJoinState();

		Network.Disconnect();

	}

	public void OnLoadLobby()
	{
		LoadMenu(LobbyGroup);
		State = eMenuState.LOBBY;

		if(LobbyManagerWrapper.Instance.bIsHost)
			ShowIPText();
	}

	void LoadMenuState()
	{
		LoadMenu(MenuGroup);
		State = eMenuState.MENU;
	}

	void LoadHostState()
	{
		LoadMenu(HostGroup);
		State = eMenuState.HOST;
	}

	void LoadJoinState()
	{
		LoadMenu(JoinGroup);
		State = eMenuState.JOIN;
	}

	void LoadMenu(CanvasGroup cg)
	{
		currentMenu.alpha = 0;
		currentMenu.blocksRaycasts = false;
		currentMenu.interactable = false;

		currentMenu = cg;
		currentMenu.alpha = 1;
		currentMenu.interactable = true;
		currentMenu.blocksRaycasts = true;
	}

	void ShowIPText()
	{
		TextMyIP.text = "My IP: " + MyIP + ":" + Port;
		TextMyIP.gameObject.SetActive(true);
	}

	public void SetSeed(int seed)
	{
		Seed.text = "Seed - " + seed;
	}
}
