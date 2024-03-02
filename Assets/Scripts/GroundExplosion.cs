using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundExplosion : MonoBehaviour
{
    //PartycleSystem
    [SerializeField]
    public ParticleSystem m_Explosion;
    //Audio
    public AudioSource m_BombExplosion;
    public AudioSource m_BombFallSound;
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Bomb")
        {
            m_BombFallSound.Stop();
            m_BombExplosion.Play();
            m_Explosion.transform.position = transform.position;
            m_Explosion.Play();
        }
    }
}
