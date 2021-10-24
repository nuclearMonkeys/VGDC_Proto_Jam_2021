using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;

	private void Awake()
	{
        if (Instance == null)
            Instance = this;
        else
        { 
            Destroy(gameObject);
            return;
        }
	}
    
	public void UpgradePlayer(float newPlayerFireRate, float newPlayerSlashRate, int newPlayerSlashComboLimit = 1) 
    {
        PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();
        playerShooting.fireRate = newPlayerFireRate;
        playerShooting.slashRate = newPlayerSlashRate;
        playerShooting.slashComboLimit = newPlayerSlashComboLimit;
    }

    //This method is called when transitioning from one Scene to the next
    //it fades the audios for smoother transition
    public void SceneTransition()
	{

	}
    
}
