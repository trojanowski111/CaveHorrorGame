using UnityEngine;
using UnityEngine.Events;
public class Trigger : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;
    
    [ContextMenu("TRIGGER")]
    public void ActivateTrigger()
    {
        triggerEvent?.Invoke();
    }
}
