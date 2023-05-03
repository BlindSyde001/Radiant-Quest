using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemChore : Chore
{
    public CollectibleItemController item;

    public override void Evaluate(GameObject gameObject)
    {
        CollectibleItemController collectedItem = gameObject.GetComponent<CollectibleItemController>();

        if (collectedItem == null || collectedItem != item) return;

        CompleteChore();
    }
}
