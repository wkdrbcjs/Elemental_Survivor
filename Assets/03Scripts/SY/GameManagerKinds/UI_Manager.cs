using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance; 

    [Header("UI")]//UI
    public GameObject GameOverUI;
    public GameObject GameClearUI;
    public GameObject StatUI;
    public Image FadePanel;
    public GameObject Result_UI;
    public GameObject ClassExp_UI;
    public GameObject Canvas_UI;
    public GameObject Warning_UI;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    public IEnumerator FadeIn()
    {
        for (int i = 0; i < 50; i++)
        {
            FadePanel.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        FadePanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
