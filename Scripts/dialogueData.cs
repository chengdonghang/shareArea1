using System;
using System.Collections.Generic;
using UnityEngine;

public class dialogueData : ScriptableObject
{
    public struct OneDialogue
    {
        public string name;
        public string dialogue;
    }
    public OneDialogue[] dialogues = new OneDialogue[1];
}
