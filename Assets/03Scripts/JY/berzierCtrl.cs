using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berzierCtrl : Projectile
{
  
    Vector3[] m_points = new Vector3[4];

    private float m_timerMax = 0;
    private float m_timerCurrent = 0;
    private float m_speed;
    Vector3 temp;

    void Start()
    {
        ItemInfoInit(8);


        defaultInit();

        transform.localScale = new Vector3(projectileScale, projectileScale, 1);


        NearestGameObject();       
        Init(transform, Monster.transform, projectileSpeed, 30, 40);

        _SM.SoundPlay(_SM.Wind_Cutter,0.5f);
    }

    void Update()
    {
        if (m_timerCurrent > m_timerMax)
        {
            if (Monster != null && !hit) 
            {
                if (Monster.GetComponent<EnemyCtrl>().isDead == false)
                {
                    //Monster.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    if (CalculateCritical(criticalChance))
                    {
                        Monster.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)(attackDamage * criticalDamage), knockBack, CriticalColor, true);
                    }
                    if(!CalculateCritical(criticalChance))
                    {
                        Monster.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                    }
                    hit = true;
                }
                
            }
            GetComponent<Animator>().SetTrigger("Impact");
            //Over();
            return;
        }
        
        if (Monster != null)
        {
            if (Monster.GetComponent<EnemyCtrl>().isDead == false) 
            {
                m_points[3] = Monster.transform.position;
            }
            else
            {
                //GetComponent<Animator>().SetTrigger("Impact");
                //m_points[0] = transform.position;
                //m_timerCurrent = 0;
                //NearestGameObject();
                //GetComponent<Animator>().SetTrigger("Impact");
                //Over();
            }
        }
        

        m_timerCurrent += Time.deltaTime * m_speed;

        

        Vector3 rot = new Vector3(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x, m_points[3].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y, m_points[3].y),
            CubicBezierCurve(m_points[0].z, m_points[1].z, m_points[2].z, m_points[3].z)
        );
        float dy = transform.position.y - temp.y;
        float dx = transform.position.x - temp.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        temp = transform.position;

        transform.position = new Vector3(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x, m_points[3].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y, m_points[3].y),
            CubicBezierCurve(m_points[0].z, m_points[1].z, m_points[2].z, m_points[3].z)
        );        
    }
    GameObject NearestGameObject()
    {
        Vector3 mPosition = Input.mousePosition;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        FoundMonsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        shortDis = Vector2.Distance(gameObject.transform.position, FoundMonsters[0].transform.position);
        Monster = FoundMonsters[0];
        foreach (GameObject found in FoundMonsters)
        {
            float Distance = Vector3.Distance(new Vector3(target.x + Random.Range(-30, 30), target.y + Random.Range(-30, 30), 0), found.transform.position);

            if (Distance < shortDis)
            {
                shortDis = Distance;
                Monster = found;
            }
           
        }
        return Monster;
    }


    public void Init(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
    {
        
        m_speed = _speed;

        m_timerMax = Random.Range(0.8f, 1.0f);

        m_points[0] = _startTr.position;

        m_points[1] = _startTr.position +
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) +
            (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up) + 
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, -0.8f) * _startTr.forward);

        m_points[2] = _endTr.position +
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) + 
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up) + 
            (_newPointDistanceFromEndTr * Random.Range(0.8f, 1.0f) * _endTr.forward);

        m_points[3] = _endTr.position;

        transform.position = _startTr.position;
    }
    private float CubicBezierCurve(float a, float b, float c, float d)
    {
 
        float t = m_timerCurrent / m_timerMax; 

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
    void Over()
    {
         Destroy(this.gameObject);
    }
  
}
