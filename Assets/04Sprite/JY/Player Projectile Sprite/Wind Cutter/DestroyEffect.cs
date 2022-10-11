using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Over()
    {
        Destroy(this.gameObject);
    }
    void off()
    {
        this.gameObject.SetActive(false);
    }
}
