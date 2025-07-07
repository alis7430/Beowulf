using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPC의 상태 정의
/// </summary>
public enum NPC_STATE
{
    NONE = -1,
    IDLE = 0,
    HASQUEST,
    QUEST_PROGRESSING,
    CLEARQUEST,
    NUM_OF_STATE
}

public class NPCState : MonoBehaviour
{
    public Sprite QuestionMark;     //퀘스트가 있을 때 표시할 마크
    public Sprite ProgressMark;     //퀘스트가 진행중일 때 표시할 마크
    public Sprite ExclamtionMark;   //퀘스트가 완료되었을 때 표시할 마크

    [SerializeField]
    private NPC_STATE state = NPC_STATE.NONE;     //npc 상태를 저장할 변수

    private SpriteRenderer spriteRenderer;      //상태를 표시할 렌더러 저장

    private void Start()
    {
        if (spriteRenderer == null && this.transform.GetChild(0).GetComponent<SpriteRenderer>() != null)
            //SpriteRenderer가 있는 NpcStateMark 오브젝트는 꼭 첫번째 자식이어야 한다.
            spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (this.transform.GetComponent<QuestGiver>() != null)
        {
            SetState(NPC_STATE.HASQUEST);
        }
        else
        {
            SetState(NPC_STATE.IDLE);
        }
    }
    //--------------------------------------------------------------------------
    /// <summary>
    /// npc의 상태를 변경한다.
    /// 상태 변경에 따른 이벤트 역시 취해진다.
    /// </summary>
    /// <param name="state">바꿀 npc의 상태 변수</param>
    public void SetState(NPC_STATE state)
    {

        this.state = state;

        switch (this.state)
        {
            case NPC_STATE.NONE:
                spriteRenderer.sprite = null;
                break;
            case NPC_STATE.IDLE:
                spriteRenderer.sprite = null;
                break;
            case NPC_STATE.HASQUEST:
                spriteRenderer.sprite = QuestionMark;
                break;
            case NPC_STATE.QUEST_PROGRESSING:
                spriteRenderer.sprite = ProgressMark;
                break;
            case NPC_STATE.CLEARQUEST:
                spriteRenderer.sprite = ExclamtionMark;
                break;
            case NPC_STATE.NUM_OF_STATE:
                break;
            default:
                break;
        }
    }
    //--------------------------------------------------------------------------
    /// <summary>
    /// npc의 상태를 반환한다.
    /// </summary>
    /// <returns></returns>
    public NPC_STATE GetState()
    {
        return state;
    }
    //--------------------------------------------------------------------------
}
