using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    public GameEnding gameEnding;
    [SerializeField]
    AudioSource m_GameOver;
    [SerializeField]
    AudioSource m_AmbientSound;
    [SerializeField]
    AudioSource m_audioClipExplosion;
    [SerializeField]
    public ParticleSystem m_Explosion;
    public int m_HitsToDie = 5;

    public void RecieveHit()
    {
        m_HitsToDie--;
        if (m_HitsToDie == 0)
        {
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
