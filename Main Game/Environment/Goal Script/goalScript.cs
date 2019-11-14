using UnityEngine;
using UnityEngine.SceneManagement;

public class goalScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
