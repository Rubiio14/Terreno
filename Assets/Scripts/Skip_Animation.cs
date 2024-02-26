using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Skip_Animation : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    public void Skip(float time) // Función para reproducir la línea de tiempo desde un tiempo específico
    {
        playableDirector.Play();
        playableDirector.time = time;
        playableDirector.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skip(1200f); 
        }
    }
}
