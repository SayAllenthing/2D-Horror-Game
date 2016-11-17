using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

using System.Collections.Generic;

public class Tilemap : MonoBehaviour {

	private string jsonString;

	public struct TileData
	{
		public int index;
		public bool bPhysics;
		public Sprite sprite;
	}

	public TileData[] Tiles;
	public Texture2D SpriteMap;

	public int TileSize;

	public List<Sprite> list = new List<Sprite>();

	// Use this for initialization
	void Start () {
		LoadJSON("Test2.json");
		GetComponent<ApartmentGenerator>().SetTileMap(this);
	}

	void LoadJSON(string filename)
	{
		//string url = Application.dataPath + "\\" + filename;
		//jsonString = File.ReadAllText(url);

		//JsonData itemData = JsonMapper.ToObject(jsonString);

		//CreateTileData(itemData["tilesets"][0]);

		CreateFakeTileData();
	}

	void CreateTileData(JsonData data)
	{
		int TileCount = (int)data["tilecount"];

		Tiles = new TileData[TileCount];

		IDictionary properties = data["tileproperties"] as IDictionary;

		int mapWidth = (int)data["imagewidth"];
		int mapHeight = (int)data["imageheight"];

		for(int i = 0; i < TileCount; i++)
		{
			TileData td;
			td.index = i;

			td.bPhysics = properties.Contains(i.ToString());

			int unitsPerRow = mapWidth/32;
			int y = i/unitsPerRow * 32 + 32;
			int x;

			if(i < unitsPerRow)
				x = i * 32;
			else
				x = i % unitsPerRow * 32;
			

			td.sprite = Sprite.Create(SpriteMap, new Rect(x, mapHeight - y, 32,32), new Vector2(0.5f,0f), 32);

			list.Add(td.sprite);

			Tiles[i] = td;
		}
	}

	void CreateFakeTileData()
	{
		int TileCount = 90; //(int)data["tilecount"];

		Tiles = new TileData[TileCount];

		//IDictionary properties = data["tileproperties"] as IDictionary;

		int mapWidth = 256;//(int)data["imagewidth"];
		int mapHeight = 793;//(int)data["imageheight"];

		for(int i = 0; i < TileCount; i++)
		{
			TileData td;
			td.index = i;

			td.bPhysics = i == 7; //properties.Contains(i.ToString());

			int unitsPerRow = mapWidth/32;
			int y = i/unitsPerRow * 32 + 32;
			int x;

			if(i < unitsPerRow)
				x = i * 32;
			else
				x = i % unitsPerRow * 32;


			td.sprite = Sprite.Create(SpriteMap, new Rect(x, mapHeight - y, 32,32), new Vector2(0.5f,0f), 32);

			list.Add(td.sprite);

			Tiles[i] = td;
		}
	}
}
