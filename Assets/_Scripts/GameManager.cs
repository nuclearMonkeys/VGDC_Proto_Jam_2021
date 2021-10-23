using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public void UpgradePlayer(float newPlayerFireRate, float newPlayerSlashRate, int newPlayerSlashComboLimit = 1) 
    {
        PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
        playerShooting.fireRate = newPlayerFireRate;
        playerShooting.slashRate = newPlayerSlashRate;
        playerShooting.slashComboLimit = newPlayerSlashComboLimit;
    }
}
