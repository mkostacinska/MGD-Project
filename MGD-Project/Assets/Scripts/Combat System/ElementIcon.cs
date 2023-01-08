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

    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.clear;
        reactionText = transform.GetChild(0).GetComponent<TMP_Text>();
        reactionText.text = "";
    }

    /// <summary>
    /// Sets the image according to the given element.
    /// </summary>
    /// <param name="element"> Element to be displayed. </param>
    public void SetElementIcon(Element element)
    {
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

    /// <summary>
    /// Sets the given reaction name to appear for a given number of seconds.
    /// </summary>
    /// <param name="name"> The name of the reaction to be displayed, e.g. 'Melt'.</param>
    /// <param name="seconds"> The number of seconds to display the reaction for.</param>
    public void SetReaction(string name, float seconds)
    {
        StartCoroutine(SetReactionTimed(name, seconds));
    }
        
    public IEnumerator SetReactionTimed(string name, float seconds) {
        reactionText.text = name;
        yield return new WaitForSeconds(seconds);
        reactionText.text = "";
    }
        
}
