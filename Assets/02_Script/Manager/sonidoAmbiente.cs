using UnityEngine;

public class sonidoAmbiente : MonoBehaviour
{
    public static sonidoAmbiente instancia;

    private void Awake()
    {
        // Si ya existe uno, destruye este
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;

        DontDestroyOnLoad(gameObject);
    }
}