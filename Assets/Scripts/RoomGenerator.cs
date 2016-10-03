using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour 
{
	List<GameObject> Tiles = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		/*
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
				g.GetComponent<SpriteRenderer>().sortingLayerName = "Floor";
			}
		}
		*/
	}

	public void GenerateRoom(Tilemap tm)
	{
		float size = tm.Tiles[0].sprite.bounds.size.x;

		for(int x = 0; x < tm.Width; x++)
		{
			for(int y = 0; y < tm.Height; y++)
			{
				int tile = tm.MapData[x,y];
				Tilemap.TileData td = tm.Tiles[tile];

				GameObject g = new GameObject();
				g.transform.parent = transform;
				g.transform.localPosition = new Vector3(x * size, tm.Height * size - (y+1) * size, 0);
				g.AddComponent<SpriteRenderer>();
				g.GetComponent<SpriteRenderer>().sprite = td.sprite;
				g.GetComponent<SpriteRenderer>().sortingLayerName = "Floor";

				if(td.bPhysics)
					g.AddComponent<BoxCollider2D>();

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
