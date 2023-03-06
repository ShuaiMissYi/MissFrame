using MissFrame.Trigger;
using UnityEngine;
using DG.Tweening;

public class TweenMoveController : SingletonMono<TweenMoveController>
{

    private TweenMoveData m_TweenMoveData;

    private Tween m_MoveTween;

    public override void Init()
    {
        base.Init();
        AddListener();
    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist(StepShowType.TweenMove, OnTweenMoveCallBack);
        EventDispatcher.GetInstance().Regist(EventType.StopTweenMove, OnStopTweenMoveCallBack);
    }

    private void OnTweenMoveCallBack(params object[] objs)
    {
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0)
        {
            return;
        }
        m_TweenMoveData = objs[0] as TweenMoveData;
        StartTweenMove();
    }

    private void StartTweenMove()
    {
        //�����Ķ���
        GameObject rootOpera = GameObject.Find(m_TweenMoveData.CfgTweenMoveData.RootOperaName);
        Transform transOpera = rootOpera.transform.Find(m_TweenMoveData.CfgTweenMoveData.RelativeOperaPath);
        //ָ�������
        GameObject rootTarget = GameObject.Find(m_TweenMoveData.CfgTweenMoveData.RootTargetName);
        Transform transTarget = rootTarget.transform.Find(m_TweenMoveData.CfgTweenMoveData.RelativeTargetPath);
        //�ƶ�����
        transOpera.gameObject.SetActive(true);
        m_MoveTween = transOpera.DOMove(transTarget.position, m_TweenMoveData.CfgTweenMoveData.Duration).SetEase(Ease.Linear).OnComplete(() => 
        {
            EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_TweenMoveData.SubStep);
        });
    }
   

    private void OnStopTweenMoveCallBack(params object[] objs)
    {
        ResetTween();
    }

    private void ResetTween()
    {
        if (null == m_MoveTween)
        {
            return;
        }
        m_MoveTween.Kill();
        m_MoveTween = null;
    }


}
