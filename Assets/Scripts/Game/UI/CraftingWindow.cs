using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : UIWindow {
    
    public CanvasGroup RecipePanel;

    public GameObject RecipeGrid;
    public GameObject RecipePrefab;

    public GameObject IngredientGrid;
    public GameObject IngredientPrefab;

    List<GameObject> Recipes;
    List<GameObject> Ingredients;

    ItemData CurrentItem = null;

    public void OnRecipeClicked(ItemData data)
    {
        Ingredients = new List<GameObject>();

        RecipeAttribute r = data.GetAttribute<RecipeAttribute>() as RecipeAttribute;
        if (r != null)
        {
            foreach(Ingredient i in r.Ingredients)
            {
                AddIngredient(i.item, i.amount);
            }
        }

        ShowRecipePanel();

        CurrentItem = data;
    }

    public override void Show()
    {
        base.Show();

        Recipes = new List<GameObject>();

        ItemData lamp = ItemFactory.Instance.GetItem("SmallLamp");
        AddRecipe(lamp);
    }    

    public override void Hide()
    {
        base.Hide();

        for( int i = Recipes.Count-1;  i >= 0; i--)
        {
            GameObject g = Recipes[i];
            Recipes.Remove(g);
            Destroy(g);
        }

        HideRecipePanel();
    }

    void ShowRecipePanel()
    {      

        RecipePanel.alpha = 1;
    }

    void HideRecipePanel()
    {
        for (int i = Ingredients.Count - 1; i >= 0; i--)
        {
            GameObject g = Ingredients[i];
            Ingredients.Remove(g);
            Destroy(g);
        }

        RecipePanel.alpha = 0;
    }

    void AddRecipe(ItemData item)
    {
        GameObject g = GameObject.Instantiate(RecipePrefab, RecipeGrid.transform);
        g.GetComponent<RecipeObject>().Init(this, item);

        Recipes.Add(g);
    }

    void AddIngredient(ItemData item, int amount)
    {
        GameObject g = GameObject.Instantiate(IngredientPrefab, IngredientGrid.transform);
        g.GetComponent<IngredientObject>().Init(item.Sprite, amount);

        Ingredients.Add(g);
    }
}
