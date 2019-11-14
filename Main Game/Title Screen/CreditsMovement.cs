using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMovement : MonoBehaviour
{
    private Vector2 temp = new Vector2(0f, -200f);
    public RectTransform trans;

    // Update is called once per frame
    void Update()
    {
        temp.y += 1f;
        trans.localPosition = temp;
        if (temp.y > 300f)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
