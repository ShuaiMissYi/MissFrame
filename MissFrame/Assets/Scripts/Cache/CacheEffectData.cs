using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheEffectData : CacheBaseData
{

    //特效对象实体数据列表
    private List<PoolEntity> m_EntityList = new List<PoolEntity>();
    public List<PoolEntity> EntityList => m_EntityList;
    //子步骤轨迹协程-每个飞行点之间的间隔时间
    private Coroutine m_CorEffectFly;
    public Coroutine CorEffectFly => m_CorEffectFly;
    //子步骤协程-播放一圈需要的时长
    private Coroutine m_CorDelayOnEnd;
    public Coroutine CorDelayOnEnd => m_CorDelayOnEnd;
    //每个点飞行的tween
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
