using DG.Tweening;
using MissFrame.Trigger;
using UnityEngine;

public class TweenLookAtController : SingletonMono<TweenLookAtController>
{
    private TweenLookAtData m_TweenLookAtData;

    private Tween m_LookAtTween;

    public override void Init()
    {
        base.Init();
        AddListener();
    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist(StepShowType.TweenLookAt, OnTweenLookAtCallBack);
        EventDispatcher.GetInstance().Regist(EventType.StopTweenLookAt, OnStopTweenLookAtCallBack);
    }

    private void OnTweenLookAtCallBack(params object[] objs)
    {
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0)
        {
            return;
        }
        m_TweenLookAtData = objs[0] as TweenLookAtData;
        StartTweenLookAt();
    }

    private void StartTweenLookAt()
    {
        //�����Ķ���
        GameObject rootOpera = GameObject.Find(m_TweenLookAtData.CfgTweenLookAtData.RootOperaName);
        Transform transOpera = rootOpera.transform.Find(m_TweenLookAtData.CfgTweenLookAtData.RelativeOperaPath);
        //ָ�������
        GameObject rootTarget = GameObject.Find(m_TweenLookAtData.CfgTweenLookAtData.RootTargetName);
        Transform transTarget = rootTarget.transform.Find(m_TweenLookAtData.CfgTweenLookAtData.RelativeTargetPath);
        //�ƶ�����
        transOpera.gameObject.SetActive(true);
        m_LookAtTween = transOpera.DOLookAt(transTarget.position, m_TweenLookAtData.CfgTweenLookAtData.Duration).OnComplete(() =>
        {
            EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep,m_TweenLookAtData.SubStep);
        });
    }

    private void OnStopTweenLookAtCallBack(params object[] objs)
    {
        ResetTween();
    }

    private void ResetTween()
    {
        if (null == m_LookAtTween)
        {
            return;
        }
        m_LookAtTween.Kill();
        m_LookAtTween = null;
    }







}
