﻿using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {

	public static DebugManager Instance;

	public Transform Character;

	public GameObject SpawnObject;

	// Use this for initialization
	void Awake () 
	{
		if(Instance != null)
			Instance = null;

		Instance = this;        
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleInput();	
	}

	void HandleInput ()
	{
        if (Input.GetKeyDown(KeyCode.P))
		{
            NetworkHelper.Instance.SpawnDebugObject();
		}

        if(Input.GetKeyDown(KeyCode.T))
        {
            NetworkHelper.Instance.SpawnObject(Character.position, "Scrap");
        }
	}

	void SpawnNewObject()
	{
		GameMapData.Instance.PlaceObject("SmallLamp", Character.position);
	}
}
