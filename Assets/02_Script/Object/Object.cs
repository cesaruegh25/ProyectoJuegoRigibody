using System;
using System.Collections;
using UnityEngine;


public class Object : MonoBehaviour
{

    public TextMesh text; // Referencia al componente TextMeshProUGUI
    public GameObject player; // Referencia al jugador
    public bool objectInRange = false; // Variable para verificar si el jugador está en rango del objeto
    public bool isGrabbed = false; // Variable para verificar si el objeto está siendo agarrado
    public Transform mano;
    private GameObject originalPosition; // Variable para almacenar la posición original del objeto
    public Transform codo;
    public Transform cabeza;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mano = GameObject.FindGameObjectWithTag("mano").transform;
        originalPosition = transform.gameObject; // Guardar la posición original del objeto
        Debug.Log("Posición original del objeto: " + originalPosition.transform.position);
        codo = GameObject.FindGameObjectWithTag("codo").transform;
        cabeza = GameObject.FindGameObjectWithTag("cabeza").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.GetComponent<PlayerTake>().objectTaken && objectInRange)
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
                player.gameObject.GetComponent<PlayerLook>().inspeccionar = false;
            }
            player.gameObject.GetComponent<PlayerTake>().objectTaken = false; // Reiniciar la variable de agarre
        }
        if(isGrabbed)
        {
            objectInRange = true; // El jugador está en rango del objeto
            float lookY = player.gameObject.GetComponent<PlayerLook>().lookInput.x *
                    player.gameObject.GetComponent<PlayerLook>().mouseSensivity;
            float LookX = player.gameObject.GetComponent<PlayerLook>().lookInput.y *
                    player.gameObject.GetComponent<PlayerLook>().mouseSensivity;
            transform.Rotate(LookX, lookY, 0, Space.Self);// Space,Self es para que gire sobre si mismo
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes agregar la lógica para lo que sucede cuando el jugador entra en contacto con el objeto
            //Debug.Log("El jugador ha entrado en contacto con el objeto.");
            text.text = "[Q]";
            text.gameObject.SetActive(true); // Mostrar el texto
            objectInRange = true; // El jugador está en rango del objeto
        }
    }
    public void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false); // Ocultar el texto cuando el jugador sale del área
        objectInRange = false; // El jugador ya no está en rango del objeto
    }
    public void TakeObject()
    {
        text.gameObject.SetActive(false); // Ocultar el texto cuando el jugador sale del área
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<PlayerLook>().AcivarCamara(1);
        cabeza.transform.localPosition = new Vector3(0f, 0f, 0f);
        cabeza.transform.localRotation = new Quaternion(0.0956106037f, -0.0613357387f, 0.0817529634f, 0.990158081f);
        codo.transform.localPosition = new Vector3(-0.24f, 0.15f, 0.23f);
        codo.transform.localRotation = new Quaternion(0.767379701f, -0.329159319f, 0.502281785f, 0.224712208f);
        Debug.Log("El jugador ha agarrado el objeto.");
        transform.SetParent(mano);
        transform.localPosition = new Vector3(0, 0, 0); // Ajustar la posición local del objeto para que esté en la mano del jugador
    }
    public void DropObject()
    {
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerLook>().AcivarCamara(0);
        Debug.Log("El jugador ha soltado el objeto.");
        transform.SetParent(null); // Desvincular el objeto del jugador
        transform.position = originalPosition.transform.position;
        transform.rotation = originalPosition.transform.rotation;
    }
}
