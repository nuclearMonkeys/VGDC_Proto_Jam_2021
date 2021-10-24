using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager DM = null;
    private int level = 5;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (DM != null) StartCoroutine(DM.GetLines(level));
            level += 1;
        }
    }
}
