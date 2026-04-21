using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public float distanciaInteraccion = 3f;
    public LayerMask capaInteractuable;

    [Header("UI de Interacción")]
    public GameObject panelUIInteracuable;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (panelUIInteracuable != null) panelUIInteracuable.SetActive(false);
    }
    void Update()
    {
        Vector3 origenRayo = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 direccionRayo = cam.transform.forward;

        Color colorRayo = Color.red;
        if (Physics.Raycast(origenRayo, direccionRayo, distanciaInteraccion, capaInteractuable))
        {
            if (panelUIInteracuable != null) panelUIInteracuable.SetActive(true);
            colorRayo = Color.green;
        }
        else
        {
            if (panelUIInteracuable != null) panelUIInteracuable.SetActive(false);
        }

        Debug.DrawRay(origenRayo, direccionRayo * distanciaInteraccion, colorRayo);

        cam = Camera.main;
    }
    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            TryInteract();
        }
    }
    void TryInteract()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaInteraccion, capaInteractuable))
        {
            IInteractable interactuable = hit.collider.GetComponent<IInteractable>();

            if (interactuable != null)
            {
                interactuable.Interact();
            }
        }
    }
}