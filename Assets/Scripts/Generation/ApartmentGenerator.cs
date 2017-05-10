using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApartmentGenerator : MonoBehaviour 
{	
	public static ApartmentGenerator Instance;

	public PhysicsMaterial2D mat;

	int[,] MapData;

	int ApartmentWidth = 20;
	int ApartmentHeight = 20;

	int NumApartments = 5;

	public int Seed = 0;

	public List<Apartment> Apartments = new List<Apartment>();

	Tilemap tileMap;

	System.Random Rng;

    public delegate void GenerationCompleteDelegate();
    public GenerationCompleteDelegate OnGenerationComplete;

    // Use this for initialization
    void Awake () 
	{
		if(Instance != null)
		{
			DestroyImmediate(gameObject);
		}

		Instance = this;
		DontDestroyOnLoad(this);        
	}

	void OnLevelWasLoaded(int level)
	{
		if(level == 1)
		{
			GenerateFloor();
			GenerateTiles();
		}

        if (OnGenerationComplete != null)
            OnGenerationComplete();
    }

	public void SetSeed(int _seed)
	{
		Seed = _seed;

		//If seed gets set as zero it will auto generate
		if(Mathf.Abs(Seed) < 200)
		{
			while( Mathf.Abs(Seed) < 200)
			{			
				Seed = Random.Range(-10000, 10000);
			}
		}

		Rng = new System.Random(Seed);

		MainMenuManager.Instance.SetSeed(Seed);
	}

	public void SetTileMap(Tilemap tm)
	{
		tileMap = tm;
	}

	public void GenerateFloor()
	{
		GenerateApartments();
		GenerateHallway();       
	}

	public void GenerateTiles()
	{
		float size = tileMap.Tiles[0].sprite.bounds.size.x;

		int MapWidth = MapData.GetLength(0);
		int MapHeight = MapData.GetLength(1);

		Node[,] PathFindingData = new Node[MapWidth,MapHeight];

		for(int x = 0; x < MapWidth; x++)
		{
			for(int y = 0; y < MapHeight; y++)
			{
				int tile = MapData[x,y];
				Tilemap.TileData td = tileMap.Tiles[tile];

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

				Node node = new Node(td.bPhysics, g.transform.localPosition, x, y, g);
				PathFindingData[x,y] = node;
			}
		}

		GameMapData.Instance.SetMapData(PathFindingData, size);
	}

	public void GenerateApartments()
	{
		MapData = new int[ApartmentWidth * NumApartments, ApartmentHeight + 4];

		for(int i = 0; i < NumApartments; i++)
		{
			Apartment a = new Apartment(ApartmentWidth, ApartmentHeight, i * ApartmentWidth, 0);
			a.SetSeed(Rng);
			MapData = a.CreateApartment(MapData);
			Apartments.Add(a);
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
