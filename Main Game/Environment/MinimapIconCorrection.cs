using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconCorrection : MonoBehaviour
{
    private SpriteRenderer SelfRenderer;
    private SpriteRenderer ParentRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ParentRenderer = this.transform.parent.gameObject.GetComponent<SpriteRenderer>();
        SelfRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SelfRenderer.sprite = ParentRenderer.sprite;
    }
}
