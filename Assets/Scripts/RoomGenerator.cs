using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour {

	public int SizeX = 10;
	public int SizeY = 10;

	public Sprite Tile;

	List<GameObject> Tiles = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		float size = Tile.bounds.size.x;

		for(int x = 0; x < SizeX; x++)
		{
			for(int y = 0; y < SizeY; y++)
			{
				GameObject g = new GameObject();
				g.transform.parent = transform;
				g.transform.localPosition = new Vector3(x * size, y * size, 0);
				g.AddComponent<SpriteRenderer>();
				g.GetComponent<SpriteRenderer>().sprite = Tile;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
