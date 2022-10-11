using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaviUi : MonoBehaviour
{
    GameObject playerObject;
    public GameObject NamedMonster;
    public GameObject Portrait;

    Vector3 toVec;
    float x;
    float y;
    float angle;
    float dis;

    bool isTransOn;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = PlayerCtrl.playerInstance;
        //GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(NamedMonster!=null)
        {
            CalcTransform();
        }
        SetRotation();
        Portrait.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    void CalcTransform()
    {

        toVec = NamedMonster.transform.position - playerObject.transform.position;
        
        x = toVec.x;
        y = toVec.y;
        //x = NamedMonster.transform.position.x - PC.transform.position.x;
        //y = NamedMonster.transform.position.y - PC.transform.position.y;
        //transform.position = new Vector3(PC.transform.position.x + SetPosX(x), PC.transform.position.y + SetPosY(y), 0);
        SetPosX(x);

        //transform.position = PC.transform.position + transform.TransformDirection(Vector2.right) * dis;
        //transform.position = PC.transform.position + toVec.normalized * dis;
        
    }
    void SetRotation()
    {
        angle = Mathf.Atan2(y,x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //transform.LookAt(new Vector3(NamedMonster.transform.position.x, NamedMonster.transform.position.y,0));
    }
    void SetPosX(float m_x)
    {
        float m_X = 0;
        float m_Y = 0; 
        float MaxdistanceX = 119.64f;
        float MaxdistanceY = 67.00f;
        float m_angle = angle * Mathf.PI/180f;
        if (29.35f <= angle && angle < 150.65f)
        {
            dis = MaxdistanceY / Mathf.Sin(m_angle);
            m_X = MaxdistanceY / Mathf.Tan(m_angle);
            m_Y = MaxdistanceY;
        }
        else if (-150.65f < angle && angle < -29.35f)
        {
            dis = MaxdistanceY / Mathf.Sin(m_angle);
            m_X = -1 * MaxdistanceY / Mathf.Tan(m_angle);
            m_Y = -1 * MaxdistanceY;
        }
        else if (-29.35f < angle && angle <= 29.35f)
        {
            dis = MaxdistanceX / Mathf.Cos(m_angle);
            m_X = MaxdistanceX;
            m_Y = MaxdistanceX * Mathf.Tan(m_angle);
        }
        else if ((angle <= 180.0f && angle > 150.65f) || (angle <= -150.65f && angle > -180.0f)) //마우스가 PC보다 왼쪽에 있을 때
        {
            dis = MaxdistanceX / Mathf.Cos(m_angle);
            m_X = -1 * MaxdistanceX;
            m_Y = -1 * MaxdistanceX * Mathf.Tan(m_angle);
        }
        if (dis < 0)
        {
            dis *= -1;
        }
        transform.position = playerObject.transform.position + new Vector3(m_X, m_Y, 0);
        if (dis < toVec.magnitude)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    float SetPosY(float m_y)
    {

        float set_y;
        float Maxdistance = 50;
        if (Maxdistance < m_y && m_y > 0)
        {
            set_y = Maxdistance;
        }
        else if (-1 * Maxdistance > m_y && m_y <= 0)
        {
            set_y = -1* Maxdistance;
        }
        else
        {
            set_y = m_y;
        }
        return set_y;
    }
    
    public void SetNamedMonster(GameObject m_NamedMonster)
    {
        NamedMonster = m_NamedMonster;
    }
}
