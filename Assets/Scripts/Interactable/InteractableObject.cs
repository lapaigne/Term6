using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    public GameObject parent;

    [SerializeField]
    private UnityEvent _onInteract;

    UnityEvent IInteractable.OnInteract
    {
        get => _onInteract;
        set => _onInteract = value;
    }

    public void Interact()
    {
        _onInteract.Invoke();
    }
}
