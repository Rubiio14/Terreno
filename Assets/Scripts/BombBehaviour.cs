using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Enemy")
        {
           
            this.gameObject.SetActive(false);
        }
        
        
    }

}
