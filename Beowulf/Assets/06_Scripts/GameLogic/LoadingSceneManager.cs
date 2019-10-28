using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    public Image ProgressBar;
    public TMP_TextElement LoadingText;

    public float LoopTime;
    private float elapsedTime;
    private int index;


    private Image backgroundSource;
    public Sprite[] BackGroundImages;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    
        elapsedTime = 0;
        backgroundSource = GameObject.Find("BackGround").GetComponent<Image>();

        index = (int)Random.Range(0, BackGroundImages.Length - 1);
        backgroundSource.sprite = BackGroundImages[index];
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("00_LoadingScene");
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= LoopTime)
        {
            elapsedTime = 0;

            int idx = (int)Random.Range(0, BackGroundImages.Length - 1);

            if (index == idx)
                index++;
            else
                index = idx;

            backgroundSource.sprite = BackGroundImages[index];
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if(op.progress < 0.9f)
            {

                ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, op.progress, timer);
                if(ProgressBar.fillAmount >= op.progress)
                {
                    timer = 0;
                }
            }
            else
            {
                ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, 1.0f, timer);
                if(ProgressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;

                    yield break;
                }
            }
        }
    }
}
