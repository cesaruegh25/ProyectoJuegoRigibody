using UnityEngine;

public class Pista : MonoBehaviour, IInteractable
{
    public string nombre; // Nombre de la pista
    public string descripcion; // Descripción de la pista
    public Libreta libreta;
    public bool anotada = false; // Indica si la pista ya ha sido anotada en la libreta

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anotada = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (!anotada)
        {
            anotada = true;
            libreta.AddNote(nombre, descripcion);
            Debug.Log("Has interactuado con la pista: " + gameObject.name);
        }
    }
}
