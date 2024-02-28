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
        ChangeScene.changeScene(m_SoundDelayTime, 1);

    }
    /*Main Menu button*/
    public void MainMenu()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        ChangeScene.changeScene(m_SoundDelayTime, 0);
    }


    /*Shows Death Canvas*/
    public void ActivateGameOverScreen()
    {
        m_GameOverScreen.SetActive(true);
        StartCoroutine(ShowGameOverDelay(m_SoundDelayTime));

    }

    
    IEnumerator ShowGameOverDelay(float time)
    {

        yield return new WaitForSeconds(time);
        m_GameOverScreenHUD.SetActive(true);// Tiempo de espera entre disparos

    }

}
