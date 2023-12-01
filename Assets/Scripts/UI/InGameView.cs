using UnityEngine;

public class InGameView : View
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private UITrigger pauseTrigger;
    [SerializeField] private View pauseView;

    public override void Initialize()
    {

    }
    private void OnEnable()
    {
        pauseTrigger.showEvent += Paused;
        inputReader.pauseEvent += Paused;
    }
    private void OnDisable()
    {
        pauseTrigger.showEvent -= Paused;
        inputReader.pauseEvent -= Paused;
    }
    public override void Show()
    {
        base.Show();
        inputReader.pauseEvent += Paused;
    }
    public override void Hide()
    {
        base.Hide();
        inputReader.pauseEvent -= Paused;
    }
    private void Paused()
    {
        inputReader.EnableUIInput();
        ViewManager.Show(pauseView, true, true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
