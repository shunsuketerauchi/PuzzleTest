using UnityEngine;
using System.Collections;

public class FlickChecker : MonoBehaviour {

    private GameField m_owner_Field;

    private Vector3 m_startScreenPosition;
    private Vector3 m_endScreenPosition;

    [SerializeField, HeaderAttribute("フリックのしきい値となるなす角(Radian)"), Range(0, Mathf.PI)]
    private float m_limit_Angle = 0.8f;

    //とりあえず
    private float m_limit_Dist = 1.1f;

    GameObject m_current_FlickObject = null; 

	// Use this for initialization
	void Start () 
    {
        m_owner_Field = GetComponent<GameField>();
	}
	
    private void Touch_Begin()
    {
        for(int i = 0 ;  i < m_owner_Field.m_panel_Root.transform.childCount ; i++)
        {
            GameObject it = m_owner_Field.m_panel_Root.transform.GetChild(i).gameObject;
            var collider_Script = it.GetComponent<Touch_Collider>();
            if (!collider_Script)
                continue;
           if(collider_Script.Is_Touch())
           {
               m_current_FlickObject = it;
               m_startScreenPosition = Input.mousePosition;
               m_startScreenPosition.z = 0f;
           }
        }

    }

    private GameObject GetAffiliationPanel(Vector3 pos)
    {
        float min_dist = Mathf.Infinity;
        GameObject panel_root = m_owner_Field.m_panel_Root;
        GameObject min_obj = null;
        for(int i = 0 ; i < panel_root.transform.childCount ; i++)
        {
            GameObject it = panel_root.transform.GetChild(i).gameObject;
            if (it == m_current_FlickObject)
                continue;
            float dist = (it.transform.position - pos).magnitude;
            if(dist < min_dist)
            {
                min_obj = it;
                min_dist = dist;
            }
        }
        if (min_dist < 0.01f)
            return min_obj;
        return null;
    }

    private void Touch_End()
    {
        if(!m_current_FlickObject)
        {
            Debug.Log("m_current_FlickObject is null in Touch end !!");
            return;
        }

        m_endScreenPosition = Input.mousePosition;
        m_endScreenPosition.z = 0f;

        Vector3 start_to_end = m_endScreenPosition - m_startScreenPosition;
        //GameObject MoveTarget = null;
        //移動ターゲットとなるマスを決定する（とりあえず線形探索）
        for(int i = 0 ; i < m_owner_Field.m_square_Root.transform.childCount ; i++)
        {
            GameObject it = m_owner_Field.m_square_Root.transform.GetChild(i).gameObject;
            //距離測定
            Vector3 vec =  it.transform.position - m_current_FlickObject.transform.position;
            if (vec.magnitude >= m_limit_Dist)
                continue;
            //角度判定
            float angle = Vector3.Dot(start_to_end.normalized, vec.normalized);
            angle = Mathf.Acos(angle);
            if (angle > m_limit_Angle)
                continue;

            //移動する
            var panelscript = m_current_FlickObject.GetComponent<AttributeInterFace>();
            if(!panelscript)
            {
                Debug.Log("null!");
            }
            GameObject panel = 
                GetAffiliationPanel(it.transform.position);
            if(panel)
                panelscript.Move_Begin(panel);
            else
                panelscript.Move_Begin(it);
            
            m_current_FlickObject = null;
            break;
        }

    }

	// Update is called once per frame
	void Update () 
    {
	   if(Input.GetKeyDown(KeyCode.Mouse0))
       {
           Touch_Begin();
       }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Touch_End();
        }
	}
}
