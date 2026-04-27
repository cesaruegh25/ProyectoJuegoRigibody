using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerNote : MonoBehaviour
{
    public bool abierto = false;
    public GameObject player; // Referencia al jugador
    public GameObject libreta; // Referencia a la libreta

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        libreta.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnNote(InputValue value)
    {
        if (value.isPressed)
        {
            abierto = !abierto;
            Debug.Log("Nota " + (abierto ? "abierta" : "cerrada"));
            if (abierto)
            {
                LibretaOpen();
            }
            else
            {
                LibretaClose();
            }
        }
    }

    private void LibretaOpen()
    {
        libreta.GetComponent<Libreta>().LibretaInicio();
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<PlayerLook>().AcivarCamara(1);
        player.GetComponent<PlayerLook>().inspeccionar = true;
        libreta.gameObject.SetActive(true);
        player.gameObject.GetComponent<PlayerMovement>().movimiento = false;
    }
    private void LibretaClose()
    {
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerLook>().AcivarCamara(0);
        player.GetComponent<PlayerLook>().inspeccionar = false;
        libreta.gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerMovement>().movimiento = true;
    }

}
