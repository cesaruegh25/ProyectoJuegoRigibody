using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movimiento")]
    public float speed = 5f;
    private Rigidbody rb;
    public Vector2 moveInput;
    public bool movimiento = true;

    [Header("GroundCheck")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    // crea una esfera en el object empty que he puesto en los pies y segun el radio. detecta con que toca los pies y cambia el isGrounded.(asi no hay que poner tag ground en el suelo, ni tejados etc)
    public void GroundCheck()
    {
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundRadius);
        bool grounded = false;
        foreach (Collider col in hits)
        {
            // esto es para que no detecte al player i cambie sin tocar el suelo.
            if (col.gameObject != gameObject)
            {
                grounded = true;
                //Debug.Log("toco: " + col.gameObject.name);
                break;
            }
        }
        if (grounded != isGrounded)
        {
            isGrounded = grounded;
        }
    }
    //fixedUpdate para las fisicasa
    private void FixedUpdate()
    {
        GroundCheck();
        Vector3 direccion = transform.TransformDirection(
            new Vector3(moveInput.x, 0, moveInput.y));
        Vector3 velocity = direccion * speed;
        Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        if (!GameManager.instancia.menuPrincipal)
        {
            rb.linearVelocity = newVelocity;
        }
    }
    public void desactivarMovimiento(bool move)
    {
        movimiento = move;
    }
}
