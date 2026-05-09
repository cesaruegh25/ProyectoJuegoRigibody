using UnityEngine;
using UnityEngine.Playables;

public class tutorial : MonoBehaviour
{
    public PlayableDirector cine;
    public float duracionCinematica;
    float t;

    public GameObject gameObj;
    public GameObject cinematicaObj;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duracionCinematica = (float)cine.duration;
        gameObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > duracionCinematica)
        {
            gameObj.SetActive(true);
            cinematicaObj.SetActive(false);
        }
    }
}
