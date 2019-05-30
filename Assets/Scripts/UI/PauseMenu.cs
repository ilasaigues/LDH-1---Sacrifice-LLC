using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public GameObject view;
    // Start is called before the first frame update
    void Start()
    {

    }

    bool pressing = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Cancel") > Mathf.Epsilon)
        {
            if (!pressing)
            {
                TimeManager.paused = !TimeManager.paused;
                view.SetActive(TimeManager.paused);
                pressing = true;
            }
        }
        else
        {
            pressing = false;
        }
    }

    public void Resume()
    {
        view.SetActive(false);
        TimeManager.paused = false;
    }

    public void Retry()
    {
        TimeManager.paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit2Menu()
    {
        TimeManager.paused = false;
        SceneManager.LoadScene(0);
    }

    public void Quit2Desktop()
    {
        TimeManager.paused = false;
        Application.Quit();
    }
}

