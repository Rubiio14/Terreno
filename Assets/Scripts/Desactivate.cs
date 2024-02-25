using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivate : MonoBehaviour
{
    [SerializeField]
    public GameObject bala;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            bala.SetActive(false);
        }
       
    }
}
