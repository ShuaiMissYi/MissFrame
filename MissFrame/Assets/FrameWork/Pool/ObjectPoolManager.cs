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
/// ����ع���
/// </summary>
/// �������˽ű�д�ú�����ϲ���Ҫ�Ķ��ű���ʹ�ô˽ű�ʱ��Ҫ�Ѵ˽ű����ڳ����У������޷�ʹ�ô˽ű�
public class ObjectPoolManager : SingletonMono<ObjectPoolManager>
{   //����

    //Dictionary�ֵ�
    //�����ֵ䣬��Ϊstring���ͣ�ֵΪList���͵ģ��ֵ������Ϊpool
    private Dictionary<int, List<PoolEntity>> pool;

    private void Awake()
    {
        pool = new Dictionary<int, List<PoolEntity>>();
    }

    #region ͬ������
    /// <summary>
    /// ȡ����ķ���
    /// </summary>
    /// <param name="objName">Ԥ�������֣�Ҳ�ǳ����������</param>
    /// <param name="pos">�ֲ�����</param>
    /// <param name="qua">�ֲ���ת</param>
    /// <returns>�õ��Ķ���</returns> GameObjectΪ����ֵ ���ص���ʵ�����Ķ���
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
                Debug.LogErrorFormat($"ResPrefabAsset���в����ڸ�resID��{resID}");
                return null;
            }
            GameObject prefab = Resources.Load<GameObject>(asset.Path);

            Debug.Log("prefab.name "+ prefab.name);
            if (null == prefab)
            {
                Debug.LogError($"prefabΪ�գ��������·���Ƿ���ȷ�� {asset.Path}");
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

    //�첽����ģ��
    private IEnumerator IE_AsyncLoadPrefab(CfgResPrefabAsset asset, Action<PoolEntity> callback)
    {
        ResourceRequest request = Resources.LoadAsync(asset.Path, typeof(GameObject));
        yield return request;
        if (request.isDone)
        {
            GameObject go = request.asset as GameObject;
            if (go == null)
            {
                Debug.LogError("go Ϊ��");
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

    #region �첽����
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
            Debug.LogErrorFormat($"ResPrefabAsset���в����ڸ�resID��{resID}");
            return;
        }
        StartCoroutine(IE_AsyncLoadPrefab(asset,callBack));
    }

    #endregion



    #region �س�
    //��ʵ�������������嶼�浽������
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





