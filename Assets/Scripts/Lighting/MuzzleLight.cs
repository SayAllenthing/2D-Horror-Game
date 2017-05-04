using UnityEngine;
using System.Collections;

public class MuzzleLight : MonoBehaviour {

	Mesh msh;
	public MeshRenderer renderer;
	public MeshRenderer QuadRenderer;

	public Color MuzzleColor;
	Color QuadDefault;

	LayerMask mask;

	public float Brightness = 6;

	float EndTime = 0;

	void Start () 
	{
		mask = ~((1 << 9) | (1 << 8) | (1 << 10));
		msh = new Mesh();

		QuadDefault = QuadRenderer.material.color;

		//CreatePolygon();
	}

	public void Refresh()
	{
		StartCoroutine("eCreatePolygon");
	}

	void Update()
	{
		if(renderer.enabled && Time.time > EndTime)
		{
			renderer.enabled = false;
			QuadRenderer.material.color = QuadDefault;
		}
	}

	IEnumerator eCreatePolygon()
	{
		renderer.enabled = false;

		Vector3 vector = Vector3.right;

		float angle = -10;

		Vector3 pos = Vector3.zero;
		Vector2[] vertices2D = new Vector2[36];

		for(int i = 0; i < 36; i++)
		{
			Vector2 ray = new Vector2(transform.position.x, transform.position.y);
			RaycastHit2D hit;

			//Debug.DrawRay(ray, vector * Brightness, Color.green, 5);

			Vector3 dir = vector * (Brightness * 1.5f);

			hit = Physics2D.Raycast(ray, dir, dir.magnitude, mask);

			Vector2 point;

			if(hit.collider != null)
			{				
				point = new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y);
			}
			else
			{
				point = new Vector2(dir.x, dir.y);
			}
			vertices2D[i] = point;

			vector = Quaternion.Euler(0, 0, angle) * vector;
		}

		//vertices2D[24] = new Vector2(pos.x,pos.y);

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


		renderer.material.SetVector("_Color",  new Vector4(MuzzleColor.r, MuzzleColor.g, MuzzleColor.b, MuzzleColor.a));
		renderer.material.SetVector("_Source", new Vector4(transform.position.x, transform.position.y, 0, 1));
		renderer.material.SetFloat("_HiY", Brightness);
		renderer.material.SetFloat("_LoY", -10);

		gameObject.GetComponent<MeshFilter>().mesh = msh;




		renderer.enabled = true;

		EndTime = Time.time + 0.1f;
		//QuadRenderer.material.color = MuzzleColor;

		yield return null;
	}
}
