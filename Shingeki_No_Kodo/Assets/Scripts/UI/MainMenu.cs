using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PassaggioScene.Instance.StartFadeToOpaque(
            ()=>
            {
                SceneManager.LoadScene(1);
                PassaggioScene.Instance.StartFadeToTransparent();
            });
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
