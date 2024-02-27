using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Skip_Animation : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;
    public float skip = 1200f;
    /*Skips animation*/
    public void Skip(float time)
    {
        playableDirector.Play();
        playableDirector.time = time;
        playableDirector.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skip(skip); 
        }
    }
}
