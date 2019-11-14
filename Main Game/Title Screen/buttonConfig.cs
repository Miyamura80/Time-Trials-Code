using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonConfig : MonoBehaviour {

    public Animator anim;

    public void LevelSelect()
    {
        StartCoroutine(LevelSelectTransition());
	}

	public void quitGame(){
		Application.Quit ();
	}

	public void optionsButton(){
        ;
	}

    public void creditsButton()
    {
        StartCoroutine(CreditsButtonTransition());
    }

    IEnumerator LevelSelectTransition()
    {
        anim.SetTrigger("Click");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LevelSelect");
    }

    IEnumerator CreditsButtonTransition()
    {
        anim.SetTrigger("Click");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Credits");
    }
}


