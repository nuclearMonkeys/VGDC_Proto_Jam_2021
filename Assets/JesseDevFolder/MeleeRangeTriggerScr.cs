using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeTriggerScr : MonoBehaviour
{
    EnemyAIBase AIParent;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        AIParent = FindObjectOfType<EnemyAIBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AIParent.bPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AIParent.bPlayerInRange = false;
        }
    }
}
