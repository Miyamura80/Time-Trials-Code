using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    NewGodScript gscript;

    // Start is called before the first frame update
    void Start()
    {
        gscript = GameObject.FindGameObjectWithTag("god").GetComponent<NewGodScript>();
    }

    // Update is called once per frame
    void Update()
    {

        this.GetComponent<UnityEngine.UI.Text>().text = ConvertUnitToGameTime(gscript.godTime);
    }

    private string ConvertUnitToGameTime(int n)
    {
        string output = "";
        int hour = n / (60 * 3600);
        if (hour < 10) {
            output += "0";
        }
        output += hour.ToString() + ":";

        n = n % (60 * 3600);
        int minute = n / 3600;
        if (minute < 10)
        {
            output += "0";
        }
        output += minute.ToString() + ":";

        n %= 3600;
        int sec = n / 60;
        if (sec < 10)
        {
            output += "0";
        }
        output += sec.ToString() + ":";


        n %= 60;
        int thirds = n;
        if (thirds < 10)
        {
            output += "0";
        }
        output += thirds.ToString();

        return output;
    }

}
