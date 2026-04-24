using UnityEngine;

public class Pista : MonoBehaviour, IInteractable
{
    public Note prefabNote;

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
        Debug.Log("Has interactuado con la pista: " + gameObject.name);
    }
}
