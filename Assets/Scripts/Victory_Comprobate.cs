using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory_Comprobate : MonoBehaviour
{
    public float m_SoundDelayTime = 0.539f;
    [SerializeField]
    AudioSource m_ButtonSound;
    [SerializeField]
    AudioSource m_AmbientMusic;
    [SerializeField]
    AudioSource m_VictorySound;
    [SerializeField]
    public GameObject m_Spawnpoint;
    public GameObject m_Player;
    public GameObject m_VictoryScreen;
    void Update()
    {

        if (transform.childCount == 0 && m_VictoryScreen.activeSelf == false)
        {
            
            Cursor.lockState = CursorLockMode.None;
            m_VictorySound.Play();
            m_AmbientMusic.Stop();
            m_Player.SetActive(false);
            m_VictoryScreen.SetActive(true);

        }
    }

    public void Reiniciar()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        StartCoroutine(SoundDelay_1(m_SoundDelayTime));
    }
    public void MainMenu()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        StartCoroutine(SoundDelay_2(m_SoundDelayTime));
        
    }

    IEnumerator SoundDelay_1(float time)
    {

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);// Tiempo de espera entre disparos

    }
    IEnumerator SoundDelay_2(float time)
    {

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);// Tiempo de espera entre disparos

    }
}
