using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField]
    public GameEnding gameEnding;
    //AudioVaribales
    [SerializeField]
    AudioSource m_GameOver;
    [SerializeField]
    AudioSource m_AmbientSound;
    [SerializeField]
    AudioSource m_audioClipExplosion;
    [SerializeField]
    //ParticleVariables
    public ParticleSystem m_Explosion;
    //DamageSystemVariables
    public float m_HitsToDie = 4f;
    public float m_Damage = 1f;
    //EnemyLifeBar
    public EnemyLifeBar m_EnemyLifeBar;
   


     
    ///Class that takes life from enemies 
    public void RecieveHit()
    {
        m_HitsToDie -= m_Damage;
        m_EnemyLifeBar.m_CurrentHealth -= m_Damage;

        if (m_HitsToDie == 0f)
        {
            ///Only for Player Death
            if (gameObject.tag == "Player")
            {
                m_GameOver.Play();
                m_AmbientSound.Stop();
                gameEnding.ActivateGameOverScreen();
                Cursor.lockState = CursorLockMode.None;
            }

            m_audioClipExplosion.Play();
            m_Explosion.transform.position = transform.position;
            m_Explosion.Play();
            Destroy(gameObject);
        }
    }
}
