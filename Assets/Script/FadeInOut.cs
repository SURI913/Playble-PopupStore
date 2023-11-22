using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    private float fadeSpeed = 1.5f;
    private bool fadeInOnStart = false;
    
    private CanvasGroup canvasGroup;

    void Start()
    {
        //각 씬 마다 캔버스 인식해서 FadeInOut
        canvasGroup = GameObject.FindGameObjectWithTag("CanvasTag").GetComponent<CanvasGroup>();
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn());
        }
        else
        {
            canvasGroup.alpha = 1f;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        fadeInOnStart = false;
        gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        fadeInOnStart = true;
        gameObject.SetActive(false);

    }
}
