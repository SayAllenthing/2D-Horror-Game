using UnityEngine;
using System.Collections.Generic;

public class TestInventory : MonoBehaviour {

    public List<Item> items;

	void Start()
    {
	    for (int i = 0; i < items.Count; i++)
        {
            items[i] = Instantiate(items[i]) as Item;
        }
	}

}
