using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlashMove : WindSlash
{
    private float _time;
  
    void Start()
    {       
        ItemSkill1(this.gameObject);
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    private void Update()
    {
        _time += Time.deltaTime;
        if(_time > 1.5f)
        {
            //target = transform.position;
            //_target = _player.transform.position;
            //angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.Translate(1.0f * Time.deltaTime * Vector2.right);
            //transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 10.0f * Time.deltaTime, );            
            transform.position = _player.transform.position;
        }
        transform.Translate(1.0f * Time.deltaTime * Vector2.right);
    }
}
