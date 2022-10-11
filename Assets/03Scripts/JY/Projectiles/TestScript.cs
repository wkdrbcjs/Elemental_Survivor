using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : Projectile
{      
    public GameObject EarthSpike;   

    private void Start()
    {       
        ItemSkill2(this.gameObject);
        _SM.SoundPlay(_SM.Earth_Spike);
        StartCoroutine(EarthSpikeCast());
    }


    private void Update()
    {  
        transform.localPosition = Vector2.MoveTowards(transform.position, _target, 30.0f * Time.deltaTime);
        Delete();
    }

    IEnumerator EarthSpikeCast()
    {
        while(true)
        {
            for(int index = 0; index < ItemInfoSet.instance.Items[8].ProjectileNum; index++)
            {
                Instantiate(EarthSpike, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }            
        }
    }   

    void Delete()
    {
        if((Vector2)transform.position == _target)
        {
            StopCoroutine(EarthSpikeCast());
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()//화면밖으로 나갈때
    {
        //Destroy(gameObject);//총알 파괴
    }    
}
