using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class Object : MonoBehaviour, IInteractable
{
    public GameObject player; // Referencia al jugador
    //public bool objectInRange = false; // Variable para verificar si el jugador está en rango del objeto
    public bool isGrabbed = false; // Variable para verificar si el objeto está siendo agarrado
    public Transform mano;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public Transform codo;
    public Transform cabeza;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mano = GameObject.FindGameObjectWithTag("mano").transform;

        // 2. Guarda los valores exactos en el Start
        startPosition = transform.position;
        startRotation = transform.rotation;

        //Debug.Log("Posición original del objeto: " + startPosition);
        codo = GameObject.FindGameObjectWithTag("codo").transform;
        cabeza = GameObject.FindGameObjectWithTag("cabeza").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
            player.gameObject.GetComponent<PlayerMovement>().movimiento = false;
            float lookY = player.gameObject.GetComponent<PlayerLook>().lookInput.x *
                    player.gameObject.GetComponent<PlayerLook>().mouseSensivity;
            float LookX = player.gameObject.GetComponent<PlayerLook>().lookInput.y *
                    player.gameObject.GetComponent<PlayerLook>().mouseSensivity;
            transform.Rotate(LookX, lookY, 0, Space.Self);// Space,Self es para que gire sobre si mismo
        }
    }
    public void Interact()
    {
        if (!isGrabbed)
        {   
            TakeObject();
            isGrabbed = true;
            player.gameObject.GetComponent<PlayerLook>().inspeccionar = true;
        }
        else if (isGrabbed)
        {
            DropObject();
            isGrabbed = false;
            player.gameObject.GetComponent<PlayerMovement>().movimiento = true;
            player.gameObject.GetComponent<PlayerLook>().inspeccionar = false;
        }
        //player.gameObject.GetComponent<PlayerTake>().objectTaken = false;
    }
    /*public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes agregar la lógica para lo que sucede cuando el jugador entra en contacto con el objeto
            //Debug.Log("El jugador ha entrado en contacto con el objeto.");
            interactuar.text = "[Q]";
            interactuar.gameObject.SetActive(true); // Mostrar el texto
            objectInRange = true; // El jugador está en rango del objeto
        }
    }
    public void OnTriggerExit(Collider other)
    {
        interactuar.gameObject.SetActive(false); // Ocultar el texto cuando el jugador sale del área
        objectInRange = false; // El jugador ya no está en rango del objeto
    }*/
    public void TakeObject()
    {
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<PlayerLook>().AcivarCamara(1);
        cabeza.transform.localPosition = new Vector3(0f, 0f, 0f);
        cabeza.transform.localRotation = new Quaternion(0.0637720451f, -0.116740845f, 0.0192111824f, 0.990926683f); // 20 field of view
        codo.transform.localPosition = new Vector3(-0.24f, 0.15f, 0.23f);
        codo.transform.localRotation = new Quaternion(0.767379701f, -0.329159319f, 0.502281785f, 0.224712208f);
        Debug.Log("El jugador ha agarrado el objeto.");
        transform.SetParent(mano);
        transform.localPosition = new Vector3(-0.0115999999f, 0.0648000017f, 0.0948000029f); // Ajustar la posición local del objeto para que esté en la mano del jugador
    }
    public void DropObject()
    {
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerLook>().AcivarCamara(0);
        Debug.Log("El jugador ha soltado el objeto.");
        transform.SetParent(null); // Desvincular el objeto del jugador
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
