using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        ObjectPool.ClearPool();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
