using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApartmentGenerator : MonoBehaviour 
{	
	public PhysicsMaterial2D mat;

	int[,] MapData;

	int ApartmentWidth = 20;
	int ApartmentHeight = 20;

	int Apartments = 5;

	List<Room> Rooms;

	// Use this for initialization
	void Start () 
	{
		//GenerateRoomReal();
	}

	public void GenerateFloor(Tilemap tm)
	{
		GenerateApartments();
		GenerateHallway();

		float size = tm.Tiles[0].sprite.bounds.size.x;

		int MapWidth = MapData.GetLength(0);
		int MapHeight = MapData.GetLength(1);

		Node[,] PathFindingData = new Node[MapWidth,MapHeight];

		for(int x = 0; x < MapWidth; x++)
		{
			for(int y = 0; y < MapHeight; y++)
			{
				int tile = MapData[x,y];
				Tilemap.TileData td = tm.Tiles[tile];

				GameObject g = new GameObject();
				g.transform.parent = transform;
				g.transform.localPosition = new Vector3(x * size, MapHeight * size - (y+1) * size, 0);
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

	public void GenerateApartments()
	{
		MapData = new int[ApartmentWidth * Apartments, ApartmentHeight + 4];

		for(int i = 0; i < Apartments; i++)
		{
			Apartment a = new Apartment(ApartmentWidth, ApartmentHeight, i * ApartmentWidth, 0);
			MapData = a.CreateApartment(MapData);
		}
	}

	void GenerateHallway()
	{
		int MapWidth = MapData.GetLength(0);
		int MapHeight = MapData.GetLength(1);

		for(int x = 0; x < MapWidth; x++)
		{
			for( int y = 0; y < MapHeight; y++)
			{
				if(MapData[x,y] == 0)
				{
					if(x == 0 || x == MapWidth-1 || y == 0 || y == MapHeight-1)
					{
						MapData[x,y] = 7;
					}
					else
					{
						MapData[x,y] = 81;
					}
						
				}
			}
		}
	}
}
