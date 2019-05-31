using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Initializer : MonoBehaviour
{
    public Transform loadingDisc;
    public float loadingDiscRotation = 90;
    public float minLoadTime = 5;
    const string MENU_SCENE_NAME = "MainMenu";
    float loadTime;
    bool loaded = false;
    // Start is called before the first frame update
    void Awake()
    {
        Director.OnManagerSceneLoaded += ManagerSceneLoaded;
        Director.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        loadingDisc.transform.up = Quaternion.AngleAxis(loadingDiscRotation * Time.time, Vector3.forward) * Vector3.up;

        if (loaded && loadTime > minLoadTime)
        {
            SceneManager.LoadScene(MENU_SCENE_NAME);
        }
        loadTime += Time.deltaTime;
    }

    void ManagerSceneLoaded()
    {
        loaded = true;
    }
}
