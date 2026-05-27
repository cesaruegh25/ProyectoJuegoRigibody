using UnityEngine;

public class boloEnemigo : MonoBehaviour
{
    public bool destruido = false;
    public AudioSource audioSource; // Referencia al componente AudioSource
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destruido = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z >= 0.20 || transform.rotation.z <= -0.20)
        {
            Destroy(gameObject, 1f);
            if (!destruido)
            {
                if (GameManager.instancia != null)
                {
                    GameManager.instancia.BoloEnemigoDestruido();
                }
                destruido = true; // Llama al método para actualizar el conteo de bolos
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball") || collision.gameObject.CompareTag("bolo"))
        {
            audioSource.Play();
        }
    }
}
