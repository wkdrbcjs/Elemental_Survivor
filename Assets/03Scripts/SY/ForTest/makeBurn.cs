using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBurn : MonoBehaviour
{
    public GameObject fireburn;
    // Start is called before the first frame update
    void Awake()
    {
        var instance = Instantiate(fireburn,this.transform);
        instance.GetComponent<EnemyBurner>().lifetime = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
