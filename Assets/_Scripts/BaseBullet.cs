using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D body;
    public float damage;
    public float speed;

    void Awake() 
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right * speed;
    }

    public void SetDirection(Vector2 direction) 
    {
        if (!body) 
        {
            body = GetComponent<Rigidbody2D>();
            body.gravityScale = 0;
        }
        transform.right = direction;
        body.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Debug.Log(other.gameObject.name);
        if (other.CompareTag("Enemy")) 
        {
            // Do damage to enemy
            
            other.GetComponent<EnemyAIBase>().Damage(1);
        }
        if (other.CompareTag("Player"))
            return;
        Destroy(this.gameObject);
    }
}
