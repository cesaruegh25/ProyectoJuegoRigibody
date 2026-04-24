using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Libreta : MonoBehaviour
{
    public List<Note> notas = new List<Note>();

    public int pagActual = 0;

    public Note prefabNoteIzq;
    public Note prefabNoteDer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPrevious(InputValue value)
    {
        if (value.isPressed)
        {
            ActivePrevPag();
        }
    }
    public void OnNext(InputValue value)
    {
        if (value.isPressed)
        {
            ActiveNextPag();
        }
    }
    public void AddNote(String T, String D)
    {
        if (notas.Count % 2 == 0)
        {
            Note nuevaNota = Instantiate(prefabNoteIzq, transform);
            nuevaNota.EscribirNota(T, D, notas.Count.ToString());
            notas.Add(nuevaNota);
        }
        else
        {
            Note nuevaNota = Instantiate(prefabNoteDer, transform);
            nuevaNota.EscribirNota(T, D, notas.Count.ToString());
            notas.Add(nuevaNota);
        }
    }


    public void ActiveNextPag()
    {
        if (pagActual < notas.Count)
        {
            pagActual += 2;
            foreach (var nota in notas)
            {
                if (notas.IndexOf(nota) == pagActual || notas.IndexOf(nota) == (pagActual + 1))
                {
                    nota.gameObject.SetActive(true);
                }
                else
                    nota.gameObject.SetActive(false);
            }
            Debug.Log("Pagina actual Next: " + pagActual);
        }
    }
    public void ActivePrevPag()
    {
        if (pagActual > 0)
        {
            pagActual -= 2;
            foreach (var nota in notas)
            {
                if (notas.IndexOf(nota) == pagActual || notas.IndexOf(nota) == (pagActual + 1))
                {
                    nota.gameObject.SetActive(true);
                }
                else
                    nota.gameObject.SetActive(false);
            }
        }
        Debug.Log("Pagina actual Prev: " + pagActual);
    }
}
