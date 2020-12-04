using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Prototype Level");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnRetry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        GameController.instance.levelWinUI.enabled = false;
        GameController.instance.levelLooseUI.enabled = false;
    }
}
