using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUIObject : MonoBehaviour 
{
	public Image Renderer;
	public Text AmountText;

	public string Item;

	public Button button;

	void Start()
	{
		Debug.Log("Test");

		GetComponent<Button>().onClick.AddListener(delegate {
			OnClicked();
		});
	}

	public void SetObject(Inventory.InventoryObject obj)
	{
		Item = obj.item;

		AmountText.text = obj.amount > 1 ? obj.amount.ToString() : "";

		if(obj.amount < 1)
		{
			Renderer.enabled = false;
		}
		else
		{
			Renderer.enabled = true;
			Renderer.sprite = obj.sprite;
		}
	}

	public void OnClicked()
	{
		
	}
}
