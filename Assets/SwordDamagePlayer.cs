using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamagePlayer : MonoBehaviour
{
    public bool bHasDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.tag == "Enemy" && bHasDamaged == false)
        {
            bHasDamaged = true;

            collision.GetComponent<EnemyAIBase>().Damage(3);
        }
    }
}
