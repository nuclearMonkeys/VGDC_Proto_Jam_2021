using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Number Variables")]
    public Transform firingPoint;
    public GameObject playerSword;
    public BaseBullet bulletPrefab;
    private Vector2 mousePosition;
    private Vector2 firingOrigin;
    
    private Rigidbody2D body;
    private RaycastHit2D hit;
    private bool canFire = true;

    // private enemyBehavior damaging;
    // private weaponInfo useClip;

    // Update is called once per frame
    private void Update() 
    {
        if (Input.GetAxisRaw("Fire1") != 0) 
            Fire();
        if (Input.GetAxisRaw("Fire2") != 0)
            Slash();
    }

    public void Fire() 
    {
        if (canFire) 
        {
            StartCoroutine(fireEnumerator());
        }
    }

    public void Slash() 
    {
        if (canFire)
            StartCoroutine(slashEnumerator());
    }

    IEnumerator slashEnumerator() 
    {
        playerSword.SetActive(true);
        canFire = false;
        yield return new WaitForSeconds(0.5f);
        canFire = true;
        playerSword.SetActive(false);
    }

    IEnumerator fireEnumerator() 
    {
        BaseBullet bullet = Instantiate(bulletPrefab, firingPoint);
        bullet.transform.SetParent(null);
        canFire = false;
        yield return new WaitForSeconds(0.3f);
        canFire = true;
    }
}
