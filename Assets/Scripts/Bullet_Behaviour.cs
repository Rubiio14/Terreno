using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Behaviour : MonoBehaviour
{
    [SerializeField]
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Terrain")
        {
            this.gameObject.SetActive(false);
        }
    }
}
