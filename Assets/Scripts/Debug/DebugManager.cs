using UnityEngine;
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
		if(Input.GetKeyDown(KeyCode.P))
		{
			SpawnNewObject();
		}
	}

	void SpawnNewObject()
	{
		GameMapData.Instance.PlaceObject("SmallLamp", Character.position);
	}
}
