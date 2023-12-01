using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PauseMenuView : View
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Button resumeButton;

    public override void Initialize()
    {

    }
    private void OnEnable()
    {
        // resumeButton.onClick.AddListener(Resume);
        // inputReader.resumeEvent += Resume;
    }
    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(Resume);
        inputReader.resumeEvent -= Resume;
    }
    public override void Show()
    {
        base.Show();
        inputReader.resumeEvent += Resume;
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        resumeButton.onClick.AddListener(Resume);
    }
    public override void Hide()
    {
        base.Hide();
        inputReader.resumeEvent -= Resume;
        EventSystem.current.SetSelectedGameObject(null);
        // resumeButton.onClick.RemoveListener(Resume);
    }
    private void Resume()
    {
        Debug.Log("resume");
        inputReader.EnableGameplayInput();
        ViewManager.ShowLast(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
