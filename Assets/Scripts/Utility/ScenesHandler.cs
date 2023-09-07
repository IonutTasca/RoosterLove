using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesHandler
{
    public static void GoToLoginScene()
    {
       SceneManager.LoadScene(sceneBuildIndex: 0);//login
    }
    public static void GoToGameScene()
    {
        PlayerDataHandler.Instance.GetUserInventory();
        SceneManager.LoadScene(sceneBuildIndex: 1);//game
    }
}
