using UnityEngine;

public class Object : MonoBehaviour, ITake
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes agregar la lógica para lo que sucede cuando el jugador entra en contacto con el objeto
            Debug.Log("El jugador ha entrado en contacto con el objeto.");
        }
    }
}
