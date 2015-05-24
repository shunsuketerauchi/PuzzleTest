using UnityEngine;
using System.Collections;

public class GoalPanel : AttributeInterFace {

    [SerializeField, HeaderAttribute("ゴールとなる数")]
    private int m_TargetCount;

    private NumberRenderer m_numberRenderer;

    [SerializeField]
    private GameObject m_explosion;

    [SerializeField]
    private GameObject hanabi;

    [SerializeField]
    Transform m_hanabi_point;

	// Use this for initialization
	void Start () 
    {
        m_numberRenderer = GetComponentInChildren<NumberRenderer>();
        m_common_param = this.transform.root.GetComponent<PanelCommonParametor>();
        m_rigitBody = GetComponent<Rigidbody>();
	}
	
    void Check_Overlap()
    {
       GameObject panel_root = m_common_param.m_owner_Field.m_panel_Root;
        for(int i = 0 ; i < panel_root.transform.childCount ; i++)
        {
            GameObject it = panel_root.transform.GetChild(i).gameObject;
            float dist = (this.transform.position - it.transform.position).magnitude;
            if(dist < 0.001f)
            {
                if(Add_Score(it))
                {
                    Begin_Effect();
                    DestroyObject(it);
                    DestroyObject(this.gameObject);
                }

            }
        }
    }

    void Begin_Effect()
    {
        GameObject ex = GameObject.Instantiate(m_explosion);
        ex.transform.position = this.transform.position;

        GameObject h = GameObject.Instantiate(hanabi);
        h.transform.position = m_hanabi_point.position;

        for (int i = 0; i < this.transform.root.childCount; i++ )
        {
            var script = this.transform.root.GetChild(i).GetComponent<AttributeInterFace>();
            script.Explosion();
        }

    }

    private bool Add_Score(GameObject add_Object)
    {
        var normal_attribute = add_Object.GetComponent<NormalPanel>();
        if (!normal_attribute)
            return false;
        int sub_score = (int)normal_attribute.m_currentTime;

        m_TargetCount -= sub_score;
        return true;
    }


	// Update is called once per frame
	void Update () 
    {
        Check_Overlap();
        m_numberRenderer.SetNumber((int)m_TargetCount);
    }
	
}
