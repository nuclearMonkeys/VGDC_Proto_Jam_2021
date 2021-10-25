using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
abstract public class EnemyAIBase : MonoBehaviour
{
    public float attackInterval = 1.0f;
    public float currentAttackTime = 0.0f;

    public float BulletSpeed = 1.0f;
    public float EnemySpeed = 1.0f;
    public GameObject EnemyBullet;
    public GameObject Player;
    public int Health = 10;
    public bool bShouldMove = false;
    public bool bShouldRange = false;
    public bool bShouldMelee = false;

    public bool bIsRanging = false;
    public bool bIsMeleeing = false;

    public bool bPlayerInRange = false;

    public Rigidbody2D rb2d;

    public Animator Animator;
    public GameObject BloodPS;

    public bool bReadytoFight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int Amount)
    {
        AudioManager.instance.Play("impact");
        BloodPS.GetComponent<ParticleSystem>().Play();
        Health -= Amount;
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //do shit
        GameManager.Instance.OnEnemyDeath(SceneManager.GetActiveScene().buildIndex + 1);
        EnemyBullet[] bullets = FindObjectsOfType<EnemyBullet>();
        EnemyBulletRing[] bulletRings = FindObjectsOfType<EnemyBulletRing>();

        foreach (EnemyBullet b in bullets)
        {
            Destroy(b.gameObject);
        }

        foreach (EnemyBulletRing br in bulletRings)
        {
            Destroy(br.gameObject);
        }

        Destroy(gameObject);
    }
}
