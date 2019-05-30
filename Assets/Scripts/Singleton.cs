﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<T>();
            if (_instance == null) _instance = new GameObject(typeof(T).ToString() + " singleton.").AddComponent<T>();
            return _instance;
        }
    }

    private static T _instance;
}
