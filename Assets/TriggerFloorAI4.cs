using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFloorAI4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAILevel4>())
        {
            collision.gameObject.GetComponent<EnemyAILevel4>().bDoMelee = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAILevel4>())
        {
            collision.gameObject.GetComponent<EnemyAILevel4>().bDoMelee = false;
        }
    }
}
