using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puerta : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public Animator openandclose;

    public void Interact()
    {
        isOpen = !isOpen; 
        if (isOpen)
        {
            Debug.Log("La puerta se ha abierto.");
            StartCoroutine(opening());
        }
        else
        {
            Debug.Log("La puerta se ha cerrado.");
            StartCoroutine(closing());
        }
    }

    IEnumerator opening()
    {
        print("you are opening the door");
        openandclose.Play("Opening");
        isOpen = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        print("you are closing the door");
        openandclose.Play("Closing");
        isOpen = false;
        yield return new WaitForSeconds(.5f);
    }
}
