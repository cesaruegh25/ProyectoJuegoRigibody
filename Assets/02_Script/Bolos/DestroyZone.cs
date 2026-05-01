using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        { 
            Destroy(collision.gameObject, 2f);
            // Aquí puedes agregar la lógica para dañar al jugador o cualquier otra acción que desees realizar
        }
    }
}
