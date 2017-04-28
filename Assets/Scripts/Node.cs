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

	bool bHasObject = false;

	public GameObject gameObject;
	SpriteRenderer spriteRenderer;

	float Width;
	float Height;

	public Node(bool _physics, Vector3 _pos, int _x, int _y, GameObject _g)
	{
		bPhysics = _physics;
		Position = _pos;

		X = _x;
		Y = _y;

		gameObject = _g;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		Width = spriteRenderer.bounds.size.x;
		Height = spriteRenderer.bounds.size.y;
	}

	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public bool CanPlaceObject()
	{
		return !bPhysics && !bHasObject;
	}

	public void PlaceObject(GameObject obj)
	{
		//Legacy
		Vector3 pos = Position;
		pos.y += Height/2;

		//GameObject g = GameObject.Instantiate(obj, pos, Quaternion.identity) as GameObject;
		//NetworkHelper.Instance.SpawnObject(g);
		bHasObject = true;
	}

	public void SetObject(GameObject obj)
	{
		gameObject = obj;
		bHasObject = true;
	}

	public Vector3 GetObjectPosition()
	{
		return new Vector3(Position.x, Position.y + Height/2, Position.z);
	}
}
