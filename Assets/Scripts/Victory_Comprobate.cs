using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Victory_Comprobate : MonoBehaviour
{
    //Enemies Counter
    [SerializeField]
    TextMeshProUGUI m_EnemiesLeftLabel;
    //Audio
    public float m_SoundDelayTime = 0.539f;
    [SerializeField]
    AudioSource m_ButtonSound;
    [SerializeField]
    AudioSource m_AmbientMusic;
    [SerializeField]
    AudioSource m_VictorySound;
    //Player
    public GameObject m_Player;
    //Canvas
    public GameObject m_VictoryScreen;
    /*If Enenemies childcount == 0, Shows Victory Canvas*/
    void Update()
    {
        m_EnemiesLeftLabel.text = transform.childCount.ToString("00") + " Enemies left";

       
        if (transform.childCount == 0 && m_VictoryScreen.activeSelf == false)
        {
            
            Cursor.lockState = CursorLockMode.None;
            m_VictorySound.Play();
            m_AmbientMusic.Stop();
            m_Player.SetActive(false);
            m_VictoryScreen.SetActive(true);

        }
    }
    /*Reset Button*/
    public void Reiniciar()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        StartCoroutine(SoundDelay_1(m_SoundDelayTime));
    }
    /*Main Menu Button*/
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
