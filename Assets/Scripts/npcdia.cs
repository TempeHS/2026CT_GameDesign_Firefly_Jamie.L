using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;

    public DialogueChoice[] choices;
}
[System.Serializable]
public class DialogueChoice
{
    public int DialogueIndex;
    public string[] choices;
    public int[] nextDialogueIndexs;
}