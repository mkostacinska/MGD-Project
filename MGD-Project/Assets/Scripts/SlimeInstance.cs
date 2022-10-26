using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeInstance : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int level = 1;
    [SerializeField] private int range = 1;

    private Slime thisSlime;
    // Start is called before the first frame update
    void Start()
    {
        thisSlime = new Slime(health, level, range);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") {    //for now, it searches by gameObject name
            MonoBehaviour.print("hit player");
            Player p = collision.gameObject.GetComponent<PlayerInstance>().thisPlayer;
            p.setHealth(p.getHealth() - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
