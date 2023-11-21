using UnityEngine;

public class NPCHeadLook : MonoBehaviour
{
    [SerializeField] private Transform npcHead;
    [SerializeField] private float headLookSpeed;

    [SerializeField] private float xClamp;
    [SerializeField] private float yClamp;

    public void LookAt(Transform lookPosition)
    {
        if(Mathf.Abs(npcHead.rotation.y) > yClamp || Mathf.Abs(npcHead.rotation.x) > xClamp) 
        // stops from moving further past the clamp but it will spin all the way back if in clamp again, shit bugged
        return;

        Vector3 targetDirection = lookPosition.position - npcHead.position;

        Vector3 newDirection = Vector3.RotateTowards(npcHead.forward, targetDirection, headLookSpeed, 0.0f);

        npcHead.rotation = Quaternion.LookRotation(newDirection);
    }
}
