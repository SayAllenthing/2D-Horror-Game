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
	public int[,] MapData;

	public Texture2D SpriteMap;

	public int Height;
	public int Width;
	public int TileSize;

	public List<Sprite> list = new List<Sprite>();

	// Use this for initialization
	void Start () {
		LoadJSON("Test.json");

		GetComponent<RoomGenerator>().GenerateRoom(this);
	}

	void LoadJSON(string filename)
	{
		string url = Application.dataPath + "\\" + filename;
		jsonString = File.ReadAllText(url);

		JsonData itemData = JsonMapper.ToObject(jsonString);

		Height = (int)itemData["height"];
		Width = (int)itemData["width"];

		CreateTileData(itemData["tilesets"][0]);
		CreateMap(itemData["layers"][0]["data"]);
	}

	void CreateTileData(JsonData data)
	{
		int TileCount = (int)data["tilecount"];

		Tiles = new TileData[TileCount];

		IDictionary properties = data["tileproperties"] as IDictionary;

		int mapWidth = (int)data["imagewidth"];
		int mapHeight = (int)data["imageheight"];
		//Debug.Log(data["tileproperties"][0]);

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

			//Debug.Log("Creating Sprite at " + "X:" + x + ". Y:" + y);

			td.sprite = Sprite.Create(SpriteMap, new Rect(x, mapHeight - y ,32,32), new Vector2(0.5f,0f), 32);

			list.Add(td.sprite);

			Tiles[i] = td;
		}


	}

	void CreateMap(JsonData data)
	{
		MapData = new int[Width,Height];

		for(int i = 0; i < data.Count; i++)
		{
			int y = i/Width;
			int x;
			if(i < Width)
				x = i;
			else
				x = i % Width;
			
			int num = (int)data[i] - 1;

			MapData[x,y] = num;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
