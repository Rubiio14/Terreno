using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEnding : MonoBehaviour
{
     
    public float m_SoundDelayTime = 0.539f;
    public GameObject m_GameOverScreen;
    public GameObject m_GameOverScreenHUD;
    [SerializeField]
    AudioSource m_ButtonSound;
    

    /*Reset the game*/
    public void Reiniciar()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        StartCoroutine(SoundDelay_1(m_SoundDelayTime));


    }
    /*Main Menu button*/
    public void MainMenu()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        StartCoroutine(SoundDelay_2(m_SoundDelayTime));
    }


    /*Shows Death Canvas*/
    public void ActivateGameOverScreen()
    {
        m_GameOverScreen.SetActive(true);
        StartCoroutine(UIDelay(m_SoundDelayTime));

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
    IEnumerator UIDelay(float time)
    {

        yield return new WaitForSeconds(time);
        m_GameOverScreenHUD.SetActive(true);// Tiempo de espera entre disparos

    }

}
