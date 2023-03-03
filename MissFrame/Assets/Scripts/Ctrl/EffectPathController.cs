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

    //��Ч�������Э��
    private Coroutine m_CoroutineEffectFly;
    //��һ����Ч�������Э��
    private Coroutine m_CoroutineDelayOnEnd;

    private List<Tween> tweenList = new List<Tween>();

    public EffectPathController()
    {
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
        Debug.LogFormat("��ʼʹ��entity");
        for (int i = 0; i < m_CreatEffectNum; i++)
        {
            ObjectPoolManager.GetInstance().GetObjectFormPoolAsyncByResId(m_EffectData.CfgShowEffectPathData.EffectResID, (entity) =>
            {
                index++;
                entity.gameObject.SetActive(true);
                PoolEntityList.Add(entity);
                if (index == m_CreatEffectNum)
                {
                    //������ϣ���ʼ���Ź켣��Ч
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
        //Debug.LogError("�ִ��յ�");
        OnEndExcuteStep();
    }

    private IEnumerator IE_EffectFly()
    {
        Debug.LogFormat("��ʼЭ��");
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

    //ֹͣ-��ЧЭ��
    private void StopIE_EffectFly()
    {
        if (null != m_CoroutineEffectFly)
        {
            StopCoroutine(m_CoroutineEffectFly);
            m_CoroutineEffectFly = null;
        }
    }
    //ֹͣ-�������Э��
    private void StopIE_DelayOnEnd()
    {
        if (null != m_CoroutineDelayOnEnd)
        {
            StopCoroutine(m_CoroutineDelayOnEnd);
            m_CoroutineDelayOnEnd = null;
        }
    }


    //ֹͣչʾ��Ч·����ʾ
    private void StopShowEffectPath()
    {
        //ֹͣ-��ЧЭ��
        StopIE_EffectFly();
        //ֹͣ-�������Э��
        StopIE_DelayOnEnd();
        //����-����ʱ�Ĳ�����  todo
        ResetEndStep();
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

    //��һ����Ч�ִ��ص��ִ�еĲ���
    private void OnEndExcuteStep()
    {
        List<int> idList = m_EffectData.CfgShowEffectPathData.OnEndStepIdList;
        Debug.LogFormat("��Ч�����յ㣬ִ����ز���");
        EventDispatcher.GetInstance().DispatchEvent(EventType.FinishSubStep, m_EffectData.SubStep);
        foreach (var id in idList)
        {
            //ִ�еĲ�����ʱ������������
            SubStepData data = CfgManager.GetInstance().GetSubStepData(id);
            data.Run();
        }
    }

    //����-�ִ��յ��Ĳ������
    private void ResetEndStep()
    {
        List<int> idList = m_EffectData.CfgShowEffectPathData.OnEndStepIdList;
        Debug.LogFormat("����-�ִ��յ��Ĳ������");
        foreach (var id in idList)
        {
            SubStepData data = CfgManager.GetInstance().GetSubStepData(id);
            data.ResetSubStep();
        }
    }



}
