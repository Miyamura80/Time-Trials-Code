using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator anim;
    public bool killed;
    private Animator playerDeathAnim;

    //TODO: Change pixels per thingy in sprite renderer
    private void Start()
    {
        killed = false;
        playerDeathAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>().animator;
    }

    void Update()
    {
        if (killed)
        {
            StartCoroutine(ReloadTransition());
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            killed = true;
        }
    }

    IEnumerator ReloadTransition()
    {
        anim.SetTrigger("Dead");
        playerDeathAnim.SetTrigger("dead");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>().enabled = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
