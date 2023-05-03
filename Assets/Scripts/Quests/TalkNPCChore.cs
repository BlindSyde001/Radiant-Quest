using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkNPCChore : Chore
{
    public NPCController npc;

    public override void Evaluate(GameObject actionObject)
    {
        NPCController talkedNPC = actionObject.GetComponent<NPCController>();

        if (talkedNPC == null || talkedNPC != npc) return;

        CompleteChore();
    }
}
