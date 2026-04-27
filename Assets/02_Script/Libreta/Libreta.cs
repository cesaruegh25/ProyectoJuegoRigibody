using NUnit.Framework;
using System;
using System.Collections;
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
        LibretaInicio();
    }
    public void LibretaInicio()
    {
        foreach (var nota in notas)
        {
            if (notas.IndexOf(nota) == 0 || notas.IndexOf(nota) == 1)
            {
                nota.gameObject.SetActive(true);
            }
            else
                nota.gameObject.SetActive(false);
        }
    }
    public void Update()
    {
        // detectar la fecha izquierda solo cuando se pulsa la primera vez
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            ActivePrevPag();
            Debug.Log("pagina actual: " + pagActual);
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            ActiveNextPag();
            Debug.Log("pagina actual: " + pagActual);
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
