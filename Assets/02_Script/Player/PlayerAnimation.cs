using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
    private void FixedUpdate()
    {
        /*Vector3 vWorrld = rb.linearVelocity;
        Vector3 vLocal = transform.InverseTransformDirection(vWorrld);
        animator.SetFloat("X", vLocal.x);
        animator.SetFloat("Y", vLocal.z);*/

    }

    void Update()
    {
        
    }
    public void AnimcionLanzar()
    {
        animator.SetTrigger("Lanzar");
    }
}
