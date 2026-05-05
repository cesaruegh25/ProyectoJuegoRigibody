using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia; // Permite acceder desde otros scripts

    [Header("Configuración")]
    public int bolosRestantes = 10;
    public int tirosMaximos = 2;

    public TMP_Dropdown dropownBolas;
    public TextMeshProUGUI panelVictoria;
    public TextMeshProUGUI panelBolos;
    public Button botonReiniciar;

    public int tirosRealizados = 0;
    public bool juegoTerminado = false;
    public bool menuPrincipal = false;

    void Awake()
    {
        if (instancia == null)
        { 
            instancia = this;
        }

        // Seguridad: Solo intentamos escribir si la referencia existe en el Inspector
        if (panelBolos != null)
        {
            panelBolos.text = "Quedan " + bolosRestantes + " bolos.";
        }

        if (botonReiniciar != null)
        {
            botonReiniciar.gameObject.SetActive(false);
            // Nos aseguramos de que el botón tenga asignada la función de reiniciar
            botonReiniciar.onClick.RemoveAllListeners();
            botonReiniciar.onClick.AddListener(ReiniciarJuego);
        }
        juegoTerminado = false;
        menuPrincipal = true;
    }
    void Update()
    {
        // Comprobamos si el jugador ha perdido después de cada tiro
        if ((tirosRealizados >= tirosMaximos || bolosRestantes <= 0) && GameObject.FindGameObjectsWithTag("ball").Length == 0)
        {
            ComprobarDerrota();
        }
        if (instancia == null)
        {
            instancia = this;
        }
        if (!menuPrincipal)
        {
            dropownBolas.gameObject.SetActive(false);
        } else
        {
            dropownBolas.gameObject.SetActive(true);
        } 
    }
    public void RegistrarTiro()
    {
        tirosRealizados++;
        Debug.Log("Tiro número: " + tirosRealizados);
    }
    public void BoloDestruido()
    {
        bolosRestantes--;
        panelBolos.text = "Quedan " + bolosRestantes + " bolos.";
    }
    public void ComprobarDerrota()
    {
        Invoke("FinPartida", 2f);
    }
    void FinPartida()
    {
        menuPrincipal = true;
        if (panelVictoria != null)
        {
            if (!juegoTerminado && bolosRestantes > 0)
            {
                panelVictoria.text = "Derrota... Intenta de nuevo.";
            }
            else if (!juegoTerminado && bolosRestantes <= 0)
            {
                panelVictoria.text = "Victoriaaaaaa!!!";
            }
            juegoTerminado = true;
            botonReiniciar.gameObject.SetActive(true);
        }
    }
    
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}