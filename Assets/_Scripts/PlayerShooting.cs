using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Number Variables")]
    public Transform firingPoint;
    public GameObject playerSword;
    public BaseBullet bulletPrefab;
    public float slashComboLimit = 1;
    public float fireRate = 0.7f;
    public float slashComboCooldown = 0.3f; // This is the cool down for the sword combo
    public float slashRate = 0.7f;          // This is the the time between different sets of combos
    private Vector2 mousePosition;
    private Vector2 firingOrigin;
    
    private Rigidbody2D body;
    private RaycastHit2D hit;
    private int currentCombo;
    private bool canFire = true;
    private bool canSlash = true;

    float lastTime;

    public Animator Animator;

    // private enemyBehavior damaging;
    // private weaponInfo useClip;

    private void Start() 
    {
        StartCoroutine(slashEnumerator());
    }

    private void Update() 
    {
        if (Input.GetAxisRaw("Fire1") != 0) 
            Fire();
        // if (Input.GetAxisRaw("Fire2") != 0)
        //     Slash();
        if (canFire == true && !playerSword.activeSelf)
        {
            Animator.Play("PlayerWalk");
        }
    }

    public void Fire() 
    {
        if (canFire) 
            StartCoroutine(fireEnumerator());
    }

    // public void Slash() 
    // {
    //     if (canSlash)
    //         StartCoroutine(slashEnumerator());
    // }

    IEnumerator fireEnumerator() 
    {
        AudioManager.instance.Play("blaster");
        Animator.Play("PlayerShoot");
        BaseBullet bullet = Instantiate(bulletPrefab, firingPoint);
        bullet.transform.SetParent(null);
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
    
    IEnumerator slashEnumerator() 
    {
        while (true) 
        {
            if (Input.GetButtonDown("Fire2")) 
            {
                PlaySwordAudio();
                Animator.Play("LeftSlash");
                playerSword.GetComponent<SwordDamagePlayer>().bHasDamaged = false;
                playerSword.SetActive(true);
                currentCombo++;
                Debug.Log("Attack" + currentCombo);
                lastTime = Time.time;
                yield return new WaitForSeconds(slashRate);
                playerSword.SetActive(false);

                while(currentCombo < slashComboLimit && (Time.time - lastTime) < slashComboCooldown) 
                {
                    if (Input.GetButtonDown("Fire2"))  
                    {
                        Animator.Play("PlayerRightSlash");
                        playerSword.GetComponent< SwordDamagePlayer>().bHasDamaged = false;
                        playerSword.SetActive(true);
                        currentCombo++;
                        Debug.Log("Attack" + currentCombo);
                        lastTime = Time.time;
                        yield return new WaitForSeconds(slashRate);
                        playerSword.SetActive(false);
                    }
                    yield return null;
                }

                currentCombo = 0;
                playerSword.SetActive(false);
                yield return new WaitForSeconds(slashComboCooldown - (Time.time - lastTime));
            }
            yield return null;
            playerSword.SetActive(false);
        }
    }
    private void PlaySwordAudio()
    {
        //randomly select which sword swing to get
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                AudioManager.instance.Play("sword-swing1");
                break;
            case 1:
                AudioManager.instance.Play("sword-swing2");
                break;
            case 2:
                AudioManager.instance.Play("sword-swing3");
                break;
        }

    }
}
