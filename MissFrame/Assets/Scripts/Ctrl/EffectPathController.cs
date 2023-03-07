using DG.Tweening;
using MissFrame.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPathController : SingletonMono<EffectPathController>
{
    private int m_CreatEffectNum = 0;
    private EffectData m_EffectData;

    private Dictionary<int,CacheEffectData> DicCacheEffectData = new Dictionary<int,CacheEffectData>();

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
        m_EffectData = objs[0] as EffectData;
        m_CreatEffectNum = m_EffectData.CfgShowEffectPathData.CreatNum;
        CreatEffect();
    }

    private void CreatEffect()
    {
        int subStepId = m_EffectData.SubStep.CfgSubStepData.Id;
        CacheEffectData cacheData = new CacheEffectData(subStepId);
        if (DicCacheEffectData.ContainsKey(subStepId))
        {
            LogUtilits.LogErrorFormat($"id�ظ������飺{subStepId}");
            return;
        }
        DicCacheEffectData.Add(subStepId, cacheData); 
        int index = 0;
        Debug.LogFormat("��ʼ����entity");
        int resId = m_EffectData.CfgShowEffectPathData.EffectResID;
        for (int i = 0; i < m_CreatEffectNum; i++)
        {
            ObjectPoolManager.GetInstance().GetObjectFormPoolAsyncByResId(resId, (entity) =>
            {
                index++;
                entity.gameObject.SetActive(true);
                cacheData.AddPoolEntity(entity);
                if (index == m_CreatEffectNum)
                {
                    CreatEndEffectObj();
                }
            });
        }
    }
    //������Ч��������
    private void CreatEndEffectObj()
    {
        //������ϣ���ʼ���Ź켣��Ч
        int subStepId = m_EffectData.SubStep.CfgSubStepData.Id;
        CacheEffectData effectData = DicCacheEffectData[subStepId];
        Coroutine cor1 = StartCoroutine(IE_EffectFly());
        Coroutine cor2 = StartCoroutine(IE_DelayEndAction());
        effectData.SetCorEffectFly(cor1);
        effectData.SetCorDelayOnEnd(cor2);
    }

    private IEnumerator IE_DelayEndAction()
    {
        yield return new WaitForSeconds(m_EffectData.CfgShowEffectPathData.Duration);
        LogUtilits.LogFormat("�ִ��յ�");
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_EffectData.SubStep);
    }

    private IEnumerator IE_EffectFly()
    {
        Debug.LogFormat("��ʼЭ��---��Ч�켣");
        int subStepId = m_EffectData.SubStep.CfgSubStepData.Id;
        CacheEffectData cacheData = DicCacheEffectData[subStepId];
        List<PoolEntity> entityList = cacheData.EntityList;
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
        int subStepId = m_EffectData.SubStep.CfgSubStepData.Id;
        CacheEffectData effectData = DicCacheEffectData[subStepId];
        effectData.AddTween(t);
    }

    private void OnStopExecuteEffectPathCallBack(params object[] objs)
    {
        if (objs.ArrayIsNull(true))
        {
            return;
        }
        EffectData data = objs[0]as EffectData;
        int subStepId = data.SubStep.CfgSubStepData.Id;
        CacheEffectData cacheData = null;
        if (!DicCacheEffectData.TryGetValue(subStepId,out cacheData))
        {
            LogUtilits.LogErrorFormat($"���Ӳ���Id�Ļ�������Ϊ�գ����飡id:{subStepId}");
            return;
        }
        StopShowEffectPath(cacheData);
    }

    //ֹͣ-��ЧЭ��
    private void StopIE_EffectFly(CacheEffectData cacheData)
    {
        Coroutine cor = cacheData.CorEffectFly;
        if (null == cor)
        {
            return;
        }
        StopCoroutine(cor);
        cor = null;
    }
    //ֹͣ-�������Э��
    private void StopIE_DelayOnEnd(CacheEffectData cacheData)
    {
        Coroutine cor = cacheData.CorDelayOnEnd;
        if (null == cor)
        {
            return;
        }
        StopCoroutine(cor);
        cor = null;
    }


    //ֹͣչʾ��Ч·����ʾ
    private void StopShowEffectPath(CacheEffectData cacheData)
    {
        //ֹͣ-��ЧЭ��
        StopIE_EffectFly(cacheData);
        //ֹͣ-�������Э��
        StopIE_DelayOnEnd(cacheData);
        //ֹͣtween
        ClearTween(cacheData);
        //����entity����
        ClearEntity(cacheData);
    }
    //����entity
    private void ClearEntity(CacheEffectData cacheData)
    {
        List<PoolEntity> entityList = cacheData.EntityList;
        foreach (var item in entityList)
        {
            item.Recycle();
        }
        entityList.Clear();
    }
    //��ֹtween
    private void ClearTween(CacheEffectData cacheData)
    {
        List<Tween> twList = cacheData.TweenList;
        foreach (Tween t in twList) 
        {
            t.Kill();
        }
        twList.Clear();
    }


}
