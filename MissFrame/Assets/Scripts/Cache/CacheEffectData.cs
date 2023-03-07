using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheEffectData : CacheBaseData
{

    //��Ч����ʵ�������б�
    private List<PoolEntity> m_EntityList = new List<PoolEntity>();
    public List<PoolEntity> EntityList => m_EntityList;
    //�Ӳ���켣Э��-ÿ�����е�֮��ļ��ʱ��
    private Coroutine m_CorEffectFly;
    public Coroutine CorEffectFly => m_CorEffectFly;
    //�Ӳ���Э��-����һȦ��Ҫ��ʱ��
    private Coroutine m_CorDelayOnEnd;
    public Coroutine CorDelayOnEnd => m_CorDelayOnEnd;
    //ÿ������е�tween
    private List<Tween> m_TweenList = new List<Tween>();
    public List<Tween> TweenList => m_TweenList;

    public CacheEffectData(int subStepId) : base(subStepId)
    {
        
    }

    public void AddPoolEntity(PoolEntity entity)
    {
        m_EntityList.Add(entity);
    }
    public void SetCorEffectFly(Coroutine cor)
    {
        m_CorEffectFly = cor;
    }
    public void SetCorDelayOnEnd(Coroutine cor)
    {
        m_CorDelayOnEnd = cor;
    }
    public void AddTween(Tween tw)
    {
        m_TweenList.Add(tw);
    }


}
