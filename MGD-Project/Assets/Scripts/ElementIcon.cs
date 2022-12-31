using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIcon : MonoBehaviour
{
    Image image;
    [SerializeField] Sprite pyroSprite;
    [SerializeField] Sprite cryoSprite;
    [SerializeField] Sprite electroSprite;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.clear;
    }

    public void setElementIcon(Element element)
    {
        print(element);
        image.color = Color.white;
        switch (element)
        {
            case Elements.Pyro:
                image.sprite = pyroSprite;
                return;
            case Elements.Cryo:
                image.sprite = cryoSprite;
                return;
            case Elements.Electro:
                image.sprite = electroSprite;
                return;
        }
        image.sprite = null;
        image.color = Color.clear;
        return;
    }
}
