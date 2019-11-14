using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorButtons : MonoBehaviour
{
    public Animator anim;

    public void LevelX()
    {
        StartCoroutine(LevelXTransition());
    }

    public void Back()
    {
        StartCoroutine(BackTransition());
    }

    IEnumerator LevelXTransition()
    {
        anim.SetTrigger("Click");
        yield return new WaitForSeconds(3f);
        string number = GetComponentInChildren<Text>().text;
        SceneManager.LoadScene("Level" + number);
    }

    IEnumerator BackTransition()
    {
        anim.SetTrigger("Click");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Title");
    }
}