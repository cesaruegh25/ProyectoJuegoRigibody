using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia; // Permite acceder desde otros scripts

    [Header("Configuración")]
    public int bolosRestantes = 10;
    public int tirosMaximos = 2;

    public TMP_Dropdown dropownBolas;
    public TextMeshProUGUI panelVictoria;
    public TextMeshProUGUI panelBolos;
    public TextMeshProUGUI panelBolosEnemigo;
    public UnityEngine.UI.Button botonReiniciar;

    public int tirosRealizados = 0;
    public bool juegoTerminado = false;
    public bool menuPrincipal = false;

    public Animator player;
    public BarreraBolos barrera;

    bool final = false;
    [Header("Puntuaciones")]
    public int bolosJugador;
    private int bolosEnemigo = 10;

    [Header("UI Final")]
    public GameObject hudFinal; // El panel con el botón de "Salir"

    public GameObject enemigoActual;
    public int tirosEnemigo = 0;
    public bool turnoJugador = true;

    private bool procesandoResultado = false;

    void Awake()
    {
        if (instancia == null)
        { 
            instancia = this;
        }
        /*
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
        menuPrincipal = true;*/
    }

    private void Start()
    {
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
        Time.timeScale = 1f;
    }
    void Update()
    {
        // Comprobamos si el jugador ha perdido después de cada tiro
        if ((tirosRealizados >= tirosMaximos || bolosRestantes <= 0) && GameObject.FindGameObjectsWithTag("ball").Length == 0)
        {
            if (enemigoActual == null)
            {
                ComprobarDerrota();
            }
        }
        if (tirosRealizados >= tirosMaximos && GameObject.FindGameObjectsWithTag("ball").Length == 0)
        {
            if (enemigoActual != null && !procesandoResultado)
            {
                procesandoResultado = true;
                CompararResultados();

            }
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
    public void BoloEnemigoDestruido()
    {
        bolosEnemigo--;
        panelBolosEnemigo.text = "Bolos enemigos restantes: " + bolosEnemigo;
    }
    public void ComprobarDerrota()
    {
        if (!final)
        {
            final = true;
            Invoke("FinPartida", 3f);
        }

    }
    public void PrepararSiguienteNivel()
    {
        ReiniciarJuego();
    }
    void FinPartida()
    {
        menuPrincipal = true;
        if (!juegoTerminado && bolosRestantes > 0)
        {
            player.Play("Defeat Idle");
            botonReiniciar.gameObject.SetActive(true);
        }
        else if (!juegoTerminado && bolosRestantes <= 0)
        {
            player.Play("Victory Idle");
            CinematicManager.instancia.ReproducirSiguiente();
        }
        juegoTerminado = true;
        barrera.ActivarBarrera();
    }

    public void IniciarTurnoEnemigo(GameObject enemigo)
    {
        if(enemigoActual != enemigo)
        {
            tirosEnemigo = 0; // Reiniciar el conteo de tiros del enemigo
        }
        enemigoActual = enemigo;
        turnoJugador = false;
    }
    public void EjecutarTiroEnemigo()
    {
        turnoJugador = false;
        if (tirosEnemigo <= 1)
        {
            enemigoActual.GetComponent<Animator>().SetTrigger("lanzar");
        }
        tirosEnemigo++;
        Debug.Log("Tiro enemigo número: " + tirosEnemigo);
    }
    void CompararResultados()
    {
        // Lógica de comparación
        if (bolosRestantes <= bolosEnemigo)
        {
            /*if (enemigoActual.name.Contains("Ch06"))
            {
                MostrarFinalJuego();
            }
            else
            {*/
                if (CinematicManager.instancia != null)
                {
                    CinematicManager.instancia.ReproducirSiguiente();
                }
                else
                {
                    Debug.LogError("Error: ¡No se encuentra el CinematicManager en la escena!");
                }
            //}
        }
        else
        {
            enemigoActual.GetComponent<Animator>().SetTrigger("victory");
            Debug.Log("Perdiste. Reintentar.");
            FinPartida();
        }
    }
    public void MostrarFinalJuego()
    {
        Time.timeScale = 0; // Pausar el juego al final
        menuPrincipal = true;
        Cursor.visible = true; // Mostrar el cursor para que el jugador pueda interactuar con el botón de salir
        Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor para que pueda moverse libremente
        // Aquí activas la cinemática final y luego el HUD
        SceneManagerController.Instance.LoadScene("EndGame");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo...");
    }
    public void ReiniciarJuego()
    {
        player.Play("Idle");
        botonReiniciar.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("PosBolos").GetComponent<PosBolos>().ReiniciarBolos();
        GameObject.FindGameObjectWithTag("PosBolosEnemy").GetComponent<PosBolos>().ReiniciarBolos();
        juegoTerminado = false;
        menuPrincipal = false;
        final = false;
        tirosRealizados = 0;
        bolosRestantes = 10;
        bolosEnemigo = 10;
        panelBolos.text = "Quedan " + bolosRestantes + " bolos.";
        procesandoResultado = false;
        turnoJugador = true;
        if (enemigoActual != null)
        {
            panelBolosEnemigo.text = "Bolos enemigos restantes: " + bolosEnemigo;
            tirosEnemigo = 0;
            turnoJugador = false;
            Invoke("EjecutarTiroEnemigo", 1f);
            Debug.Log("Tiro enemigo programado reinicio");
        }
    }
}