using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCdialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool istyping, isDialogueActive;

}
