using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemChore : Chore
{
    public override void Evaluate(GameObject actionObject)
    {
        CollectibleItemController collectedItem = actionObject.GetComponent<CollectibleItemController>();

        if (collectedItem == null || collectedItem.name != goalObjName) return;

        CompleteChore();
    }
}
