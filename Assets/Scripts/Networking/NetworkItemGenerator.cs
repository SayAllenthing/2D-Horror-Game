using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkItemGenerator : MonoBehaviour
{
    public static NetworkItemGenerator Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(Instance);
        }

        Instance = this;

        //ApartmentGenerator.Instance.OnGenerationComplete += GenerateItems;
    }

    private void Start()
    {
        StartCoroutine("eGenerateItemsTimer");
    }

    public void GenerateItems()
    {        
        int rndX, rndY;

        foreach(Apartment a in ApartmentGenerator.Instance.Apartments)
        {
            foreach (Room r in a.Rooms)
            {
                rndX = Random.Range(0, r.width);
                rndY = Random.Range(0, r.height);

                int[] pos = r.GetProperCoords(rndX, rndY);

                pos[0] += a.PosX;
                pos[1] += a.PosY;

                Vector3 position = GameMapData.Instance.GetNodeFromXY(pos[0], pos[1]).Position;

                NetworkHelper.Instance.SpawnObject(position, "Scrap");
            }
        }
    }

    IEnumerator eGenerateItemsTimer()
    {
        while(Time.timeSinceLevelLoad < 3)
        {
            Debug.Log("Wait " + Time.timeSinceLevelLoad);
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Generate" + Time.timeSinceLevelLoad);

        GenerateItems();
        yield return null;
    }
}
