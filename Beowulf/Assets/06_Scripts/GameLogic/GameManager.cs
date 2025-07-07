using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----------------------------------------------------------
// Scripts\GameLogic\GameManager.cs
//
// 싱글톤으로 구현된 GameManager 클래스입니다.
// 게임의 전체적인 흐름을 제어하는 함수들을 가지고 있습니다.
// 게임매니저 인스턴스를 사용하여 함수를 호출하십시오.
//-----------------------------------------------------------
public class GameManager : MonoBehaviour
{
    #region C# properties
    //-----------------------------------------------------------
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    #endregion

    #region variables
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;
    #endregion

    //-----------------------------------------------------------
    #region methods
    private void Awake()
    {
        // 인스턴스가 존재하지 않는 경우, 이 객체를 인스턴스로 만든다.
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else
            DestroyImmediate(gameObject);
        

        if (SceneManager.GetActiveScene().name == "02_Tutorial")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SoundManager.instance.PlayBGM("Tutorial");
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //ExitGame();

        if (SceneManager.GetActiveScene().name == "02_Tutorial")
        {
            if (Input.GetKeyDown(KeyCode.Y))
                LoadingSceneManager.LoadScene("03_Stage1");
            if (Input.GetKeyDown(KeyCode.T))
            {
                Quest q = new Quest();
                QuestManager.instance.AddQuestListContents(q);
                Debug.Log("호출");
            }
        }
    }

    public void StartNewGame()
    {
        LoadingSceneManager.LoadScene("02_Tutorial");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    //-----------------------------------------------------------
    #endregion
}
