using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba_Disparo : MonoBehaviour
{
    [SerializeField]
    GameObject balaTipo1;
    [SerializeField]
    GameObject balaTipo2;
   

    public int Speed = 130;

    

    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaTipo1, 5);
        ObjectPool.PreLoad(balaTipo2, 7);

       
        

    }

    // Update is called once per frame
    void Update()
    {
        //raycast
        if (Physics.Raycast(transform.position, transform.TransformDirection(0, 0, 1), out RaycastHit hitinfo, 20f))
        {
            Debug.Log("Hit Something");
            Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, 1) * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, 1) * 20f, Color.green);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject Bala1 = ObjectPool.GetObject(balaTipo1);



            //Rigidbody rb = balaTipo1.GetComponent<Rigidbody>();
            Bala1.transform.position = transform.position;
            //Add force to bullet
            //rb.AddForce(transform.TransformDirection(0, 0, 1) * Speed);
            Bala1.transform.position += Vector3.forward * Speed * Time.deltaTime;
            StartCoroutine(Recicle(balaTipo1, Bala1, 2.0f));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject Bala2 = ObjectPool.GetObject(balaTipo2);

            //Rigidbody rb_2 = balaTipo2.GetComponent<Rigidbody>();
            Bala2.transform.position = transform.position;
            //Add force to bullet
            //rb_2.AddForce(transform.TransformDirection(0, 0, 1) * Speed);
            Bala2.transform.position += Vector3.forward * Speed * Time.deltaTime;
            StartCoroutine(Recicle(balaTipo2, Bala2, 2.0f));
        }
    }
    
    IEnumerator Recicle(GameObject primitive, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(primitive, go);
    }
}
