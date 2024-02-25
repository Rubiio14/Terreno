using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba_Disparo : MonoBehaviour
{
    [SerializeField]
    GameObject Muzzle; 
    [SerializeField]
    GameObject balaTipo1;
    [SerializeField]
    GameObject balaTipo2;
    [SerializeField]
    GameObject m_ScopeDirection;

    public float m_BulletSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.PreLoad(balaTipo1, 5);
        ObjectPool.PreLoad(balaTipo2, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Muzzle.SetActive(true);
            Shoot(balaTipo1, m_ScopeDirection);
        }
        else if(Input.GetKeyUp(KeyCode.Z))
        {
            Muzzle.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Muzzle.SetActive(true);
            Shoot(balaTipo2, m_ScopeDirection);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            Muzzle.SetActive(false);
        }
    }
   
    void Shoot(GameObject bulletType, GameObject originObject)
    {
        GameObject bullet = ObjectPool.GetObject(bulletType);
        bullet.transform.position = transform.position;

        // Definir el origen del rayo en la posición del objeto especificado
        Vector3 m_RayOrigin = originObject.transform.position;

        //Raycast
        RaycastHit m_Hit;
        if (Physics.Raycast(transform.position, transform.forward, out m_Hit))
        {
            // Si el rayo golpea algo, establecer la dirección hacia el punto de impacto
            Vector3 m_Direction = (m_Hit.point - m_RayOrigin).normalized;
            bullet.GetComponent<Rigidbody>().velocity = m_Direction * m_BulletSpeed;
        }
        else
        {
            // Si el rayo no golpea nada, disparar en la dirección hacia adelante
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * m_BulletSpeed;
        }
        // Obtener la dirección en la que está mirando la nave
        Vector3 direction = transform.forward;

        // Aplicar impulso en la dirección de la nave
        bullet.GetComponent<Rigidbody>().velocity = direction * m_BulletSpeed;

        // Alinear la bala con la dirección de disparo (en el eje Z)
        bullet.transform.rotation = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);

        // Iniciar la rutina de reciclaje de la bala
        StartCoroutine(RecicleObject(bulletType, bullet, 2.0f));
    }


    IEnumerator RecicleObject(GameObject bulletType, GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(bulletType, bullet);
    }
    



}