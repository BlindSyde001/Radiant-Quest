using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkNPCChore : Chore
{
    public override void Evaluate(GameObject actionObject)
    {
        NPCController talkedNPC = actionObject.GetComponent<NPCController>();

        if (talkedNPC == null || talkedNPC.name != goalObjName) return;

        CompleteChore();
    }
}
