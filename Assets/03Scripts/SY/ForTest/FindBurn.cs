using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBurn : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("exist : " + Target.GetComponentInChildren<ParentClass>().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
