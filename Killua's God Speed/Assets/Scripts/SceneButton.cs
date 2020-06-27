using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneButton : Button
{
    //public SceneAsset targetScene;
    //public int levelIndex;

    // Start is called before the first frame update

    public void ChangeScene(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player has Quit :(");
    }
}
