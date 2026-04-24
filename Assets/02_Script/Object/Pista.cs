using UnityEngine;

public class Pista : MonoBehaviour, IInteractable
{
    public string nombre; // Nombre de la pista
    public string descripcion; // Descripción de la pista
    public Libreta libreta;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        libreta.AddNote(nombre, descripcion);
        Debug.Log("Has interactuado con la pista: " + gameObject.name);
    }
}
