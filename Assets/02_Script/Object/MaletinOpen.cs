using System;
using System.Collections;
using UnityEngine;

public class MaletinOpen : MonoBehaviour, IInteractable
{
    public bool isOpen = false;

    private void AbrirMaletin()
    {
        transform.localPosition = new Vector3(0f, 0.0219999999f, -0.331999987f);
        StartCoroutine(Open());
        
    }
    private IEnumerator Open()
    {
        int i = 0;
        while(i < 2)
        {
            yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de abrir el maletín
            Vector3 targetRotation = new Vector3(315, 0, 0);
            transform.Rotate(targetRotation, Space.Self);
            Debug.Log("¡Maletín abierto!");
            i++;
        }
        isOpen = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void Interact()
    {
        if (!isOpen)
        {
            AbrirMaletin();
        }
    }
}
