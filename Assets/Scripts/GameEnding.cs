using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameEnding : MonoBehaviour
{
    
    public GameObject gameOverScreen;
    
   
    public void Reiniciar()
    {
        ObjectPool.ClearPool();
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        ObjectPool.ClearPool();
        SceneManager.LoadScene(0);
    }


    // Método para activar la pantalla de Game Over
    public void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);   
    }

}
