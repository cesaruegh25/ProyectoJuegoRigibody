using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager instancia;

    [Header("Referencias de Objetos")]
    public GameObject objetoGameplay;

    [Header("Lista de Cinemáticas")]
    public PlayableDirector[] cinemáticas;
    public GameObject[] enemigos;

    public int indiceActual = 0;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        foreach (GameObject en in enemigos) en.SetActive(false);
    }

    public void ReproducirSiguiente()
    {
        if (indiceActual < cinemáticas.Length)
        {
            objetoGameplay.SetActive(false);
            foreach (GameObject en in enemigos) en.SetActive(false);
            GameObject cineObj = cinemáticas[indiceActual].gameObject;
            cineObj.SetActive(true);
            cinemáticas[indiceActual].Play();
            cinemáticas[indiceActual].stopped += AlTerminarCinematica;
        }
        else
        {
            Debug.Log("Fin del juego: No hay más cinemáticas.");
        }
    }

    private void AlTerminarCinematica(PlayableDirector director)
    {
        director.stopped -= AlTerminarCinematica;
        director.gameObject.SetActive(false);
        indiceActual++;
        objetoGameplay.SetActive(true);
        if (indiceActual > 0 && (indiceActual - 1) < enemigos.Length)
        {
            enemigos[indiceActual - 1].SetActive(true);
            GameManager.instancia.IniciarTurnoEnemigo(enemigos[indiceActual - 1]);
        }
        GameManager.instancia.PrepararSiguienteNivel();
    }
}