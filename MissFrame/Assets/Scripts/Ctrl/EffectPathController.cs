using DG.Tweening;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPathController : SingletonMono<EffectPathController>
{
    private int m_CreatEffectNum = 0;
    private EffectData m_EffectData;

    private List<PoolEntity> PoolEntityList = new List<PoolEntity>();

    //特效间隔播放协程
    private Coroutine m_CoroutineEffectFly;
    //第一个特效飞行完毕协程
    private Coroutine m_CoroutineDelayOnEnd;

    private List<Tween> tweenList = new List<Tween>();

    public override void Init()
    {
        base.Init();
        AddListener();
    }

    private void AddListener()
    {
        EventDispatcher.GetInstance().Regist(StepShowType.ShowEffectPath, OnShowEffectPathCallBack);
        EventDispatcher.GetInstance().Regist(EventType.StopExecuteEffectPath, OnStopExecuteEffectPathCallBack);
    }

    private void OnShowEffectPathCallBack(params object[] objs)
    {
        if (GameUtilits.GameIsNull(objs) ||objs.Length==0)
        {
            return;
        }
        m_EffectData = objs[0]as EffectData;
        m_CreatEffectNum = m_EffectData.CfgShowEffectPathData.CreatNum;
        CreatEffect();
    }

    private void CreatEffect()
    {
        int index = 0;
        Debug.LogFormat("开始生成entity");
        for (int i = 0; i < m_CreatEffectNum; i++)
        {
            ObjectPoolManager.GetInstance().GetObjectFormPoolAsyncByResId(m_EffectData.CfgShowEffectPathData.EffectResID, (entity) =>
            {
                index++;
                entity.gameObject.SetActive(true);
                PoolEntityList.Add(entity);
                if (index == m_CreatEffectNum)
                {
                    //生成完毕，开始播放轨迹特效
                    StopIE_EffectFly();
                    StopIE_DelayOnEnd();
                    m_CoroutineEffectFly = StartCoroutine(IE_EffectFly());
                    m_CoroutineDelayOnEnd = StartCoroutine(IE_DelayEndAction());
                }
            });
        }
    }
    private IEnumerator IE_DelayEndAction()
    {
        yield return new WaitForSeconds(m_EffectData.CfgShowEffectPathData.Duration);
        //Debug.LogError("抵达终点");
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_EffectData.SubStep);
    }

    private IEnumerator IE_EffectFly()
    {
        Debug.LogFormat("开始协程---特效轨迹");
        for (int i = 0; i < m_CreatEffectNum; i++)
        {
            TargetFly(PoolEntityList[i],i);
            yield return new WaitForSeconds(m_EffectData.CfgShowEffectPathData.Interval);
        }
    }


    private void TargetFly(PoolEntity entity,int index)
    {
        Tween t = entity.transform.DOPath(m_EffectData.CfgShowEffectPathData.PathPointArray,m_EffectData.CfgShowEffectPathData.Duration,  PathType.Linear, PathMode.Full3D)
            .SetOptions(false)
            .SetLookAt(0.001f);
        t.SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        tweenList.Add(t);
    }

    private void OnStopExecuteEffectPathCallBack(params object[] objs)
    {
        if (null == m_EffectData)
        {
            return;
        }
        if (GameUtilits.GameIsNull(objs) || objs.Length == 0)
        {
            return;
        }
        EffectData data = objs[0]as EffectData;
        if (data.CfgShowEffectPathData.Id == m_EffectData.CfgShowEffectPathData.Id)
        {
            StopShowEffectPath();
        }
    }

    //停止-特效协程
    private void StopIE_EffectFly()
    {
        if (null != m_CoroutineEffectFly)
        {
            StopCoroutine(m_CoroutineEffectFly);
            m_CoroutineEffectFly = null;
        }
    }
    //停止-步骤结束协程
    private void StopIE_DelayOnEnd()
    {
        if (null != m_CoroutineDelayOnEnd)
        {
            StopCoroutine(m_CoroutineDelayOnEnd);
            m_CoroutineDelayOnEnd = null;
        }
    }


    //停止展示特效路径显示
    private void StopShowEffectPath()
    {
        //停止-特效协程
        StopIE_EffectFly();
        //停止-步骤结束协程
        StopIE_DelayOnEnd();
        //停止tween
        ClearTween();
        //回收entity对象
        ClearEntity();
    }
    //回收entity
    private void ClearEntity()
    {
        foreach (PoolEntity item in PoolEntityList)
        {
            item.Recycle();
        }
        PoolEntityList.Clear();
    }
    //终止tween
    private void ClearTween()
    {
        foreach (var item in tweenList)
        {
            item.Kill();
        }
        tweenList.Clear();
    }


}
