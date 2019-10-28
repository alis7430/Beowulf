using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuAction : MonoBehaviour
{
    public GameObject TitleText;
    TMP_Text titleText;

    
    void Start()
    {
        titleText = TitleText.GetComponent<TMP_Text>();
        StartCoroutine(TmpTextAlphaRender(titleText, 0.0f, 1.0f, 3.0f, 1.5f));
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
        
    }
}
