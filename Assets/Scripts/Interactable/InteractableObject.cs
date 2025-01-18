using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent _onInteract;

    UnityEvent IInteractable.OnInteract
    {
        get => _onInteract;
        set => _onInteract = value;
    }

    public void Interact(GameObject newParent)
    {
        // to do:
        // pass data (parent) to Invoke method
        _onInteract.Invoke();
    }
}
