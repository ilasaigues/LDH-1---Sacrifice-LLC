using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Director
{
    private static bool _initialized;

    static string MANAGER_SCENE_NAME = "Managers";

    public static System.Action OnManagerSceneLoaded = () => { };

    public static bool Initialized
    {
        get
        {
            if (!_initialized)
            {
                _initialized = true;
                SceneManager.LoadSceneAsync(MANAGER_SCENE_NAME, LoadSceneMode.Additive);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            return _initialized;
        }
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == MANAGER_SCENE_NAME)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            OnManagerSceneLoaded();
            Debug.Log("Manager scene loaded. All systems nominal.");
        }
    }

    public static void Initialize()
    {
        if (Initialized)
        {
            Debug.Log("Director initializing. Safe travels, friend!");
        }
    }


    private static List<AbstractManager> managers = new List<AbstractManager>();

    public static T GetManager<T>() where T : AbstractManager
    {
        foreach (var manager in managers)
        {
            if (manager is T) return manager as T;
        }
        Debug.LogWarning(string.Format("No manager of type {0} found, returning null.", typeof(T).ToString()));
        return null;
    }


    public static void SubscribeManager<T>(T newManager) where T : AbstractManager
    {
        string t = typeof(T).ToString();
        if (!managers.Contains(newManager))
        {
            managers.Add(newManager);
            Debug.Log("New manager subscribed, name: " + newManager.gameObject.name);
        }
    }
}
