using UnityEngine;
using System.Collections;

public class ScaleWeaver : MonoBehaviour {

    [SerializeField, HeaderAttribute("ScaleをいじるターゲットObject")]
    private GameObject m_target;

    private bool m_is_Active = false;

    [SerializeField, HeaderAttribute("周期が終わる時間")]
    private float m_wave_Time = 0.3f;

    private float m_begin_Time;

    private Vector3 m_owner_Scale;

	// Use this for initialization
	void Start () 
    {
        m_owner_Scale = m_target.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_is_Active)
            Wave();
	}

    void Wave()
    {
        float progress_Time = Time.time - m_begin_Time;
        float rate = Mathf.PI * progress_Time;

        Vector3 scale = m_owner_Scale;
        Vector3 current_Scale = new Vector3((scale.x *  Mathf.Sin(rate)) +m_owner_Scale.x,
                                                                         (scale.y * Mathf.Sin(rate)) + m_owner_Scale.y,
                                                                         (scale.z * Mathf.Sin(rate)) + m_owner_Scale.z);
        m_target.transform.localScale = current_Scale;
        if (rate >= Mathf.PI)
        {
            m_target.transform.localScale = m_owner_Scale;
            m_is_Active = false;

        }

    }

    public void Begin_Wave()
    {
        m_is_Active = true;
        m_begin_Time = Time.time;
    }
}
