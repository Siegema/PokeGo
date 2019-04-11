using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{ 
    public int gameScene;

    public void LoggingIn()
    {
        SceneManager.LoadScene(gameScene);

        int x = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(x);
    }
}
