using UnityEngine;
using UnityEngine.XR;

public class LanzarEnemy : MonoBehaviour
{

    public GameObject prefabBall;
    private GameObject ball;
    [SerializeField] private GameObject hand;
    public float fuerzaBola = 1000f; // Fuerza con la que se lanzará la bola
    private float fuerzaActual = 1f;


    public void lanzar()
    {
        if (ball != null)
        {
            ball.transform.SetParent(null); // Desvincula la bola del jugador
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.isKinematic = false; // Asegura que la bola no sea kinemática
            rb.useGravity = true; // Activa la gravedad para la bola
            rb.AddForce(transform.forward * fuerzaActual * fuerzaBola);
            Destroy(ball, 5f); // Destruye la bola después de 5 segundos para limpiar la escena
            ball = null; // Limpia la referencia a la bola en la mano
        }
    }
    public void inicio()
    {
        if (ball == null)
        {
            prefabBall.transform.localScale = new Vector3(2f, 2f, 2f); // Ajusta el tamaño de la bola
            ball = Instantiate(prefabBall, hand.transform.position, Quaternion.identity, hand.transform); // Instancia la bola como hijo de la mano
                                                                                                          //ball = hand.transform.GetChild(0).gameObject; // Asume que la bola es el primer hijo de la mano
        }
    }
}
