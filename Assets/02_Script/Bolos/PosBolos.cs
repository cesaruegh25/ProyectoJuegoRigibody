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
        for (int i = 0; i < posBolos.Count; i++)
        {
            GameObject bolo = Instantiate(boloPrefab, posBolos[i].position, Quaternion.identity);
            bolos.Add(bolo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
