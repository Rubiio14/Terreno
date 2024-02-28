using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private static ChangeScene instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public static void changeScene(float time, int indexScene)
    {
        instance.StartCoroutine(SoundDelay(indexScene, time));
    }
    public static IEnumerator SoundDelay(int indexScene, float time)
    {

        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(indexScene);

    }
}
