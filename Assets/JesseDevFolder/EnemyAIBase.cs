﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Health -= Amount;
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //do shit
    }
}
