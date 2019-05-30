using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public System.Action OnClose = () => { };

    void Update()
    {
        if (Input.GetAxis("Cancel") > Mathf.Epsilon)
        {
            OnClose();
            gameObject.SetActive(false);
        }
    }


}
