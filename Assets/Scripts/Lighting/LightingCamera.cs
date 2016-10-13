using UnityEngine;
using System.Collections;
using System.IO;

public class LightingCamera : MonoBehaviour 
{
	Camera camera;

	Texture2D tex;
	Mesh msh;
	RenderTexture rt;

	public Renderer rend;

	// Use this for initialization
	void Start () 
	{
		tex = new Texture2D(Screen.width,Screen.height, TextureFormat.ARGB32, false);
		rt = new RenderTexture(Screen.width,Screen.height, 32);
		camera = GetComponent<Camera>();


	}
	
	// Update is called once per frame
	void Update () 
	{
		CreateTexture();

		if(Input.GetKeyDown(KeyCode.P))
			TakeScreen();
	}

	void CreateTexture()
	{


		camera.targetTexture = rt;
		//camera.Render();

		RenderTexture.active = rt;


		tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		tex.Apply();

		RenderTexture.active = null;
		//LightingCamera.targetTexture = null;


		//byte[] bytes = tex.EncodeToPNG();
		//File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

		//Debug.Log("Screenshot did");

		rend.material.SetTexture("_MainTex", tex);
	}

	void TakeScreen()
	{
		byte[] bytes = tex.EncodeToPNG();
		File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

		Debug.Log("Screenshot did");
	}
}
