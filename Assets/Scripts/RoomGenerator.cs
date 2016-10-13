using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour 
{	
	public PhysicsMaterial2D mat;

	// Use this for initialization
	void Start () 
	{
		
	}

	public void GenerateRoom(Tilemap tm)
	{
		float size = tm.Tiles[0].sprite.bounds.size.x;

		Node[,] PathFindingData = new Node[tm.Width,tm.Height];

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
				{
					g.AddComponent<BoxCollider2D>();
					g.GetComponent<BoxCollider2D>().sharedMaterial = mat;
				}

				Node node = new Node(td.bPhysics, g.transform.localPosition, x, y);
				PathFindingData[x,y] = node;
			}
		}

		AINetwork.Instance.SetMapData(PathFindingData, size);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
