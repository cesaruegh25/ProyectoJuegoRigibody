using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lanzar : MonoBehaviour
{
    public GameObject prefabBall;
    public GameObject arrow;
    public GameObject hand;
    private GameObject ball;
    public float fuerzaBola = 1000f; // esto es temporal cada bola tendra su fuerza, pero por ahora lo dejo fijo para probar

    public Slider barraFuerza;
    public float velocidadCarga = 1.5f; // Qué tan rápido sube la barra

    private float fuerzaActual = 0f;
    private bool cargando = false;
    private bool subiendo = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barraFuerza.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cargando)
        {
            ActualizarFuerza();
        }
    }
    public void OnApuntar(InputValue Value)
    {
        if (Value.isPressed && GameManager.instancia.tirosRealizados < GameManager.instancia.tirosMaximos
            && !GameManager.instancia.menuPrincipal)
        {
            barraFuerza.gameObject.SetActive(true);
            cargando = true;
            PlayerLook playerLook = GetComponent<PlayerLook>();
            playerLook.AcivarCamara(1);
            Debug.Log("Apuntar");
            if (ball == null)
            {
                Instantiate(prefabBall, hand.transform.position, Quaternion.identity, hand.transform); // Instancia la bola como hijo de la mano
                ball = hand.transform.GetChild(0).gameObject; // Asume que la bola es el primer hijo de la mano
            }
            //new Vector3(0.0109999999f, 0.316000015f, 0.186000004f)
            arrow.SetActive(true);
        }
    }
    public void OnLanzar(InputValue Value)
    {
        if (Value.isPressed && GameManager.instancia.tirosRealizados < GameManager.instancia.tirosMaximos 
            && !GameManager.instancia.menuPrincipal)
        {
            cargando = false;
            arrow.SetActive(false);
            GetComponent<PlayerAnimation>().AnimacionLanzar();
            barraFuerza.value = 0; // Reiniciar barra tras el tiro
            GameManager.instancia.RegistrarTiro();
        }
    }
    public void lanzar()
    {
        if (ball != null)
        {
            ball.transform.SetParent(null); // Desvincula la bola del jugador
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.isKinematic = false; // Asegura que la bola no sea kinemática
            rb.useGravity = true; // Activa la gravedad para la bola
            rb.AddForce(transform.forward * fuerzaActual * fuerzaBola);
            ball = null; // Limpia la referencia a la bola en la mano
            PlayerLook playerLook = GetComponent<PlayerLook>();
            barraFuerza.gameObject.SetActive(false);
            playerLook.AcivarCamara(0);
        }
    }
    void ActualizarFuerza()
    {
        // Esto crea un efecto de "vaivén" (sube y baja) si se mantiene mucho tiempo
        if (subiendo)
        {
            fuerzaActual += Time.deltaTime * velocidadCarga;
            if (fuerzaActual >= 1f) subiendo = false;
        }
        else
        {
            fuerzaActual -= Time.deltaTime * velocidadCarga;
            if (fuerzaActual <= 0f) subiendo = true;
        }

        // Aplicamos el valor al Slider (el slider debe tener Min Value 0 y Max Value 1)
        barraFuerza.value = fuerzaActual;
    }
}
