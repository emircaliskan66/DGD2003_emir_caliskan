using UnityEngine;

public class AnimButton : MonoBehaviour
{
    public int stateValue;
    public NPCController npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npc.SetAnimationState(stateValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npc.SetAnimationState(0);
        }
    }
}