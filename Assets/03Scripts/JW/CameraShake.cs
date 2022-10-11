using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;
    ImageEffectAllowedInSceneView ie;

    private void Update()
    {
        initialPosition = GameObject.FindWithTag("MainCamera").transform.position;//카메라 흔들릴 위치값
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, -50f);
            //transform.position = new Vector3 (0f,0f,-10f);
        }
    }
    public void VibrateForTime(float time)

    {
        ShakeTime = time;
    }
    IEnumerator Shake()
    {
        for (int i = 0; i < 10; i++)
        {
            float cameraPosX = Random.value * ShakeAmount * 2 - ShakeAmount;
            float cameraPosY = Random.value * ShakeAmount * 2 - ShakeAmount;
            Vector3 cameraPos = transform.position;
            cameraPos.x += cameraPosX;
            cameraPos.y += cameraPosY;
            transform.position = cameraPos;
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = new Vector3(0f, 0f, -10f);
    }
    
}
