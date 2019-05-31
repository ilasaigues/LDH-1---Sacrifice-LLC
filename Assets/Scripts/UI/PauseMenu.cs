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
        LevelEntity.Instance.OnDefeat += OnDefeat;
        LevelEntity.Instance.OnSuccess += OnSuccess;
    }


    bool pressing = false;
    bool gameOver = false;
    void OnDefeat()
    {
        gameOver = true;
    }

    void OnSuccess(int score)
    {
        gameOver = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
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
        SceneManager.LoadScene(1);
    }

    public void Quit2Desktop()
    {
        TimeManager.paused = false;
        Application.Quit();
    }
}

