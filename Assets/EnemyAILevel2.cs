using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILevel2 : EnemyAIBase
{

    public GameObject MeleeCollider;

    

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.StartScene();
        MeleeCollider.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bReadytoFight)
        {
            //if player close
            if (currentAttackTime >= attackInterval)
            {
                currentAttackTime = 0.0f;
                if (bPlayerInRange)
                {
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

    }

    private void FixedUpdate()
    {
        if (bReadytoFight)
        {
            if (!bIsRanging && !bIsMeleeing && !bPlayerInRange)
            {
                Animator.Play("WalkAnim");

                if (Player)
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

            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }

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
            //bIsMeleeing = true;
            StartCoroutine(IEMeleeAttack());
            //wait a bit

            // play animations with key frames
            //activate melee hitboxes
        }

    }

    IEnumerator IERangeAttack()
    {
        Animator.Play("RangeAnim");
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
        
        Animator.Play("MeleeAnim");
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
        if (Player)
        {
            transform.position = Player.transform.position - Player.transform.right * -0.7f;

            setRotation();
        }

    }

    void EndMelee()
    {
        bIsMeleeing = false;
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

    void FireBullet()
    {
        setRotation();
        Instantiate(EnemyBullet, transform.position, Quaternion.identity);
    }
}
