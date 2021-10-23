using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILevel3 : EnemyAIBase
{
    public GameObject MeleeCollider;
    public List<GameObject> TeleportPoints = new List<GameObject>();
    int TeleportCount = 0;
    //public int BulletCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        TeleportCount = Random.Range(2, 4);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player close
        if (currentAttackTime >= attackInterval)
        {
            currentAttackTime = 0.0f;
            if (TeleportCount <=0)
            {
                //Debug.Log("Hello");
                
                MeleeAttack();
                // do melee
            }
            else
            {
                // do range
                RangeAttack();
            }
        }
        currentAttackTime += Time.deltaTime;
    }

    void RangeAttack()
    {
        if (!bIsRanging && !bIsMeleeing)
        {
            //bIsRanging = true;
            StartCoroutine(IERangeAttack());
            //wait a bit

            //shoot projectile
        }

    }

    void MeleeAttack()
    {
        if (!bIsRanging && !bIsMeleeing)
        {
            Debug.Log("Hello2");
            //bIsMeleeing = true;
            StartCoroutine(IEMeleeAttack());
            //wait a bit

            // play animations with key frames
            //activate melee hitboxes
        }

    }

    void enableColliderTrue()
    {
        enableMeleeCollider(true);
    }

    void enableColliderFalse()
    {
        enableMeleeCollider(false);
    }

    void enableMeleeCollider(bool enabler)
    {
        MeleeCollider.SetActive(enabler);
    }

    void TeleportToPlayer()
    {
        transform.position = Player.transform.position - Player.transform.right * -0.7f;

        setRotation();
    }


    void ChargeAtPlayer()
    {
        Vector3 targ = Player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rb2d.velocity = transform.right * EnemySpeed;
    }
    void EndMelee()
    {
        bIsMeleeing = false;
        rb2d.velocity = Vector2.zero;
    }

    void EndRange()
    {
        bIsRanging = false;
    }


    void BeginMelee()
    {
        bIsMeleeing = true;
    }

    void BeginRange()
    {
        bIsRanging = true;
    }


    IEnumerator IERangeAttack()
    {
        Animator.Play("TeleportRange");
        
        while (bIsRanging)
        {
            yield return null;
        }

        bIsRanging = false;
        currentAttackTime = 0.0f;
        yield return null;
    }

    IEnumerator IEMeleeAttack()
    {
        TeleportCount = Random.Range(2, 4);
        Animator.Play("TeleportCharge");
        //Animator.
        while (bIsMeleeing)
        {
            yield return null;
        }

        bIsMeleeing = false;
        currentAttackTime = 0.0f;
        yield return null;
    }

    void setRotation()
    {
        // rotation code
        Vector3 targ = Player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void TeleportToPoint()
    {
        //leave behind particle

        //make bullet ring

        GameObject Point = TeleportPoints[Random.Range(0, TeleportPoints.Count - 1)];

        transform.position = Point.transform.position;

        TeleportCount -= 1;
    }

    void fireBulletRing()
    {
        int BulletCount = Random.Range(7,10);




        setRotation();
        for (int i = 0; i < BulletCount; i++)
        {
            Quaternion Rot = Quaternion.Euler(0, 0, (360/BulletCount) * i);
            Instantiate(EnemyBullet, transform.position, Rot);
        }


    }
}
