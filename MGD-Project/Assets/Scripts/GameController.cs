using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pickups;
    [SerializeField] private TextMeshProUGUI keyLabel;
    private int keyCount = 0;
    [SerializeField] private int keyNum = 3;

    // Start is called before the first frame update
    void Start()
    {
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;
    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> keysChildren = pickups.GetComponentsInChildren<Transform>()
            .Where(key => key.gameObject.activeInHierarchy && key != pickups.transform)
            .ToList();

        keyCount = keyNum - keysChildren.Count;
        updateText();
    }

    void updateText()
    {
        keyLabel.text = "keys: " + keyCount + "/" + keyNum;
    }
}
