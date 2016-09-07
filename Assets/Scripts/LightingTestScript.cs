using UnityEngine;
using System.Collections;
using System.IO;

public class LightingTestScript : MonoBehaviour {


	Mesh msh;

	public Transform target;



	public int ConeLength = 3;
	public int ConeWidth = 2;
	public int Raycasts = 5;

	// Use this for initialization
	void Start () 
	{
		
		msh = new Mesh();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.DrawLine(transform.position, Vector3.right * 5, Color.red, 0.1f, false);

					
		CreatePolygon();
	}

	void CreatePolygon()
	{
		Vector3 pos = Vector3.zero;

		Vector2[] vertices2D = new Vector2[Raycasts+1];

		Vector2 TargetAngle = transform.parent.GetComponent<Character>().LastAngle;

		Vector3 AngleFacing = new Vector3(TargetAngle.x, TargetAngle.y);

		float inc = ((float)ConeWidth/(Raycasts-1));

		for( int i = 0; i < Raycasts; i++)
		{
			float y = pos.y + (i * inc) + -(float)ConeWidth/2;
			Vector2 dir = new Vector2(pos.x + ConeLength, y);

			dir.x *= dir.normalized.x;

			float angle = Vector2.Angle(Vector2.right, AngleFacing);
			if(Vector2.Dot(AngleFacing, Vector2.up) < 0)
				angle *= -1;

			dir = Quaternion.Euler(0,0, angle) * dir;

			Vector2 ray = new Vector2(transform.position.x, transform.position.y);
			RaycastHit2D hit;

			Debug.DrawRay(ray, dir, Color.green);

			hit = Physics2D.Raycast(ray, dir, dir.magnitude);

			if(hit.collider != null)
			{				
				vertices2D[i] = new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y);
			}
			else
			{
				//Debug.Log(dir.x + " " + transform.position.x);


				vertices2D[i] = new Vector2(dir.x, dir.y);
			}
		}

		vertices2D[Raycasts] = new Vector2(pos.x,pos.y);


		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}
		

		msh.vertices = vertices;
		msh.triangles = indices;

		// Set up game object with mesh;
		//gameObject.AddComponent(typeof(MeshRenderer));
		//MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		gameObject.GetComponent<MeshFilter>().mesh = msh;
	}


}
