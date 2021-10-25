using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    EnemyAIBase AIParent;
    Rigidbody2D rb2d;

    Vector3 OriginalPos;
    // Start is called before the first frame update
    void Start()
    {

        AIParent = FindObjectOfType<EnemyAIBase>();
        rb2d = GetComponent<Rigidbody2D>();
        OriginalPos = AIParent.gameObject.transform.right * AIParent.BulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = OriginalPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.OnPlayerDeath();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
    }
}
