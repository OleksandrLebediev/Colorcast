using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefsSystem
{
    public PlayerPrefsSystem(string key)
    {
        _key = key;
    }

    private string _key;

    public void Save(int data)
    {
        PlayerPrefs.SetInt(_key, data);
        PlayerPrefs.Save();
    }

    public int Load()
    {
        if (PlayerPrefs.HasKey(_key))
        {
            return PlayerPrefs.GetInt(_key);
        }

        return 0;
    }
}