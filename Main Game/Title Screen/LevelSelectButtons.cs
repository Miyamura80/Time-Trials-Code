using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Buttons : MonoBehaviour
{

    public void LevelX()
    {
        string number = GetComponentInChildren<Text>().text;
        SceneManager.LoadScene("Level" + number);
    }

    public void Back()
    {
        SceneManager.LoadScene("Title");
    }
}
