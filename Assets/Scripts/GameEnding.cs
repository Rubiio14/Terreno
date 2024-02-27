using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameEnding : MonoBehaviour
{
    public float m_SoundDelayTime = 0.539f;
    public GameObject gameOverScreen;
    public GameObject gameOverScreenHUD;
    [SerializeField]
    AudioSource m_ButtonSound;
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


    // Método para activar la pantalla de Game Over
    public void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
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
        gameOverScreenHUD.SetActive(true); ;// Tiempo de espera entre disparos

    }
}
