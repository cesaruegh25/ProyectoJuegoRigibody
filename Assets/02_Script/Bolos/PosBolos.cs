using System.Collections.Generic;
using UnityEngine;

public class PosBolos : MonoBehaviour
{
    public GameObject boloPrefab;
    public List<Transform> posBolos = new List<Transform>();
    public List<GameObject> bolos = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReiniciarBolos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReiniciarBolos()
    {
        foreach (GameObject bolo in bolos)
        {
            if (bolo != null)
            {
                Destroy(bolo);
            }
        }
        bolos.Clear();
        for (int i = 0; i < posBolos.Count; i++)
        {
            GameObject bolo = Instantiate(boloPrefab, posBolos[i].position, Quaternion.identity);
            bolos.Add(bolo);
        }
    }
}
