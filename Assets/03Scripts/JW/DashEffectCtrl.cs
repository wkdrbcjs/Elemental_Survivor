using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectCtrl : MonoBehaviour
{
    public GameObject TraceTarget;
    // Start is called before the first frame update
    void Start()
    {
        TraceTarget = PlayerCtrl.playerInstance;
        Vector3 mPosition = GameObject.Find("FirePos").transform.position;
        Vector3 oPosition = GameObject.Find("FirePosPivot").transform.position; ;
       
        float dy = mPosition.y - oPosition.y;
        float dx = mPosition.x - oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        if (90 > rotateDegree && rotateDegree > -90 ) // �뽬 (�¿�)���� �� �¿� ���� 
        {
            
            GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Dashover()
    {
        Destroy(this.gameObject);
    }
    void over()// �뽬�� ������ ��
    {
        transform.Translate(Vector2.right * 1.5f);//��ġ�� ���� ������ �ű�(�װ� �� �� �ڿ���������) 
        GetComponent<SpriteRenderer>().flipX = true;// �¿� ����
    }
}
