using UnityEngine;
using UnityEngine.Events;

public abstract class View : MonoBehaviour
{
	// [SerializeField] private UnityEvent onOpenedEvent;
    // [SerializeField] private UnityEvent onClosedEvent;
	// public bool invokeEvent = true;
	[HideInInspector] public Canvas canvas => GetComponent<Canvas>();

	public abstract void Initialize();
	public virtual void Show()
	{
		// onOpenedEvent?.Invoke();
	 	canvas.enabled = true;
	}
	public virtual void Hide()
	{
		// onClosedEvent?.Invoke();
		canvas.enabled = false;
	}
}
