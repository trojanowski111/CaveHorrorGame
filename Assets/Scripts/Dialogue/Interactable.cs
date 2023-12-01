using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteractEvent;
    [SerializeField] private UnityEvent<Transform> onNearbyEvent;

    public void SetInteractEvent()
    {
        onInteractEvent?.Invoke();
    }
    public void SetNearbyEvent(Transform target)
    {
        onNearbyEvent?.Invoke(target);
    }
}
