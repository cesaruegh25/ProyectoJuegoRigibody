using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia; // Permite acceder desde otros scripts

    [Header("Configuración")]
    public int bolosRestantes = 10;
    public int tirosMaximos = 2;

    public TMP_Dropdown dropownBolas;
    public TextMeshProUGUI panelVictoria;
    public TextMeshProUGUI panelBolos;
    public UnityEngine.UI.Button botonReiniciar;

    public int tirosRealizados = 0;
    public bool juegoTerminado = false;
    public bool menuPrincipal = false;

    public Animator player;
    public BarreraBolos barrera;

    bool final = false;
    [Header("Puntuaciones")]
    public int bolosJugador;
    public int bolosEnemigo;

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

        // Simular que el enemigo tira después de un par de segundos
        Invoke("EjecutarTiroEnemigo", 2f);
    }
    public void EjecutarTiroEnemigo()
    {
        turnoJugador = false;
        if (tirosEnemigo <= 1)
        {
            enemigoActual.GetComponent<Animator>().SetTrigger("lanzar");
            bolosEnemigo = Random.Range(1, 11);
            Debug.Log("El enemigo ha derribado: " + bolosEnemigo);
            turnoJugador = true;
        }
        tirosEnemigo++;
    }
    void CompararResultados()
    {
        // Lógica de comparación
        if (bolosRestantes <= bolosEnemigo)
        {
            Debug.Log("¡Ganaste al enemigo!");
            if (enemigoActual.name.Contains("Ch06"))
            {
                MostrarFinalJuego();
                Debug.Log("¡Has completado el juego! Mostrando cinemática final...");
            }
            else
            {
                if (CinematicManager.instancia != null)
                {
                    CinematicManager.instancia.ReproducirSiguiente();
                }
                else
                {
                    Debug.LogError("Error: ¡No se encuentra el CinematicManager en la escena!");
                }
            }
        }
        else
        {
            Debug.Log("Perdiste. Reintentar.");
            FinPartida();
        }
    }
    void MostrarFinalJuego()
    {
        // Aquí activas la cinemática final y luego el HUD
        hudFinal.SetActive(true);
        Time.timeScale = 0; // Pausar el juego al final
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
        juegoTerminado = false;
        menuPrincipal = false;
        final = false;
        tirosRealizados = 0;
        bolosRestantes = 10;
        panelBolos.text = "Quedan " + bolosRestantes + " bolos.";
        procesandoResultado = false;
    }
}