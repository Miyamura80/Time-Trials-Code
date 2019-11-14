using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{

    private List<float> xmoves;
    private List<float> ymoves;
    public int frequencyKinda;
    private int count,stepNumber;
    private Transform trs;
    public GameObject pastSelf1;
    public bool warped;
    private GameObject tempPastSelf;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        xmoves = new List<float>();
        ymoves = new List<float>();
        trs = this.gameObject.transform;
        warped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (count % frequencyKinda == 0)
        {
            if (!warped)
            {
                xmoves.Add(trs.position.x);
                ymoves.Add(trs.position.y);
            }

            if (warped && (stepNumber<xmoves.Count))
            {

                tempPastSelf.transform.position = new Vector2(xmoves[stepNumber], ymoves[stepNumber]);
                stepNumber++;
            }


        }
        count++;

    }

    public void TTravel()
    {

        tempPastSelf = Instantiate(pastSelf1, new Vector2(xmoves[0], ymoves[0]), Quaternion.identity) as GameObject;
        warped = true;
        count = 0;
        stepNumber = 0;
    }
}
