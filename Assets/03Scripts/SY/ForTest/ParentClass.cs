using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentClass : MonoBehaviour
{
    // Start is called before the first frame update
    protected void Start()
    {
        Debug.Log("ParantStart");
        ppap();
    }

    void Awake()
    {
        Debug.Log("ParantAwake");
        
    }


    protected void ppap()
    {
        Debug.Log("ParantPPAP");
    }
    protected void call()
    {
        Debug.Log("ParantCALL)");
    ppap();
    }
}
