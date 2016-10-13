using UnityEngine;
using System.Collections;

public class Node
{
	public Vector3 Position;
	public bool bPhysics;

	public int X;
	public int Y;

	public int gCost;
	public int hCost;

	public Node Parent;

	public Node(bool _physics, Vector3 _pos, int _x, int _y)
	{
		bPhysics = _physics;
		Position = _pos;

		X = _x;
		Y = _y;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
