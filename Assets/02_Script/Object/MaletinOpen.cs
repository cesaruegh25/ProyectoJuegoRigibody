using System;
using System.Collections;
using UnityEngine;

public class MaletinOpen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes agregar la lógica para abrir el maletín, como reproducir una animación o cambiar el estado del objeto
            Debug.Log("¡Maletín abierto!");
            AbrirMaletin();
        }
    }

    private void AbrirMaletin()
    {
        transform.localPosition = new Vector3(0f, 0.0219999999f, -0.331999987f);
    }
    private IEnumerator Open()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de abrir el maletín
            Vector3 targetRotation = new Vector3(315, 0, 0);
            transform.Rotate(targetRotation, Space.Self);
        }

    }
}
