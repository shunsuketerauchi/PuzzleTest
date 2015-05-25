using UnityEngine;
using System.Collections;

public class MulPanel : AttributeInterFace {

    private GameObject m_hold_NormalPanel;

    [SerializeField]
    private GameObject m_nanel_prototype;


    void Start()
    {
        m_common_param = this.transform.root.GetComponent<PanelCommonParametor>();
        m_rigitBody = GetComponent<Rigidbody>();
    }

    //とりあえずやっつけ
    void Check_Hold_Panel()
    {
        GameObject panel_root = m_common_param.m_owner_Field.m_panel_Root;
        for (int i = 0; i < panel_root.transform.childCount; i++)
        {
            GameObject it = panel_root.transform.GetChild(i).gameObject;
            float dist = (this.transform.position - it.transform.position).magnitude;
            if (dist < 0.01f)
            {
                //普通のパネルかどうかチェック
                var panel_script = it.GetComponent<NormalPanel>();
                if (panel_script)
                {
                    m_hold_NormalPanel = it;
                    break;
                }
            }
        }
    }

    void RemoveCheck()
    {
        float dist = (this.transform.position - m_hold_NormalPanel.transform.position).magnitude;
        if (dist > 0.1f)
            m_hold_NormalPanel = null;
    }

    bool Mul_Check()
    {
        GameObject panel_root = m_common_param.m_owner_Field.m_panel_Root;
        for (int i = 0; i < panel_root.transform.childCount; i++)
        {
            GameObject it = panel_root.transform.GetChild(i).gameObject;
            if (it == m_hold_NormalPanel)
                continue;
            float dist = (this.transform.position - it.transform.position).magnitude;
            if (dist < 0.01f)
            {
                //普通のパネルかどうかチェック
                var panel_script = it.GetComponent<NormalPanel>();
                if (panel_script)
                {
                    Mul_Score(it);
                    return true;

                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_hold_NormalPanel)
        {
            Check_Hold_Panel();
        }
        else
        {
            RemoveCheck();
            if (Mul_Check())
                DestroyObject(this.gameObject);
        }
    }

    private void Mul_Score(GameObject add_object)
    {
        var hold_script = m_hold_NormalPanel.GetComponent<NormalPanel>();
        var add_script = add_object.GetComponent<NormalPanel>();
        GameObject insert = GameObject.Instantiate(m_nanel_prototype);
        insert.transform.parent = this.transform.root;
        insert.transform.position = this.transform.position;
        insert.transform.rotation = this.transform.rotation;

        var script = insert.GetComponent<NormalPanel>();
        int time = (int)(hold_script.m_currentTime * add_script.m_currentTime);
        script.SetParametor((float)time,
            hold_script.m_timeScale * 2.0f);

        DestroyObject(m_hold_NormalPanel);
        DestroyObject(add_object);
    }
}
