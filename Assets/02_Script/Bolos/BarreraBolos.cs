using UnityEngine;

public class BarreraBolos : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivarBarrera()
    {
        animator.Play("move");
    }
    public void Idle()
    {
        animator.Play("idle");
    }
}
