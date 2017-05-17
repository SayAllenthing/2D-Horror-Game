using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUIObject : MonoBehaviour 
{
	public Image Renderer;
	public Text AmountText;

	public ItemData Data;

	public Button button;

	void Start()
	{		
		GetComponent<Button>().onClick.AddListener(delegate {
			OnClicked();
		});
	}

	public void SetObject(ItemData _data, int _num)
	{
		Data = _data;

		AmountText.text = _num > 1 ? _num.ToString() : "";

		if(_num < 1)
		{
			Renderer.enabled = false;
			GetComponent<Button>().enabled = false;
		}
		else
		{
			Renderer.enabled = true;
			Renderer.sprite = Data.Sprite;
			GetComponent<Button>().enabled = true;
		}
	}

	public void OnClicked()
	{
		GameUIManager.Instance.OnItemClicked(Data.Name);
	}
}
