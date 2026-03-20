using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator animator;
    private float targetState = 0f;
    public float transitionSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float current = animator.GetFloat("AnimationState");
        float next = Mathf.Lerp(current, targetState, Time.deltaTime * transitionSpeed);
        animator.SetFloat("AnimationState", next);
    }

    public void SetAnimationState(int state)
    {
        targetState = (float)state;
    }
}