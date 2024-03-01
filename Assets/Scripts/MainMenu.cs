using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public float m_SoundDelayTime;
    [SerializeField]
    AudioSource m_ButtonSound;

    ///Play Button
    public void Play()
    {
        m_ButtonSound.Play();
        ObjectPool.ClearPool();
        ChangeScene.changeScene(m_SoundDelayTime, 1);       
    }

    ///Exit Button
    public void Quit()
    {
        m_ButtonSound.Play();
        StartCoroutine(SoundDelay_2(m_SoundDelayTime));
        
    }

    IEnumerator SoundDelay_2(float time)
    {

        yield return new WaitForSeconds(time);
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
        

    }
}
