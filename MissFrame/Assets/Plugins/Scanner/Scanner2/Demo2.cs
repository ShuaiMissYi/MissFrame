using UnityEngine;

public class Demo2 : MonoBehaviour
{
    //是否运行扫描特效
    public bool m_IsExecute=false;

	public bool IsActive 
	{
		set
		{
            m_IsExecute = value;
            m_FxType = Scanner.ScannerObject.FxType.FT_None;
            m_FxType = Scanner.ScannerObject.FxType.FT_Additional;
        }
	}



	public enum ScanMode { SCAN_DIR = 0, SCAN_SPH };
	[Header("Parameters")]
	public Scanner.ScannerObject.FxType m_FxType = Scanner.ScannerObject.FxType.FT_Additional;
	public ScanMode m_ScanMode = ScanMode.SCAN_DIR;
	public GameObject m_Emitter;
	public Vector4 m_Dir = new Vector4 (1, 0, 0, 0);
	[Range(0.1f, 2f)] public float m_Amplitude = 1f;
	[Range(1f, 16f)] public float m_Exp = 3f;
	[Range(8f, 64f)] public float m_Interval = 20f;
	[Range(1f, 32f)] public float m_Speed = 10f;
	[Header("Internal")]
	public Scanner.ScannerObject[] m_Fxs;

	void Start ()
	{
		m_Fxs = transform.GetComponentsInChildren<Scanner.ScannerObject> ();
		for (int i = 0; i < m_Fxs.Length; i++)
			m_Fxs[i].Initialize ();
	}
	void Update ()
	{
		if (!m_IsExecute)
		{
			return;
		}
		for (int i = 0; i < m_Fxs.Length; i++)
		{
			m_Fxs[i].ApplyFx (m_FxType);
			m_Fxs[i].UpdateSelfParameters ();
			if (ScanMode.SCAN_DIR == m_ScanMode)
			{
				m_Fxs[i].ApplyDirectionalScan (m_Dir);
				m_Fxs[i].SetMaterialsVector ("_LightSweepVector", m_Dir);
			}
			else if (ScanMode.SCAN_SPH == m_ScanMode)
			{
				m_Fxs[i].ApplySphericalScan ();
				m_Fxs[i].SetMaterialsVector ("_LightSweepVector", m_Emitter.GetComponent<Transform> ().position);
			}
			m_Fxs[i].SetMaterialsFloat ("_LightSweepAmp", m_Amplitude);
			m_Fxs[i].SetMaterialsFloat ("_LightSweepExp", m_Exp);
			m_Fxs[i].SetMaterialsFloat ("_LightSweepInterval", m_Interval);
			m_Fxs[i].SetMaterialsFloat ("_LightSweepSpeed", m_Speed);
		}
	}
}
