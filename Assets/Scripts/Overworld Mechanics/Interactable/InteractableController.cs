using UnityEngine;

public abstract class InteractableController : MonoBehaviour
{
    public string objName;
    [SerializeField] protected bool _interactionActive = true;

    // Custom action to call on player interaction with this object
    [SerializeField] private UnityEngine.Events.UnityEvent interactAction;

    protected abstract void Interaction();

    public void PlayerInteract()
    {
        Interaction();
        interactAction?.Invoke();
        Quest.Instance.EvaluateAction(gameObject);
    }

    public bool isInteractionActive()
    {
        return _interactionActive;
    }

    public void ActivateInteraction(bool activate)
    {
        _interactionActive = activate;
    }
}
