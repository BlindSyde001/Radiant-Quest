using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : InteractableController
{
    public string npcName;
    public List<string> dialogue = new List<string>{};

    public override void PlayerInteract() {
        if(!UIController.Instance.isDialogueActive()) {
            UIController.Instance.StartDialogue(npcName, dialogue);
        }
    }
}
