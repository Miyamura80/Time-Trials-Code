using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform followee;
    private Vector3 tempVec;

    // Start is called before the first frame update
    void Start()
    {
        tempVec.z = -10;
    }

    // Update is called once per frame
    void Update()
    {
        tempVec.x = followee.position.x;
        tempVec.y = followee.position.y;

        this.transform.position = tempVec;
    }
}
