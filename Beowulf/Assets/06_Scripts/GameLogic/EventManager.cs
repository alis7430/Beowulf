using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----------------------------------------------------------
//Scripts\GameLogic\EventManager.cs
//
//Enum defining all possible game events
//More events should be added to the list
//-----------------------------------------------------------
public enum EVENT_TYPE
{
    NONE,
    GAME_INIT,
    GAME_END,
    UPDATE_UI,
    PLAYER_HIT,
    DEAD,
    GET_ITEM,
    ENEMY_KILLED,
    NUM_OF_EVENTS
};
public class EventManager : MonoBehaviour
{
    #region C# properties
    //-----------------------------------------------------------
    // 인스턴스에 접근하기 위한 프로퍼티
    public static EventManager Instance
    {
        get
        {
            return _instance;
        }
        set { }
    }
    #endregion

    #region variables
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static EventManager _instance;

    // 이벤트를 위한 델리게이트 변수 선언
    public delegate void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);

    // 리스너 오브젝트 배열(이벤트를 수신하는 모든 오브젝트가 등록된다.)
    private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
    #endregion
    //-----------------------------------------------------------

    #region methods
    void Awake()
    {
        // 인스턴스가 존재하지 않는 경우, 이 객체를 인스턴스로 만든다.
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        }
        else // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
            DestroyImmediate(this);
    }

    //-----------------------------------------------------------
    /// <summary>
    /// Function to add specified listener-object to array of listeners
    /// </summary>
    /// <param name="Event_Type">Event to Listen for</param>
    /// <param name="Listener">Object to listen for event</param>
    public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
    {
        // 이 이벤트에 해당하는 모든 리스너 리스트
        List<OnEvent> ListenList = null;

        //새로운 이벤트가 ListenList에 추가된다. 키값에 따라 리스너가 존재하는지 검사한 후, 존재하면 리스트에 추가한다.
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            //리스트가 존재하므로 새로운 리스너를 추가한다.
            ListenList.Add(Listener);
            return;
        }

        //만일 없다면 키에 따른 새로운 리스트를 생성한다.
        ListenList = new List<OnEvent>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList); //리스너 딕셔너리 리스에 새로운 이벤트를 저장한다
    }
    //-----------------------------------------------------------
    /// <summary>
    /// Function to post event to listeners
    /// </summary>
    /// <param name="Event_Type">Event to invoke</param>
    /// <param name="Sender">Object invoking event</param>
    /// <param name="Param">Optional argument</param>
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        //모든 리스너에게 이벤트가 일어남을 알린다.

        //이 이벤트에 해당하는 모든 리스너 리스트
        List<OnEvent> ListenList = null;

        //키값에 해당하는 리스너가 없다면 알릴 리스너들이 없기 때문에 빠져나간다.
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;

        //해당하는 이벤트가 있다면 적당한 리스너에게 알린다.
        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null)) //만일 리스너 오브젝트가 비어있지 않다면, 이벤트를 알린다,.
                ListenList[i](Event_Type, Sender, Param);
        }
    }
    //-----------------------------------------------------------
    //Remove event type entry from dictionary, including all listeners
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        //Remove entry from dictionary
        Listeners.Remove(Event_Type);
    }
    //-----------------------------------------------------------
    //딕셔너리 내에있는 중복된 엔트리들을 제거한다.
    public void RemoveRedundancies()
    {
        //Create new dictionary
        Dictionary<EVENT_TYPE, List<OnEvent>> TmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

        //Cycle through all dictionary entries
        foreach (KeyValuePair<EVENT_TYPE, List<OnEvent>> Item in Listeners)
        {
            //Cycle through all listener objects in list, remove null objects
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                //If null, then remove item
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }

            //If items remain in list for this notification, then add this to tmp dictionary
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        //Replace listeners object with new, optimized dictionary
        Listeners = TmpListeners;
    }
    //-----------------------------------------------------------
    //Called on scene change. Clean up dictionary
    void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
    //-----------------------------------------------------------
    #endregion
}
