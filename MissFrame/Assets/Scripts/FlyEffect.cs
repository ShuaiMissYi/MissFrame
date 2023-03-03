using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FlyEffect : MonoBehaviour
{
    public int CreatEffectNum = 5;

    public float Duration = 4f;

    public float Interval = 0.1f;

    public PathType pathType = PathType.CatmullRom;
    public GameObject Prefab;

    private List<PoolEntity> EffectList = new List<PoolEntity>(); 

    public Vector3[] waypoints = new[] {
        new Vector3(4, 2, 6),
        new Vector3(8, 6, 14),
        new Vector3(4, 6, 14),
        new Vector3(0, 6, 6),
        new Vector3(-3, 0, 0)
    };



    void Start()
    {
        //StartCoroutine(IE_EffectFly());
        CreatEffect();
    }

    private void CreatEffect()
    {
        int index = 0;
        for (int i = 0; i < CreatEffectNum; i++)
        {
            /*
            GameObject go = GameObject.Instantiate(Prefab);
            go.name = i.ToString();
            EffectList.Add(go.transform);
            */
            ObjectPoolManager.GetInstance().GetObjectFormPoolAsyncByResId(1,(entity)=>
            {
                index++;
                Debug.LogFormat($"index:  {index}");
                entity.gameObject.SetActive(true);
                EffectList.Add(entity);
                if (index==CreatEffectNum)
                {
                    StartCoroutine(IE_EffectFly());
                }
            });
        }
    }

    private IEnumerator IE_EffectFly()
    {
        Debug.LogFormat("开始协程");
        for (int i = 0; i < CreatEffectNum; i++)
        {
            TargetFly(EffectList[i].transform,i);
            yield return new WaitForSeconds(Interval);
        }
    }


    private void TargetFly(Transform target,int curIndex)
    {
        Tween t = target.DOPath(waypoints, Duration, pathType, PathMode.Full3D,50,Color.red)
            .SetOptions(false)
            .SetLookAt(0.001f);
        t.SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }












}
