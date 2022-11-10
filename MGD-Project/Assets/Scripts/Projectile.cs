using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        MonoBehaviour.print("projectile hit enemy");
        NPC enemy = null;
        if (other.gameObject.TryGetComponent(out SlimeInstance slime))
        { enemy = other.gameObject.GetComponent<SlimeInstance>().thisSlime; }      //sets enemy to slime if it exists
        if (other.gameObject.TryGetComponent(out WalkerInstance walker))
        { enemy = other.gameObject.GetComponent<WalkerInstance>().thisWalker; }    //sets enemy to walker if it exists
        if (other.gameObject.TryGetComponent(out TurrentInstance turret))
        { enemy = other.gameObject.GetComponent<TurrentInstance>().thisTurret; }    //sets enemy to turret if it exists
        enemy.setHealth(enemy.getHealth() - 1);

        if (!enemy.Equals(null))
        {
            Destroy(gameObject);
        }
    }
}
