using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildClass : ParentClass
{
    // Start is called before the first frame update

    //���� : C.Awake->C.ppap->P.ppap->P.start->P.ppap
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
        //�������̵� ���־ base���� ȣ���ϸ� base���� ����
    }
}
