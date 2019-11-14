using UnityEngine;

public class Pausing : MonoBehaviour
{
    private godScript god;
    public StrainBar strainBarScript;
    public CharacterControl characterControl;
    public GameOver gameOver;
    Animator anim;

    private bool justChanged;

    private void Start()
    {
        god = this.GetComponent<godScript>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>().animator;
        justChanged = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            god.enabled = false;
            characterControl.enabled = false;
            anim.SetBool("strain", true);
            strainBarScript.fillPercentage -= 0.15f;

            if (strainBarScript.fillPercentage <= 0f)
            {
                gameOver.killed = true;
            }
        }
        else if (justChanged)
        {
            justChanged = false;

            god.enabled = true;
            characterControl.enabled = true;
            anim.SetBool("strain", false);
        }
        else
        {
            if (strainBarScript.fillPercentage <= 100f)
            {
                strainBarScript.fillPercentage += 0.3f;
            }
        }
    }
}
