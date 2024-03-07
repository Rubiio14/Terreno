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
    public GameObject m_Explosion;
    public int m_Nexplotions;
    //DamageSystemVariables
    public float m_HitsToDie = 4f;
    public float m_Damage = 1f;
    //EnemyLifeBar
    public EnemyLifeBar m_EnemyLifeBar;
   
    void Start()
    {
        ObjectPool.PreLoad(m_Explosion, m_Nexplotions);
    }

     
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
            //m_Explosion.transform.position = transform.position;
            //m_Explosion.Play();
            GameObject m_Explotion2 = ObjectPool.GetObject(m_Explosion);
            m_Explotion2.transform.position = transform.position;
            m_Explotion2.GetComponent<ParticleSystem>().Play();
            StartCoroutine(Explotion_Timer(m_Explosion, m_Explotion2, 2.0f));
            Destroy(gameObject);
        }
    }

    IEnumerator Explotion_Timer(GameObject m_Explosion, GameObject m_Explotion2, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RecicleObject(m_Explosion, m_Explotion2);
       
    }
}
