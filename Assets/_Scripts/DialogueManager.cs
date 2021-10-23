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
    
    [SerializeField] private TextAsset file = null; // File with character dialogue
    private string[] lines = new string[128]; // Array of all the lines
    private int[] start_indices;
    private int[] lose_indices;
    private int[] win_indices;
    private int reset = 0;
    private int position = 5;
    
    private Regex line_regex = new Regex("^#(?'character'.*: )(?'dialogue'.*)$", RegexOptions.Compiled);
    private Regex lose_regex = new Regex("^\\(LOSE\\): (?'dialogue'.*)$", RegexOptions.Compiled);
    private Regex win_regex = new Regex("^\\(WIN\\): (?'dialogue'.*)$", RegexOptions.Compiled);
    
    public void Awake()
    {
        lines = GetLinesFromFile(file);
        start_indices = new int[] {5, 9, 18, 29, 40, 52, 68};
        lose_indices = new int[] {14, 25, 36, 48, 64};
        win_indices = new int[] {16, 27, 38, 50, 66};
        StartCoroutine(GetLines());
    }
    
    //---------------------------------------------------
    // UI FUNCTIONS
    //---------------------------------------------------
    public void ShowDialogueBox(bool show)
    {
        this.gameObject.SetActive(show);
    }
    
    private void UpdateCharacterName (string name)
    {
        char_name.text = name;
    }
    
    private IEnumerator UpdateCharacterLine (string line)
    {
        do
        {
            char_line.text = line;
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Mouse0));
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
    
    public IEnumerator GetLines()
    {
        string temp = "";
        string name = "";
        string line = "";
        for (int i = position; i < start_indices[reset + 1]; i++)
        {
            temp = lines[i];

            if (string.IsNullOrEmpty(temp)) continue;
            name = FindRegexMatch(lines[i], line_regex, "character");
            line = FindRegexMatch(lines[i], line_regex, "dialogue");
            
            if (!string.IsNullOrEmpty(name)) UpdateCharacterName(name);
            if (!string.IsNullOrEmpty(line)) yield return StartCoroutine(UpdateCharacterLine(line));
        }
        reset += 1;
        ClearDialogue();
    }
    
    public IEnumerator GetConditionLines(int level, bool win)
    {
        if (win) yield return StartCoroutine(UpdateCharacterLine(FindRegexMatch(lines[level - 1], win_regex, "dialogue")));
        else yield return StartCoroutine(UpdateCharacterLine(FindRegexMatch(lines[level - 1], lose_regex, "dialogue")));
        ClearDialogue();
    }
}
