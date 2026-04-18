using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (player == null)
        {
            player = GetComponent<PlayerMovement>();
        }
    }
    private void FixedUpdate()
    {
        Vector3 direccion = transform.TransformDirection(
            new Vector3(player.moveInput.x, 0, player.moveInput.y));
        animator.SetFloat("X", direccion.x);
        animator.SetFloat("Y", direccion.z);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
