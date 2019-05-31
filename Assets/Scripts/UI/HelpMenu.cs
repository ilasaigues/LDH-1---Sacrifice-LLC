using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpMenu : MonoBehaviour
{
    public System.Action OnClose = () => { };

    public Transform pageContainer;

    public Button prevButton;
    public Button nextbutton;

    int CurrentPage
    {
        get
        {
            return _currentPage;
        }
        set
        {
            _currentPage = value;
            for (int i = 0; i < pageContainer.childCount; i++)
            {
                pageContainer.GetChild(i).gameObject.SetActive(i == _currentPage);
            }
            SetButtonInteractions();
        }
    }
    int _currentPage = 0;


    private void OnEnable()
    {
        SetButtonInteractions();
    }

    private void SetButtonInteractions()
    {
        prevButton.interactable = CurrentPage > 0;
        nextbutton.interactable = CurrentPage < pageContainer.childCount - 1;
    }

    void Update()
    {
        if (Input.GetAxis("Cancel") > Mathf.Epsilon)
        {
            OnClose();
            gameObject.SetActive(false);
            CurrentPage = 0;
        }
    }

    public void NextPage()
    {
        CurrentPage = Mathf.Clamp(CurrentPage + 1, 0, pageContainer.childCount - 1);
    }

    public void PrevPage()
    {
        CurrentPage = Mathf.Clamp(CurrentPage - 1, 0, pageContainer.childCount - 1);
    }
}
