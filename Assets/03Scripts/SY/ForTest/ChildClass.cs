using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildClass : ParentClass
{
    // Start is called before the first frame update

    //순서 : C.Awake->C.ppap->P.ppap->P.start->P.ppap
    void Awake()
    {
        Debug.Log("ChildAwake");
        ppap();
        //call();
        base.call();
    }

    new protected void ppap()
    {
        Debug.Log("cHILDppap");
    }

    new void call()
    {
        Debug.Log("cHILDcall");
        ppap();
        //오버라이딩 되있어도 base에서 호출하면 base꺼가 나옴
    }
}
