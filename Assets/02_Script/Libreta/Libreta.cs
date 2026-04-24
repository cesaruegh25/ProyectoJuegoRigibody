using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Libreta : MonoBehaviour
{
    public List<Note> notas = new List<Note>();

    public int pagActual = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNote(Note note)
    {
        notas.Add(note);
    }


    public void ActiveNextPag()
    {
        if (pagActual < notas.Count)
        {
            foreach (var nota in notas)
            {
                if (notas.IndexOf(nota) == pagActual || notas.IndexOf(nota) == (pagActual + 1))
                {
                    nota.gameObject.SetActive(true);
                }
                else
                    nota.gameObject.SetActive(false);
            }
            pagActual *= 2;
        }
    }
    public void ActivePrevPag()
    {
        if (pagActual > 0)
        {
            foreach (var nota in notas)
            {
                if (notas.IndexOf(nota) == pagActual || notas.IndexOf(nota) == (pagActual + 1))
                {
                    nota.gameObject.SetActive(true);
                }
                else
                    nota.gameObject.SetActive(false);
            }
            pagActual -= 2;
        }
    }
}
