using UnityEngine;

public class LockScript : MonoBehaviour
{
    public Sprite openLock;
    public Sprite closedLock;

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<StateStorage>().state)
        {
            this.GetComponent<SpriteRenderer>().sprite = openLock;
            this.gameObject.tag = "gimmickNonWall";
        }
        else {
            this.GetComponent<SpriteRenderer>().sprite = closedLock;
            this.gameObject.tag = "gimmick";
        }
    }
}
