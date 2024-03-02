using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingBombBehaviour : MonoBehaviour

{
    

    //Prefabs
    [SerializeField] GameObject m_BombPrefab;

    //Bomb
    public float m_BombTimer = 5f;
    public bool m_CanBomb = true;
    [SerializeField]
    public AudioSource m_BombSound;
    

    void Start()
    {
        ObjectPool.PreLoad(m_BombPrefab, 1);
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2") && m_CanBomb)
        {
            m_BombSound.Play();
            Shoot(m_BombPrefab);
            m_CanBomb = false;
            StartCoroutine(BombTimer(m_BombTimer));
        }
        
            
        
    }
    
    /// <summary>
    /// Shoots the bullet
    /// </summary>
    /// <param name="m_BombPrefab"></param>
    void Shoot(GameObject m_BombPrefab)
    {

        GameObject m_Bomb2 = ObjectPool.GetObject(m_BombPrefab);
        m_Bomb2.transform.position = transform.position;
        ///Apply Bullet Speed
        m_Bomb2.GetComponent<Rigidbody>().velocity = Vector3.forward;

        StartCoroutine(RecicleObject(m_BombPrefab, m_Bomb2, 2.0f));
    }
    IEnumerator RecicleObject(GameObject m_BombPrefab, GameObject m_Bomb2, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(m_BombPrefab, m_Bomb2);
    }

    IEnumerator BombTimer(float m_BombTimer)
    {
        yield return new WaitForSeconds(m_BombTimer);
        m_CanBomb = true;
    }

}
