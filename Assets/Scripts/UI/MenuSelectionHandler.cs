using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuSelectionHandler : MonoBehaviour
{
	// [SerializeField] private InputReader _inputReader;
	public GameObject currentSelection;
	public GameObject mouseSelection;

	[SerializeField] private InputActionReference _navigateAction;
    [SerializeField] private InputActionReference _mouseMove;

	private void OnEnable()
	{
		_navigateAction.action.performed += HandleMoveSelection;
		_mouseMove.action.performed += HandleMoveCursor;
	}

	private void OnDisable()
	{
		_navigateAction.action.performed -= HandleMoveSelection;
		_mouseMove.action.performed -= HandleMoveCursor;
	}

	/// <summary>
	/// Fired by keyboard and gamepad inputs. Current selected UI element will be the ui Element that was selected
	/// when the event was fired. The _currentSelection is updated later on, after the EventSystem moves to the
	/// desired UI element, the UI element will call into UpdateSelection()
	/// </summary>
	private void HandleMoveSelection(InputAction.CallbackContext callbackContext)
	{
		Cursor.visible = false;

		// Handle case where no UI element is selected because mouse left selectable bounds
		if (EventSystem.current.currentSelectedGameObject == null)
			EventSystem.current.SetSelectedGameObject(currentSelection);
	}

	private void HandleMoveCursor(InputAction.CallbackContext callbackContext)
	{
		if (mouseSelection != null)
		{
			EventSystem.current.SetSelectedGameObject(mouseSelection);
		}

		Cursor.visible = true;
	}

	public void HandleMouseEnter(GameObject UIElement)
	{
		mouseSelection = UIElement;
		EventSystem.current.SetSelectedGameObject(UIElement);
	}

	public void HandleMouseExit(GameObject UIElement)
	{
		if (EventSystem.current.currentSelectedGameObject != UIElement)
			return;

		// deselect UI element if mouse moves away from it
		mouseSelection = null;
		EventSystem.current.SetSelectedGameObject(null);
	}

	/// <summary>
	/// Method interactable UI elements should call on Submit interaction to determine whether to continue or not.
	/// </summary>
	/// <returns></returns>
	public bool AllowsSubmit()
	{
		// if LMB is not down, there is no edge case to handle, allow the event to continue
		return !Mouse.current.leftButton.isPressed
			   // if we know mouse & keyboard are on different elements, do not allow interaction to continue
			   || mouseSelection != null && mouseSelection == currentSelection;
	}

	/// <summary>
	/// Fired by gamepad or keyboard navigation inputs
	/// </summary>
	/// <param name="UIElement"></param>
	public void UpdateSelection(GameObject UIElement) => currentSelection = UIElement;

	// Debug
	// private void OnGUI()
	// {
	//	 	GUILayout.Box($"_currentSelection: {(_currentSelection != null ? _currentSelection.name : "null")}");
	//	 	GUILayout.Box($"_mouseSelection: {(_mouseSelection != null ? _mouseSelection.name : "null")}");
	// }
}
