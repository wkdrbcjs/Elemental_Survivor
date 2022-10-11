using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassSellectManager : MonoBehaviour
{
    public GameObject[] ClassText = new GameObject[5];
    public Image PlayerImage;
    public Image PetImage;
    public Image FadePanel;

    public Sprite[] PlayerImageSet = new Sprite[5];

    public Sprite[] PetImageSet = new Sprite[4];

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Animate());
        StartCoroutine(FadeIn());
    }

    IEnumerator Animate()
    {
        i += 1;
        if (i >= 5)
        {
            i = 0;
        }
        PlayerImage.sprite = PlayerImageSet[i];
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Animate());
    }

    public void StartButton()
    {
        StartCoroutine(FadeOut());
    }
    public void SellectButton(int a)
    {
        PetImage.sprite = PetImageSet[a];
        ClassText[0].SetActive(false);
        ClassText[1].SetActive(false);
        ClassText[2].SetActive(false);
        ClassText[3].SetActive(false);
        ClassText[4].SetActive(false);

        ClassText[a].SetActive(true);
    }
    IEnumerator FadeIn()
    {
        for (int i = 0; i < 50; i++)
        {
            FadePanel.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        FadePanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);

    }
    IEnumerator FadeOut()
    {
        FadePanel.gameObject.SetActive(true);
        for (int i = 0; i < 50; i++)
        {
            FadePanel.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("InGame");
    }
}
