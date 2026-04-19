using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTake : MonoBehaviour
{
    public bool objectTaken = false; // Variable para indicar si el objeto está siendo agarrado

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGrab(InputValue value)
    {
        if (value.isPressed)
        {
            // Aquí puedes agregar la lógica para lo que sucede cuando el jugador presiona la tecla de agarrar
            objectTaken = true; // Reiniciar la variable de rango
        }
        // usar una coroutine para esperar sin convertir el método en un iterador
        StartCoroutine(ResetGrabAfterDelay(1f));
    }

    private IEnumerator ResetGrabAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        objectTaken = false; // Reiniciar la variable de agarre después del retraso
    }
}
