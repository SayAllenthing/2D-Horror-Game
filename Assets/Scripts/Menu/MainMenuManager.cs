using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
	public CanvasGroup TitleGroup;
	public CanvasGroup MenuGroup;

	public enum eMenuState
	{
		TITLE,
		MENU,
		LOBBY
	}

	public eMenuState State;
	CanvasGroup currentMenu;

	// Use this for initialization
	void Awake () 
	{
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
			}
		}
	}

	void LoadMenuState()
	{
		LoadMenu(MenuGroup);
		State = eMenuState.MENU;
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
}
