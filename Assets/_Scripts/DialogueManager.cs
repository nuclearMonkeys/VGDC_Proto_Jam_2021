using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI char_name = null; // Character's name
    [SerializeField] private TextMeshProUGUI char_line = null; // Character's dialogue line
    [SerializeField] private TextMeshProUGUI input_reminder = null; // Input Reminder
    [SerializeField] private float time = 0.05f;
    
    [SerializeField] private TextAsset file = null; // File with character dialogue
    private string[] lines = new string[128]; // Array of all the lines
    private int[] start_indices = new int[] {5, 9, 18, 29, 40, 52, 68, 83};
    private int[] lose_indices = new int[] {14, 25, 36, 48, 64};
    private int[] win_indices = new int[] {16, 27, 38, 50, 66};
    private int reset = 0;
    private int position = 4;
    
    private Regex line_regex = new Regex("^#(?'character'.*): (?'dialogue'.*)$", RegexOptions.Compiled);
    private Regex lose_regex = new Regex("^\\(LOSE\\): (?'dialogue'.*)$", RegexOptions.Compiled);
    private Regex win_regex = new Regex("^\\(WIN\\): (?'dialogue'.*)$", RegexOptions.Compiled);
    
    public void Awake()
    {
        lines = GetLinesFromFile(file);
    }
    
    //---------------------------------------------------
    // UI FUNCTIONS
    //---------------------------------------------------
    public void ShowDialogueBox(bool show)
    {
        this.gameObject.SetActive(show);
    }
    
    public void ShowInputReminder(bool show)
    {
        if (show) input_reminder.text = "enter space to continue";
        else input_reminder.text = "";
    }
    
    private void UpdateCharacterName (string name)
    {
        char_name.text = name;
    }
    
    private IEnumerator UpdateCharacterLine (string line)
    {
        ShowInputReminder(false);
        for (int i = 0; i < line.Length; i++)
        {
            char_line.text = line.Substring(0, i);
            yield return new WaitForSeconds(time);
        }
        char_line.text = line;
        yield return new WaitForSeconds(time);
        ShowInputReminder(true);
        
        do
        {
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Space));
    }
    
    private void ClearDialogue ()
    {
        char_name.text = "";
        char_line.text = "";
    }
    
    //---------------------------------------------------
    // FILE READING FUNCTIONS
    //---------------------------------------------------
    private string[] GetLinesFromFile (TextAsset file)
    {
        return Regex.Split(file.text, "\n");
    }
    
    //---------------------------------------------------
    // REGEX FUNCTIONS
    //---------------------------------------------------
    private string FindRegexMatch (string line, Regex pattern, string group)
    {
        if (!pattern.IsMatch(line)) return "";
        GroupCollection groups = pattern.Match(line).Groups;
        return groups[group].Value;
    }
    
    // Count from 0
    public IEnumerator GetLines(int level)
    {
        ShowDialogueBox(true);
        
        // Error-checking
        Debug.Log(level);
        if (level < 0 || level >= start_indices.Length - 1) yield break; 
        position = start_indices[level];
        
        string temp = "";
        string name = "";
        string line = "";
        for (int i = position; i < start_indices[level + 1]; i++)
        {
            temp = lines[i];

            if (string.IsNullOrEmpty(temp)) continue;
            name = FindRegexMatch(lines[i], line_regex, "character");
            line = FindRegexMatch(lines[i], line_regex, "dialogue");
            
            if (!string.IsNullOrEmpty(name)) UpdateCharacterName(name);
            if (!string.IsNullOrEmpty(line)) yield return StartCoroutine(UpdateCharacterLine(line));
        }
        ClearDialogue();
        ShowDialogueBox(false);
    }
    
    // Count from 1
    public IEnumerator GetConditionLines(int level, bool win)
    {
        if (level < 0 || level > start_indices.Length - 1) yield break;
        
        ShowDialogueBox(true);
        if (win) yield return StartCoroutine(UpdateCharacterLine(FindRegexMatch(lines[level - 1], win_regex, "dialogue")));
        else yield return StartCoroutine(UpdateCharacterLine(FindRegexMatch(lines[level - 1], lose_regex, "dialogue")));
        ClearDialogue();
        ShowDialogueBox(false);
    }
}
