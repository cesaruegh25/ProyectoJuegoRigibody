using UnityEngine;
using UnityEngine.InputSystem;

public class Lanzar : MonoBehaviour
{

    public GameObject ball;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnApuntar(InputValue Value)
    {
        if (Value.isPressed)
        {
           Debug.Log("Apuntar");
        }
    }
    public void OnLanzar(InputValue Value)
    {
        if (Value.isPressed)
        {
            /*GameObject newBall = Instantiate(ball, transform.position + transform.forward, Quaternion.identity);
            Rigidbody rb = newBall.GetComponent<Rigidbody>();*/
            ball.transform.SetParent(null); // Desvincula la bola del jugador
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.isKinematic = false; // Asegura que la bola no sea kinemática
            rb.useGravity = true; // Activa la gravedad para la bola
            rb.AddForce(transform.forward * 500f);
        }
    }
}
