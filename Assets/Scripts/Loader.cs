using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
        StoreScene,
        LevelsMenuScene,
        Level1Scene,
        Level2Scene,
        Level3Scene,
    }

    private static Scene targetScene;

    public static void Load(object targetScene)
    {
        if (targetScene is Scene)
        {
            Loader.targetScene = (Scene)targetScene;
        }
        else if (targetScene is string)
        {
            if (Enum.TryParse(targetScene.ToString(), out Scene scene))
            {
                Loader.targetScene = scene;
            }
            else
            {
                // Handle the case where the string does not correspond to any Scene enum value
                Debug.LogError("Invalid scene name provided.");
                return;
            }
        }
        else
        {
            // Handle the case where an unsupported type is provided
            Debug.LogError("Invalid parameter type provided.");
            return;
        }

        //Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());

    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static Scene GetCurrentScene()
    {
        string currentSceneName = GetCurrentSceneName();

        if (Enum.TryParse(currentSceneName, out Scene currentScene))
        {
            return currentScene;
        }
        else
        {
            // Handle the case where the current scene name does not correspond to any Scene enum value
            Debug.LogError("Current scene name does not correspond to any Scene enum value.");
            return Scene.MainMenuScene; // Return a default scene value here
        }
    }

    public static Scene GetSecondLevelScene()
    {
        Scene[] levelScenes = new Scene[] 
                                    { 
                                        Scene.Level1Scene,
                                        Scene.Level2Scene, 
                                        Scene.Level3Scene, 
                                    };

        if (levelScenes.Length >= 2)
        {
            return levelScenes[1]; 
        }
        else
        {
            throw new Exception("Not enough scenes defined to retrieve the second level scene.");
        }
    }

    public static bool IsNotLevelScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        return sceneName != Scene.LevelsMenuScene.ToString() && sceneName != Scene.StoreScene.ToString();
    }

}

public static class LevelNameMapping
{
    private static Dictionary<string, string> sceneToLevelNameMap = new Dictionary<string, string>
        {
            { "Level1Scene", "LEVEL 1" },
            { "Level2Scene", "LEVEL 2" },
            { "Level3Scene", "LEVEL 3" },
        };

    public static string GetCurrentLevelName()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneToLevelNameMap.TryGetValue(sceneName, out string levelName))
        {
            return levelName;
        }

        return sceneName;
    }
}
