using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILevel4 : EnemyAIBase
{
    public GameObject MeleeCollider;
    public List<GameObject> TeleportPoints = new List<GameObject>();
    public int targetIndex = 1;
    int prevIndex = 0;
    public GameObject EnemyBulletRing;
    public bool bDoMelee = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        MakeNewPoint();
        MoveToPoint();
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(Vector3.Distance(TeleportPoints[targetIndex].transform.position, transform.position)) <= 0.5f)
        {
            MakeNewPoint();
           
        }
        MoveToPoint();
        setRotation();
        //if player close
        if (currentAttackTime >= attackInterval)
        {
            

            currentAttackTime = 0.0f;
            if (bDoMelee)
            {
                MeleeAttack();
                
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
            //Debug.Log("Hello2");
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
       // bIsMeleeing = true;
    }

    void BeginRange()
    {
        //bIsRanging = true;
    }


    IEnumerator IERangeAttack()
    {
        Animator.Play("AimAttackAI4");
        bIsRanging = true;
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
        //TeleportCount = Random.Range(2, 4);
        Animator.Play("RingAttackAI4");
        //Animator.
        bIsMeleeing = true;
        while (bIsMeleeing)
        {
            yield return null;
        }

        bIsMeleeing = false;
        currentAttackTime = 0.0f;
        MakeNewPoint();
        MoveToPoint();
        yield return null;
    }

    void setRotation()
    {
        // rotation code
        if (Player)
        {
            Vector3 targ = Player.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

    }

    void MoveToPoint()
    {
        //leave behind particle

        //make bullet ring

       

        Vector3 Dir = (TeleportPoints[targetIndex].transform.position - transform.position);
        Dir.Normalize();

        if (targetIndex == 0)
        {
            rb2d.velocity = Dir * EnemySpeed * 1.25f;
        }
        else
        {
            rb2d.velocity = Dir * EnemySpeed;
        }

        //TeleportCount -= 1;
    }

    void MakeNewPoint()
    {
        prevIndex = targetIndex;
        targetIndex = Random.Range(0, TeleportPoints.Count - 1);

        //bIsMeleeing = false;
        //GameObject Point = TeleportPoints[targetIndex];
    }

    void fireBulletRing()
    {
        Debug.Log("Here");
        int BulletCount = Random.Range(7, 10);




        
        for (int i = 0; i < BulletCount; i++)
        {
            Quaternion Rot = Quaternion.Euler(0, 0, (360 / BulletCount) * i);
            Instantiate(EnemyBulletRing, transform.position, Rot);
        }


    }

    void FireBullet()
    {
        setRotation();
        Instantiate(EnemyBullet, transform.position, Quaternion.identity);
    }
}
