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
    //key：轨迹id    value：资源数据类列表
    private Dictionary<int,List<PoolEntity>> DicPoolEntity = new Dictionary<int, List<PoolEntity>>();

    //key：轨迹id  value：子步骤轨迹协程-每个飞行点之间的间隔时间
    private Dictionary<int,Coroutine> DicCorEffectFly = new Dictionary<int,Coroutine>();
    //key：轨迹id  value：子步骤协程-播放一圈需要的时长
    private Dictionary<int, Coroutine> DicCorDelayOnEnd = new Dictionary<int, Coroutine>();

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
        if (objs.ArrayIsNull())
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
        int effectId = m_EffectData.CfgShowEffectPathData.Id;
        int resId = m_EffectData.CfgShowEffectPathData.EffectResID;
        List<PoolEntity> entityList = null;
        DicPoolEntity.TryGetValue(effectId, out entityList);
        if (entityList.ListIsNull())
        {
            entityList = new List<PoolEntity>();
            DicPoolEntity[effectId] = entityList;
        }
        if (entityList.Count < m_CreatEffectNum)
        {
            m_CreatEffectNum = m_CreatEffectNum - DicPoolEntity[effectId].Count;
            for (int i = 0; i < m_CreatEffectNum; i++)
            {
                ObjectPoolManager.GetInstance().GetObjectFormPoolAsyncByResId(resId, (entity) =>
                {
                    index++;
                    entity.gameObject.SetActive(true);
                    entityList.Add(entity);
                    if (index == m_CreatEffectNum)
                    {
                        CreatEndEffectObj();
                    }
                });
            }
        }
        else
        {
            CreatEndEffectObj();
        }
        
    }
    //生成特效对象后调用
    private void CreatEndEffectObj()
    {
        //生成完毕，开始播放轨迹特效
        int effectId = m_EffectData.CfgShowEffectPathData.Id;
        DicCorEffectFly[effectId] = StartCoroutine(IE_EffectFly());
        DicCorDelayOnEnd[effectId] = StartCoroutine(IE_DelayEndAction());
    }

    private IEnumerator IE_DelayEndAction()
    {
        yield return new WaitForSeconds(m_EffectData.CfgShowEffectPathData.Duration);
        LogUtilits.LogFormat("抵达终点");
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_EffectData.SubStep);
    }

    private IEnumerator IE_EffectFly()
    {
        Debug.LogFormat("开始协程---特效轨迹");
        int effectResId = m_EffectData.CfgShowEffectPathData.EffectResID;
        List<PoolEntity> entityList = DicPoolEntity[effectResId];
        for (int i = 0; i < m_CreatEffectNum; i++)
        {
            TargetFly(entityList[i],i);
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
        if (objs.ArrayIsNull())
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
        if (null == m_EffectData)
        {
            return;
        }
        Coroutine cor = DicCorEffectFly[m_EffectData.CfgShowEffectPathData.Id];
        if (null == cor)
        {
            return;
        }
        StopCoroutine(cor);
        cor = null;
    }
    //停止-步骤结束协程
    private void StopIE_DelayOnEnd()
    {
        if (null == m_EffectData)
        {
            return;
        }
        Coroutine cor = DicCorDelayOnEnd[m_EffectData.CfgShowEffectPathData.Id];
        if (null == cor)
        {
            return;
        }
        StopCoroutine(cor);
        cor = null;
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
