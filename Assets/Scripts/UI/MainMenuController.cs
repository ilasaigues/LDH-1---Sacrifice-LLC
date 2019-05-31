using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{

    public List<Button> menuButtons = new List<Button>();

    public LevelSelectionMenu levelSelectMenu;
    public HelpMenu helpMenu;

    private void Start()
    {
        levelSelectMenu.OnClose += OnMenuClose;
        helpMenu.OnClose += OnMenuClose;
    }

    public void ShowLevelSelect()
    {
        SetButtonsInteractable(false);
        levelSelectMenu.gameObject.SetActive(true);
    }

    public void ShowCredits()
    {
        SetButtonsInteractable(false);
        helpMenu.gameObject.SetActive(true);
        helpMenu.PrevPage();
    }

    void OnMenuClose()
    {
        SetButtonsInteractable(true);
    }

    void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in menuButtons)
        {
            button.interactable = interactable;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
