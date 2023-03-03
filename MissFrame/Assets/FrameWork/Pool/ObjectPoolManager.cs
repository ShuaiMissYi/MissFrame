using MissFrame.Cfg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 对象池管理
/// </summary>
/// ！！！此脚本写好后基本上不需要改动脚本，使用此脚本时需要把此脚本挂在场景中，否则无法使用此脚本
public class ObjectPoolManager : SingletonMono<ObjectPoolManager>
{   //单例

    //Dictionary字典
    //创建字典，键为string类型，值为List类型的，字典的名字为pool
    private Dictionary<int, List<PoolEntity>> pool;

    private void Awake()
    {
        pool = new Dictionary<int, List<PoolEntity>>();
    }

    #region 同步出池
    /// <summary>
    /// 取对象的方法
    /// </summary>
    /// <param name="objName">预设体名字，也是池子里的名字</param>
    /// <param name="pos">局部坐标</param>
    /// <param name="qua">局部旋转</param>
    /// <returns>得到的对象</returns> GameObject为返回值 返回的是实例化的对象
    public PoolEntity GetObjectFormPool(int resID)
    {
        PoolEntity entity = null;
        if (pool.ContainsKey(resID) && pool[resID].Count>0)
        {
            entity = pool[resID][0];
            pool[resID].RemoveAt(0);
        }
        else
        {
            Dictionary<int, CfgResPrefabAsset> map = CfgUtility.GetInstance().CfgTab.TbResPrefabAsset.DataMap;
            CfgResPrefabAsset asset = null;
            if (!map.TryGetValue(resID, out asset))
            {
                Debug.LogErrorFormat($"ResPrefabAsset表中不存在该resID：{resID}");
                return null;
            }
            GameObject prefab = Resources.Load<GameObject>(asset.Path);

            Debug.Log("prefab.name "+ prefab.name);
            if (null == prefab)
            {
                Debug.LogError($"prefab为空，请检查加载路径是否正确： {asset.Path}");
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab);
            if (!go.TryGetComponent(out entity))
            {
                entity = go.AddComponent<PoolEntity>();
                entity.InitCfgData(asset);
            }
        }
        entity.gameObject.SetActive(true);
        return entity;
    }
    #endregion

    //异步生成模型
    private IEnumerator IE_AsyncLoadPrefab(CfgResPrefabAsset asset, Action<PoolEntity> callback)
    {
        ResourceRequest request = Resources.LoadAsync(asset.Path, typeof(GameObject));
        yield return request;
        if (request.isDone)
        {
            GameObject go = request.asset as GameObject;
            if (go == null)
            {
                Debug.LogError("go 为空");
            }
            go = GameObject.Instantiate(go);
            go.SetActive(true);
            PoolEntity entity = null;
            if (!go.TryGetComponent(out entity))
            {
                entity = go.AddComponent<PoolEntity>();
                entity.InitCfgData(asset);
            }
            callback?.Invoke(entity);
        }
    }

    #region 异步出池
    public void GetObjectFormPoolAsyncByResId(int resID, Action<PoolEntity> callBack)
    {
        PoolEntity entity = null;
        if (pool.ContainsKey(resID) && pool[resID].Count > 0)
        {
            entity = pool[resID][0];
            entity.gameObject.SetActive(true);
            pool[resID].RemoveAt(0);
            callBack?.Invoke(entity);
            return;
        }
        Dictionary<int, CfgResPrefabAsset> map = CfgUtility.GetInstance().CfgTab.TbResPrefabAsset.DataMap;
        CfgResPrefabAsset asset = null;
        if (!map.TryGetValue(resID,out asset))
        {
            Debug.LogErrorFormat($"ResPrefabAsset表中不存在该resID：{resID}");
            return;
        }
        StartCoroutine(IE_AsyncLoadPrefab(asset,callBack));
    }

    #endregion



    #region 回池
    //把实例化出来的物体都存到池子中
    public void PushObjectToPool(PoolEntity entity)
    {
        int resID = entity.CfgResPrefabAsset.Id;
        if (pool.ContainsKey(resID))
        {
            pool[resID].Add(entity);
        }
        else
        {
            pool[resID] = new List<PoolEntity>() { entity };
        }
    }
    #endregion


}





