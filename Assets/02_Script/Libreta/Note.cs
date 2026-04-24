using System;
using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public TextMeshPro titulo;
    public TextMeshPro descripcion;
    public TextMeshPro pag;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BorrarTextoHoja();
    }


    public void EscribirNota(string T, string D, string P)
    {
        titulo.text = T;
        descripcion.text = D;
        pag.text = P;
    }
    private void BorrarTextoHoja()
    {
        titulo.text = "";
        descripcion.text = "";
        pag.text = "";
    }
}
