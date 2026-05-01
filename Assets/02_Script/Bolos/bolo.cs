using UnityEngine;

public class bolo : MonoBehaviour
{
    public bool destruido = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destruido = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball") || collision.gameObject.CompareTag("bolo"))
        {
            Debug.Log("Colision con la bola");
            Destroy(gameObject, 2f);
            if (!destruido)
            {
                if (GameManager.instancia != null)
                {
                    GameManager.instancia.BoloDestruido();
                }
                destruido = true; // Llama al método para actualizar el conteo de bolos
            }
            // Aquí puedes agregar la lógica para dañar al jugador o cualquier otra acción que desees realizar
        }
    }
}
