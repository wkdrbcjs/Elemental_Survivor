using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터에 들어갈 화상

public class EnemyBurner : MonoBehaviour
{
    [SerializeField]
    EnemyCtrl parentEnemy;

    
    public int damage;
    public float lifetime=10.0f;

    public bool activateManaAddiction = false;
    public int manaReturn = 0;

    public Color DamageColor = new Color(195, 0, 0);


    public static bool findBurnOnTarget(GameObject Target)
    {
        EnemyBurner instance=null;
        instance = Target.GetComponentInChildren<EnemyBurner>();
        if (instance!=null)
        {
            return true;
        }
        return false;
    }


    void Awake()
    {
        //damage += damage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        parentEnemy = GetComponentInParent<EnemyCtrl>();

        StartCoroutine("BurnParentEnemy");
    }

    IEnumerator BurnParentEnemy()
    {
        parentEnemy.GetComponent<SpriteRenderer>().color -= new Color(0, 0.8f, 0.8f, 0);
        parentEnemy.isBurned = true;
        for (int i=1; lifetime > 0.0f; lifetime -= 1.0f)
        {
            yield return new WaitForSeconds(1.0f);
            if (parentEnemy.takeDamage(damage,DamageColor)&&activateManaAddiction)
            {
                //PlayerStatus.instance.addCurrentMP(manaReturn);
            }
            
        }
        parentEnemy.isBurned = false;
        parentEnemy.GetComponent<SpriteRenderer>().color += new Color(0, 0.8f, 0.8f, 0);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
    }

    void Update()
    {
        
    }
}
