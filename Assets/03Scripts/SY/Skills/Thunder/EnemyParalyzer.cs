using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParalyzer : MonoBehaviour
{
    [SerializeField]
    EnemyCtrl parentEnemy;


    public int damage;
    public float lifetime = 3.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        //damage = 
        parentEnemy = GetComponentInParent<EnemyCtrl>();
        StartCoroutine("ParalyzeParentEnemy");

    }
     
    IEnumerator ParalyzeParentEnemy()
    {
        //Debug.Log(parentEnemy.gameObject.layer.ToString());
        yield return new WaitForSeconds(0.2f);
        if (parentEnemy!=null)//EnemyMonster
        {
            parentEnemy.pararizeDamage = damage;
            parentEnemy.isParalyzed = true;
        }
        //parentEnemy.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0.2f, 0);
        yield return new WaitForSeconds(lifetime);
        //parentEnemy.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0.2f, 0);
        if (parentEnemy != null)
        {
            //parentEnemy.Hit(gameObject, 50, 0f, new Color(255,255,0));
            parentEnemy.isParalyzed = false;
        }

        Destroy(this.gameObject);
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
