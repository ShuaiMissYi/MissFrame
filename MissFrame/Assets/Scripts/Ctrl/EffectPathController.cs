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
    //key���켣id    value����Դ�������б�
    private Dictionary<int,List<PoolEntity>> DicPoolEntity = new Dictionary<int, List<PoolEntity>>();

    //key���켣id  value���Ӳ���켣Э��-ÿ�����е�֮��ļ��ʱ��
    private Dictionary<int,Coroutine> DicCorEffectFly = new Dictionary<int,Coroutine>();
    //key���켣id  value���Ӳ���Э��-����һȦ��Ҫ��ʱ��
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
        Debug.LogFormat("��ʼ����entity");
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
    //������Ч��������
    private void CreatEndEffectObj()
    {
        //������ϣ���ʼ���Ź켣��Ч
        int effectId = m_EffectData.CfgShowEffectPathData.Id;
        DicCorEffectFly[effectId] = StartCoroutine(IE_EffectFly());
        DicCorDelayOnEnd[effectId] = StartCoroutine(IE_DelayEndAction());
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

    //ֹͣ-��ЧЭ��
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
    //ֹͣ-�������Э��
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


    //ֹͣչʾ��Ч·����ʾ
    private void StopShowEffectPath()
    {
        //ֹͣ-��ЧЭ��
        StopIE_EffectFly();
        //ֹͣ-�������Э��
        StopIE_DelayOnEnd();
        //ֹͣtween
        ClearTween();
        //����entity����
        ClearEntity();
    }
    //����entity
    private void ClearEntity()
    {
        foreach (PoolEntity item in PoolEntityList)
        {
            item.Recycle();
        }
        PoolEntityList.Clear();
    }
    //��ֹtween
    private void ClearTween()
    {
        foreach (var item in tweenList)
        {
            item.Kill();
        }
        tweenList.Clear();
    }


}
