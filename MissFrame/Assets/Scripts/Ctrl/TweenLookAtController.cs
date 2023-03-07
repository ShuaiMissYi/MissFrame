using DG.Tweening;
using MissFrame.Trigger;
using UnityEngine;

public class TweenLookAtController : SingletonMono<TweenLookAtController>
{
    private TweenLookAtData m_TweenLookAtData;

    private Tween m_LookAtTween;

    public string testName = "test内容";

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
        if (objs.ArrayIsNull())
        {
            return;
        }
        m_TweenLookAtData = objs[0] as TweenLookAtData;
        StartTweenLookAt();
    }

    private void StartTweenLookAt()
    {
        //操作的对象
        GameObject rootOpera = GameObject.Find(m_TweenLookAtData.CfgTweenLookAtData.RootOperaName);
        Transform transOpera = rootOpera.transform.Find(m_TweenLookAtData.CfgTweenLookAtData.RelativeOperaPath);
        //指定点对象
        GameObject rootTarget = GameObject.Find(m_TweenLookAtData.CfgTweenLookAtData.RootTargetName);
        Transform transTarget = rootTarget.transform.Find(m_TweenLookAtData.CfgTweenLookAtData.RelativeTargetPath);
        //移动对象
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
