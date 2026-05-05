using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public Lanzar player;
    public TMP_Dropdown dropdown;
    public List<GameObject> bolasPrefabs = new List<GameObject>();

    void Start()
    {
        //dropdown = GetComponent<TMP_Dropdown>();
        if(dropdown == null)
        {
            Debug.LogError("No se ha asignado el Dropdown en el Inspector.");
        }
        dropdown.options.Clear();
        foreach (GameObject bola in bolasPrefabs)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(bola.name));
        }
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }
    private void OnDropdownChanged(int indice)
    {
        CambiarBola(bolasPrefabs[indice]);
    }
    public void CambiarBola(GameObject nuevaBola)
    {
        GameManager.instancia.menuPrincipal = false;
        player.prefabBall = nuevaBola;
    }
}
