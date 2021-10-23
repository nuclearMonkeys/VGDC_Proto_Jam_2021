using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILevel1 : EnemyAIBase
{

    public GameObject MeleeCollider;

    // Start is called before the first frame update
    void Start()
    {
        MeleeCollider.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
    {
        if (!bIsRanging && !bIsMeleeing && !bPlayerInRange)
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
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
    void RangeAttack()
    {
        if (!bIsRanging && !bIsMeleeing)
        {
            bIsRanging = true;
            StartCoroutine(IERangeAttack());
            //wait a bit

            //shoot projectile
        }

    }

    void MeleeAttack()
    {
        if (!bIsRanging && !bIsMeleeing)
        {
            bIsMeleeing = true;
            StartCoroutine(IEMeleeAttack());
            //wait a bit

            // play animations with key frames
            //activate melee hitboxes
        }

    }

    IEnumerator IERangeAttack()
    {
        //instantiate bullet
        yield return new WaitForSeconds(0.25f);
        //set bullet speed;
        Instantiate(EnemyBullet, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.25f);
        bIsRanging = false;
        yield return null;
    }

    IEnumerator IEMeleeAttack()
    {
        yield return new WaitForSeconds(1.0f);
        MeleeCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MeleeCollider.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        bIsMeleeing = false;
        yield return null;
    }
}
