using UnityEngine;

public class ForcedFollowUpDialogue : MonoBehaviour
{
    public NPC npc;
    public NPCDialogue followUpDialogue;
    public Transform player;

    [Tooltip("How far the player must walk away from the NPC before the timer starts.")]
    public float walkAwayDistance = 2f;

    [Tooltip("How long after walking away before the forced dialogue triggers.")]
    public float delayBeforeForcedDialogue = 2f;

    private bool hasTalkedOnce = false;
    private bool followUpTriggered = false;
    private bool waitingToWalkAway = false;
    private float timer = 0f;

    private void OnEnable()
    {
        if (npc != null)
        {
            npc.OnDialogueEnded += HandleDialogueEnded;
        }
    }

    private void OnDisable()
    {
        if (npc != null)
        {
            npc.OnDialogueEnded -= HandleDialogueEnded;
        }
    }

    private void HandleDialogueEnded()
    {
        if (followUpTriggered) return;

        hasTalkedOnce = true;
        waitingToWalkAway = true;
        timer = 0f;
    }

    private void Update()
    {
        if (!hasTalkedOnce || followUpTriggered || !waitingToWalkAway) return;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, npc.transform.position);

        if (distance >= walkAwayDistance)
        {
            timer += Time.deltaTime;

            if (timer >= delayBeforeForcedDialogue)
            {
                TriggerForcedDialogue();
            }
        }
        else
        {
            // Player is still near the NPC - reset the timer.
            timer = 0f;
        }
    }

    private void TriggerForcedDialogue()
    {
        followUpTriggered = true;
        waitingToWalkAway = false;

        if (followUpDialogue != null)
        {
            npc.dialogueData = followUpDialogue;
        }

        // Force the NPC's own dialogue system to start, as if interacted with.
        npc.Interact();
    }

}