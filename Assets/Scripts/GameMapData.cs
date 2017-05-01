using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMapData : MonoBehaviour {

	public static GameMapData Instance;

	Node[,] Grid;

	float TileSize;

	bool bDrawDebug = false;

	public List<Transform> Actors = new List<Transform>();

	// Use this for initialization
	void Awake () 
	{
		if(Instance != null)
			Instance = null;

		Instance = this;
	}
	
	public void SetMapData(Node[,] _data, float _size)
	{
		Grid = _data;
		TileSize = _size;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
			bDrawDebug = !bDrawDebug;
	}

	public void AddActor(Transform t)
	{
		Actors.Add(t);
	}

	public Node GetNodeFromWorldPoint(Vector3 pos)
	{
		float percentX = pos.x / (Grid.GetLength(0) * TileSize);
		float percentY = pos.y / (Grid.GetLength(1) * TileSize);

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((Grid.GetLength(0)) * percentX);
		int y = Grid.GetLength(1) - 1 - (Mathf.RoundToInt((Grid.GetLength(1)) * percentY));
		return Grid[x,y];
	}

	public Node GetNodeFromXY(int x, int y)
	{
		return Grid[x,y];
	}

	//Objects
	public bool PlaceObject(string Object, Vector3 pos)
	{
		NetworkHelper.Instance.SpawnObject(pos, Object);

		/*
		Node n = GetNodeFromWorldPoint(pos);

		if(n.CanPlaceObject())
		{
			GameObject prefab = ItemPrefabManager.Instance.GetItemPrefab(Item.eItemType.LAMP);

			n.PlaceObject(prefab);
		}
		*/

		return false;
	}

	//AI
	public void FindPath(Vector3 startPos, Vector3 endPos)
	{
		Node startNode = GetNodeFromWorldPoint(startPos);
		Node endNode = GetNodeFromWorldPoint(endPos);

		List<Node> openNodes = new List<Node>();
		HashSet<Node> closedNodes = new HashSet<Node>();

		openNodes.Add(startNode);

		while(openNodes.Count > 0)
		{
			Node currentNode = openNodes[0];
			for(int i = 1; i < openNodes.Count; i++)
			{
				if(openNodes[i].fCost < currentNode.fCost || openNodes[i].fCost == currentNode.fCost)					
				{
					if(openNodes[i].hCost < currentNode.hCost)
						currentNode = openNodes[i];
				}
			}

			openNodes.Remove(currentNode);
			closedNodes.Add(currentNode);

			if(currentNode == endNode)
			{
				GetFinalPath(startNode, endNode);
				return;
			}

			foreach(Node neighbour in FindNeighbours(currentNode))
			{
				if(neighbour.bPhysics || closedNodes.Contains(neighbour))
				{					
					continue;
				}

				int moveCost = currentNode.gCost + GetDistance(currentNode, neighbour);
				if(moveCost < neighbour.gCost || !openNodes.Contains(neighbour))
				{
					neighbour.gCost = moveCost;
					neighbour.hCost = GetDistance(neighbour, endNode);
					neighbour.Parent = currentNode;

					if(!openNodes.Contains(neighbour))
						openNodes.Add(neighbour);
				}
			}
		}
	}

	void GetFinalPath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while(currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}
		path.Reverse();

		ThePath = path;
	}

	int GetDistance(Node nodeA, Node nodeB)
	{
		//May be fucked
		int dstX = Mathf.Abs(nodeA.X - nodeB.X);
		int dstY = Mathf.Abs(nodeA.Y - nodeB.Y);

		if(dstX > dstY)
			return 14*dstY + 10*(dstX-dstY);

		return 14*dstX + 10*(dstY-dstX);
	}

	List<Node> FindNeighbours(Node n)
	{
		List<Node> neighbours = new List<Node>();

		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <= 1; y++)
			{
				if(x == 0 && y == 0 || Mathf.Abs(x) + Mathf.Abs(y) == 2)
					continue;

				int checkX = n.X + x;
				int checkY = n.Y + y;

				if(checkX >= 0 && checkX < Grid.GetLength(0) && checkY >= 0 && checkY < Grid.GetLength(1))
					neighbours.Add(Grid[checkX,checkY]);
			}
		}

		return neighbours;
	}

	List<Node> FindNeighboursAll(Node n)
	{
		List<Node> neighbours = new List<Node>();

		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <= 1; y++)
			{
				if(x == 0 && y == 0)
					continue;

				int checkX = n.X + x;
				int checkY = n.Y + y;

				if(checkX >= 0 && checkX < Grid.GetLength(0) && checkY >= 0 && checkY < Grid.GetLength(1))
					neighbours.Add(Grid[checkX,checkY]);
			}
		}

		return neighbours;
	}

	//Debug
	public List<Node> ThePath;
	void OnDrawGizmos()
	{
		if(!bDrawDebug)
			return;

		if(Grid != null)
		{
			List<Node> ActorNodes = new List<Node>();

			for(int i = Actors.Count - 1; i >= 0; i--)
			{	
				if(Actors[i] == null)
				{
					Actors.Remove(Actors[i]);
					continue;
				}

				ActorNodes.Add(GetNodeFromWorldPoint(Actors[i].position));

			}				

			foreach(Node n in Grid)
			{
				Gizmos.color = n.bPhysics ? Color.red : Color.white;

				for(int i = 0; i < ActorNodes.Count; i++)
				{
					if(n == ActorNodes[i])
						Gizmos.color = Color.blue;
				}

				if(ThePath != null)
				{


					if(ThePath[0] == n)
						Gizmos.color = Color.green;
				}

				if(n.X == 0 || n.Y == 0)
					Gizmos.color = Color.cyan;

				Gizmos.DrawCube(new Vector3(n.Position.x, n.Position.y + TileSize/2, n.Position.z), Vector3.one * (TileSize - 0.1f));
			}

		}
	}
}
