using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Manager : MonoBehaviour
{
    public Text Classtext;
    // Start is called before the first frame update
    void Start()
    {
        switch (DataSet.instance.selectedClass)
        {
            case 0:
                Classtext.text = "Fire" + " LV." + DataSet.instance.classLevel[0].ToString();
                break;
            case 1:
                Classtext.text = "Earth" + " LV." + DataSet.instance.classLevel[1].ToString();
                break;
            case 2:
                Classtext.text = "Wind" + " LV." + DataSet.instance.classLevel[2].ToString();
                break;
            case 3:
                Classtext.text = "Water" + " LV." + DataSet.instance.classLevel[3].ToString();
                break;
            case 4:
                Classtext.text = "Thunder" + " LV." + DataSet.instance.classLevel[4].ToString();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
