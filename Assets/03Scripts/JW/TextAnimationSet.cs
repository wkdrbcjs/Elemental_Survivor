using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimationSet : MonoBehaviour
{
    public Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        this.gameObject.SetActive(false);
        //StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Flash()
    {
        for(int j=0; j<3; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                _text.color -= new Color(0f, 0f, 0f, 0.2f);
                yield return new WaitForSeconds(0.05f);
            }

            for (int i = 0; i < 5; i++)
            {
                _text.color += new Color(0f, 0f, 0f, 0.2f);
                yield return new WaitForSeconds(0.05f);
            }
        }
        //Color tempColor = _text.color;
        

        yield return new WaitForSeconds(0.3f);

    }
}
