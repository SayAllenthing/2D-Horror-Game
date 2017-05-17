using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeObject : MonoBehaviour
{
    ItemData Data;
    CraftingWindow Window;

    public Image ItemImage;
    public Button ItemButton;

    List<Ingredient> Ingredients = new List<Ingredient>();

    public void Init(CraftingWindow _window, ItemData _data)
    {
        Data = _data;
        Window = _window;        

        ItemImage.sprite = Data.Sprite;

        ItemButton.onClick.AddListener(delegate { OnClicked(); });
    }   

    public void OnClicked()
    {
        Window.OnRecipeClicked(Data);
    }
}
