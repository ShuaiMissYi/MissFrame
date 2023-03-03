using MissFrame.Cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEntity:MonoBehaviour
{
    private CfgResPrefabAsset m_CfgResPrefabAsset;
    public CfgResPrefabAsset CfgResPrefabAsset => m_CfgResPrefabAsset;

    public void InitCfgData(CfgResPrefabAsset asset)
    {
        m_CfgResPrefabAsset = asset;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Recycle()
    {
        ObjectPoolManager.GetInstance().PushObjectToPool(this);
        ResetPrefab();
        gameObject.SetActive(false);
        Debug.Log("�����ѻ���");
    }

    //����Ԥ������
    private void ResetPrefab()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
        
    }


}
