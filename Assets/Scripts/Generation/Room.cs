using UnityEngine;
using System.Collections;

public class Door
{
	public int x;
	public int y;

	public Room RoomOne;
	public Room RoomTwo;

	public Door(Room a, Room b, int _x, int _y)
	{
		RoomOne = a;
		RoomTwo = b;

		x = _x;
		y = _y;
	}

	bool DoorExists(Room a, Room b)
	{
		return ( (RoomOne == a || RoomTwo == a) && (RoomOne == b || RoomTwo == b) );
	}
}

public class Room
{
	public int x;
	public int y;
	public int width;
	public int height;
	public int tile;
	public Pivot pivot;

	public Room Right;
	public Room Left;
	public Room Top;
	public Room Bottom;

	int ApartmentWidth;
	int ApartmentHeight;

	int retries = 0;

	System.Random prng;

	public enum Pivot
	{
		TOPLEFT,
		TOPRIGHT,
		BOTTOMLEFT,
		BOTTOMRIGHT
	}

	public enum Direction
	{
		NONE,
		LEFT,
		RIGHT,
		TOP,
		BOTTOM
	}

	public Room(System.Random _rng)
	{
		prng = _rng;

		width = prng.Next(3, 7);
		height = prng.Next(3, 7);

		tile = 12;
	}

	public void CreateLink( Direction dir, Room r )
	{
		switch(dir)
		{
		case Direction.LEFT:
			Left = r; r.Right = this; break;
		case Direction.RIGHT:
			Right = r; r.Left = this; break;
		case Direction.TOP:
			Top = r; r.Bottom = this; break;
		case Direction.BOTTOM:
			Bottom = r; r.Top = this; break;
		}
	}

	public void SetPivot( Pivot _pivot, int _height, int _width )
	{
		pivot = _pivot;

		switch(pivot)
		{
		case Pivot.TOPLEFT:
			x = 1; y = 1; break;
		case Pivot.TOPRIGHT:
			x = _width-2; y = 1; break;
		case Pivot.BOTTOMLEFT:
			x = 1; y = _height-2; break;
		case Pivot.BOTTOMRIGHT:
			x = _width-2; y = _height-2; break;
		}

		ApartmentWidth = _width;
		ApartmentHeight = _height;
	}

	public int [,] DrawRoom( int[,] map )
	{
		if(pivot == Pivot.TOPLEFT)
		{
			for(int px = x; px < x + width; px++)
			{
				for(int py = y; py < y + height; py++)
				{
					map[px,py] = tile;
				}
			}
		}
		else if(pivot == Pivot.TOPRIGHT)
		{
			for(int px = x; px > x - width; px--)
			{
				for(int py = y; py < y + height; py++)
				{
					map[px,py] = tile;
				}
			}
		}
		else if(pivot == Pivot.BOTTOMLEFT)
		{
			for(int px = x; px < x + width; px++)
			{
				for(int py = y; py > y - height; py--)
				{
					map[px,py] = tile;
				}
			}
		}
		else if(pivot == Pivot.BOTTOMRIGHT)
		{
			for(int px = x; px > x - width; px--)
			{
				for(int py = y; py > y - height; py--)
				{
					map[px,py] = tile;
				}
			}
		}

		return map;
	}

	public bool Expand()
	{
		int diffWidth = 0;
		int diffHeight = 0;

		int fullWidth = ApartmentWidth - 3;
		int fullHeight = ApartmentHeight - 3;

		if(pivot == Pivot.TOPLEFT)
		{			
			diffWidth = fullWidth - (Right.width + width);
			diffHeight = fullHeight - (Bottom.height + height);
		}
		else if(pivot == Pivot.TOPRIGHT)
		{
			diffWidth = fullWidth - (Left.width + width);
			diffHeight = fullHeight - (Bottom.height + height);
		}
		else if(pivot == Pivot.BOTTOMLEFT)
		{
			diffWidth = fullWidth - (Right.width + width);
			diffHeight = fullHeight - (Top.height + height);
		}
		else if(pivot == Pivot.BOTTOMRIGHT)
		{
			diffWidth = fullWidth - (Left.width + width);
			diffHeight = fullHeight - (Top.height + height);
		}

		width += prng.Next(0, diffWidth + 1);
		height += prng.Next(0, diffHeight + 1);

		if(retries < 10)
		{
			if(diffWidth > 0 || diffHeight > 0)
			{
				retries++;
				return true;
			}
		}

		return false;
	}

	public int[,] CreateDoor(int[,] MapData, Room b)
	{
		Direction dir = GetLink(b);

		if(dir == Direction.NONE)
			return MapData;

		int DoorX;
		int DoorY;

		if(dir == Direction.RIGHT || dir == Direction.LEFT)
		{			
			int maxHeight = height > b.height ? b.height : height;

			DoorX = width;
			DoorY = prng.Next(0, maxHeight);
		}
		else
		{
			int maxWidth = width > b.width ? b.width : width;

			DoorX = prng.Next(0, maxWidth);
			DoorY = height;
		}

		int[] coords = GetProperCoords(DoorX, DoorY);

		//Debug.Log("Creating Door from " + pivot + " To " + b.pivot + " at " + coords[0] + ", " + coords[1] + " / " + DoorX + ", " + DoorY);

		MapData[coords[0], coords[1]] = tile;

		return MapData;
	}

	Direction GetLink(Room b)
	{
		if(Left == b)
			return Direction.LEFT;
		else if(Right == b)
			return Direction.RIGHT;
		else if(Top == b)
			return Direction.TOP;
		else if(Bottom == b)
			return Direction.BOTTOM;
		else
			return Direction.NONE;	
	}

	public int[] GetProperCoords(int _x, int _y)
	{
		
		if(pivot == Pivot.TOPRIGHT)
		{
			return new int[] {x - _x, y + _y };
		}
		else if(pivot == Pivot.BOTTOMLEFT)
		{
			return new int[] {x + _x, y - _y };
		}
		else if(pivot == Pivot.BOTTOMRIGHT)
		{
			return new int[] { x - _x ,y - _y};
		}

		return new int[] {x + _x, y + _y};
	}
}
