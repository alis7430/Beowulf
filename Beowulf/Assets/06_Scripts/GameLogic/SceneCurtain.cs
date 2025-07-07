using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneCurtain : MonoBehaviour
{
    public GameObject curtain;
    public TMP_Text stageText;

    // Start is called before the first frame update
    void Start()
    {
        curtain = GameObject.FindGameObjectWithTag("SceneCurtain");
        curtain.SetActive(true);
        stageText = curtain.transform.GetChild(0).GetComponent<TMP_Text>();
        Image img = curtain.GetComponent<Image>();
        
        StartCoroutine(TmpTextAlphaRender(stageText, 0f, 1f, 3f, 1f));
        StartCoroutine(ImageAlphaRender(img, 1f, 0f, 3f, 6f));


    }

    public IEnumerator TmpTextAlphaRender(TMP_Text text, float startAlpha, float endAlpha, float time, float duration)
    {

        float t = 0.0f;
        float delayTime = 0.0f;
        text.alpha = startAlpha;

        while (text.alpha != endAlpha)
        {
            delayTime += Time.deltaTime;

            if (delayTime > duration)
            {
                t += Time.deltaTime / time;
                text.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            }

            yield return null;

        }
        StartCoroutine(TmpTextAlphaRenderReverse(stageText, 1f, 0f, 3f, 2f));
    }
    public IEnumerator TmpTextAlphaRenderReverse(TMP_Text text, float startAlpha, float endAlpha, float time, float duration)
    {

        float t = 0.0f;
        float delayTime = 0.0f;

        while (text.alpha != endAlpha)
        {
            delayTime += Time.deltaTime;

            if (delayTime > duration)
            {
                t += Time.deltaTime / time;
                text.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            }

            yield return null;

        }
    }
    public IEnumerator ImageAlphaRender(Image image, float startAlpha, float endAlpha, float time, float duration)
    {

        float t = 0.0f;
        float delayTime = 0.0f;
        Color color = image.color;
        color.a = startAlpha;

        while (color.a > 0.0f)
        {
            delayTime += Time.deltaTime;

            if (delayTime > duration)
            {
                t += Time.deltaTime / time;
                color.a = Mathf.Lerp(startAlpha, endAlpha, t);
                image.color = color;
            }

            yield return null;
        }
        curtain.SetActive(false);
       // DestroyImmediate(this.gameObject);
    }
}
