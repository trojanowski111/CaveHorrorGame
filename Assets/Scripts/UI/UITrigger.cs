using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UITrigger", menuName = "UI/State trigger", order = 0)]
public class UITrigger : ScriptableObject
{
    public Action showEvent = delegate {};

    public void TriggerUI()
    {
        showEvent.Invoke();
    }
}
