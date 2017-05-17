using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class IngredientObject : MonoBehaviour {

    public Image ItemImage;
    public Text ItemAmount;

    public void Init(Sprite _image, int text)
    {
        ItemImage.sprite = _image;
        ItemAmount.text = "x" + text.ToString();
    }
}
