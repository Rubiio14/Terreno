using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanics : MonoBehaviour
{
    //Audio
    [SerializeField] AudioSource m_ShootingEffect;
    //Muzzle
    [SerializeField] GameObject Muzzle;
    //Shooting & Aiming   
    [SerializeField] GameObject m_Bullet;
    [SerializeField] GameObject m_ScopeDirection;
    public float m_BulletSpeed = 10f;

    
    void Start()
    {
        ObjectPool.PreLoad(m_Bullet, 5);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_ShootingEffect.Play();
            Muzzle.SetActive(true);
            Shoot(m_Bullet, m_ScopeDirection);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            Muzzle.SetActive(false);
        }
    }
    ///Shoot and Aim
    void Shoot(GameObject bulletType, GameObject originObject)
    {

        GameObject m_Bullet2 = ObjectPool.GetObject(bulletType);
        m_Bullet2.transform.position = transform.position;

        ///Aiming Direction
        Vector3 direction = originObject.transform.forward;

        ///Apply Bullet Speed
        m_Bullet2.GetComponent<Rigidbody>().velocity = direction * m_BulletSpeed;

       
        StartCoroutine(RecicleObject(bulletType, m_Bullet2, 2.0f));
    }

    IEnumerator RecicleObject(GameObject bulletType, GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(bulletType, bullet);
    }
}