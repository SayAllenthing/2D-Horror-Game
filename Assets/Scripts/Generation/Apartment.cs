using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Apartment
{
	public int PosX;
	public int PosY;

	public int Width;
	public int Height;

	public List<Room> Rooms = new List<Room>();

	public int[,] MapData;

	public int Seed = -1;

	System.Random prng;

	public Apartment(int _width, int _height, int _x, int _y)
	{
		Width = _width;
		Height = _height;

		PosX = _x;
		PosY = _y;
	}

	public void SetSeed(int _seed)
	{
		Seed = _seed;
	}

	public int[,] CreateApartment(int[,] data)
	{
		if(Seed == -1)
			Seed = Random.Range(-10000,10000);

		prng = new System.Random(Seed);	
		MapData = new int[Width,Height];

		for(int x = 0; x < Width; x++)
		{
			for(int y = 0; y < Height; y++)
			{
				MapData[x,y] = 7;
			}
		}

		CreateEntrace();
		CreateInitialRooms();
		ExpandRooms();
		CreateRoomMapData();
		CreateDoors();

		data = PositionData(data);

		return data;
	}

	void CreateInitialRooms()
	{
		Rooms = new List<Room>();

		Room topleft = new Room(prng);
		topleft.SetPivot(Room.Pivot.TOPLEFT, Height, Width);

		Rooms.Add(topleft);


		Room topRight = new Room(prng);
		topRight.SetPivot(Room.Pivot.TOPRIGHT, Height, Width);

		Rooms.Add(topRight);


		Room bottomLeft = new Room(prng);
		bottomLeft.SetPivot(Room.Pivot.BOTTOMLEFT, Height, Width);

		Rooms.Add(bottomLeft);


		Room bottomRight = new Room(prng);
		bottomRight.SetPivot(Room.Pivot.BOTTOMRIGHT, Height, Width);

		Rooms.Add(bottomRight);

		//Links
		topleft.CreateLink(Room.Direction.RIGHT, topRight);
		topRight.CreateLink(Room.Direction.BOTTOM, bottomRight);
		bottomRight.CreateLink(Room.Direction.LEFT, bottomLeft);
		bottomLeft.CreateLink(Room.Direction.TOP, topleft);
	}

	void ExpandRooms()
	{
		bool bComplete = false;

		Debug.Log("Expand Rooms");

		int counter = 0;

		while(!bComplete)
		{
			bComplete = true;

			for(int i = 0; i < Rooms.Count; i++)
			{
				if(Rooms[i].Expand())
					bComplete = false;
			}
		}
	}

	void CreateRoomMapData()
	{
		for(int i = 0; i < Rooms.Count; i++)
		{
			Room r = Rooms[i];

			r.DrawRoom(MapData);
		}
	}

	void CreateEntrace()
	{
		int Entrance = prng.Next(1, Width - 2);// Random.Range(1, Width - 2);
		int Bottom = (Height - 1);
		MapData[Entrance, Bottom] = 12;
	}

	void CreateDoors()
	{
		Rooms[0].CreateDoor(MapData, Rooms[1]);
		Rooms[1].CreateDoor(MapData, Rooms[3]);

		Rooms[3].CreateDoor(MapData, Rooms[2]);
		Rooms[2].CreateDoor(MapData, Rooms[0]);
	}

	int[,] PositionData(int[,] data)
	{
		Debug.Log("Creating apartment at " + PosX + ", " + PosY);

		for(int x = 0; x < Width; x++)
		{
			for(int y = 0; y < Height; y++)
			{
				data[PosX + x, PosY	+ y] = MapData[x,y];
			}
		}

		return data;
	}
}
