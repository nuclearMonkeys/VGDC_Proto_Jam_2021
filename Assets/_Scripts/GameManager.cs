using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;

    public bool bFirstTimeInLevel = true;
	private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        { 
            Destroy(gameObject);
            return;
        }
	}

    private void Start()
    {
        GameManager.Instance.StartScene();
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
    public void OnPlayerDeath()
	{
        bFirstTimeInLevel = false;
        
        //when player dies  display the lose condition dialogue according to sceneIndex
        DialogueManager dialogue = GameObject.FindWithTag("Canvas").GetComponent<DialogueManager>();
        //Debug.Log("fuk u");
        
        if (dialogue != null)
        {
            StartCoroutine(dialogue.GetConditionLines(SceneManager.GetActiveScene().buildIndex, false));
        }
        else
            Debug.LogWarning("DialogueManager is null");
	}
    public void OnEnemyDeath(int sceneIndex)
	{
        GameObject dialogue = GameObject.Find("Dialogue");

        dialogue.transform.GetChild(0).gameObject.SetActive(true);
        //dialogue.SetActive(true);

        DialogueManager dialogueManager = dialogue.GetComponent<DialogueManager>();
       
        if (dialogue != null)
        {
            StartCoroutine(dialogueManager.GetConditionLines(SceneManager.GetActiveScene().buildIndex, true));
        }
        else
            Debug.LogWarning("DialogueManager is null");
        //play the UI dialogue for win condition
    }

    public void StartScene()
    {
        GameObject dialogue = GameObject.Find("Dialogue");
        //DialogueManager.Instance.gameObject
       
        //dialogue.SetActive(true);

        DialogueManager dialogueManager = DialogueManager.Instance.gameObject.GetComponent<DialogueManager>();

        if (bFirstTimeInLevel)
        {
            DialogueManager.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<EnemyAIBase>().bReadytoFight = false;
        }
        else
        {
            FindObjectOfType<EnemyAIBase>().bReadytoFight = true;
        }


        if (DialogueManager.Instance != null && bFirstTimeInLevel)
        {
            StartCoroutine(dialogueManager.GetLines(SceneManager.GetActiveScene().buildIndex));
        }
        else
            Debug.LogWarning("DialogueManager is null");
        //play the UI dialogue for win condition
    }

    public void RestartScene()
    {
        bFirstTimeInLevel = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            bFirstTimeInLevel = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

       
    }
    
}
