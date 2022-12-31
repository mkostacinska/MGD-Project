using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementIcon : MonoBehaviour
{
    Image image;
    [SerializeField] Sprite pyroSprite;
    [SerializeField] Sprite cryoSprite;
    [SerializeField] Sprite electroSprite;
    TMP_Text reactionText;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.clear;
        //reactionText = GetComponentInChildren<TMP_Text>();
        reactionText = transform.GetChild(0).GetComponent<TMP_Text>();
        reactionText.text = "";
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

    public void setReaction(string name, float seconds)
    {
        StartCoroutine(setReactionTimed(name, seconds));
    }
        
    public IEnumerator setReactionTimed(string name, float seconds) {
        reactionText.text = name;
        yield return new WaitForSeconds(seconds);
        reactionText.text = "";
    }
        
}
