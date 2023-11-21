using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "CaveHorrorGame/DialogueNode", order = 0)]
public class DialogueNode : ScriptableObject {

    private Action randomEvent = delegate {}; // unique for every node

}
