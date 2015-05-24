using UnityEngine;
using System.Collections;

public class NormalPanel : AttributeInterFace
{
    [SerializeField, HeaderAttribute("スタート時の経過時間"), Range(0, 100)]
    private float m_progressTime = 0;

    public float m_currentTime { get; private set; }

    [SerializeField, HeaderAttribute("時間経過の速さ")]
    public float m_timeScale = 1.0f;

    private NumberRenderer m_numberRenderer;
    
    private float   m_instantiate_Time;

    //とりあえず
    private GameObject m_current_Movetarget = null;

    private bool m_isRockTimer = false;

	// Use this for initialization
	void Awake () 
    {
        m_instantiate_Time = Time.time;
        m_numberRenderer = GetComponentInChildren<NumberRenderer>();
        m_rigitBody = GetComponent<Rigidbody>();
     
	}
	
    void UpdateTime()
    {
        m_currentTime = (Time.time - m_instantiate_Time) * m_timeScale + m_progressTime;
    }

    void Move()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, m_current_Movetarget.transform.position, 0.1f);
        float dist = (this.transform.position - m_current_Movetarget.transform.position).magnitude;
        if(dist < 0.001f)
        {
            this.transform.position = m_current_Movetarget.transform.position;
            m_current_Movetarget = null;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (m_current_Movetarget)
        {
            Move();
        }
        if(!m_isRockTimer)
            UpdateTime();
        m_numberRenderer.SetNumber((int)m_currentTime);
	}

    public override void Move_Begin(GameObject moveTarget)
    {
        m_current_Movetarget = moveTarget;
        //MoveTargetのパネルがGoalの場合はストップ
        var goal_script = moveTarget.GetComponent<GoalPanel>();
        if (goal_script)
            m_isRockTimer = true;
    }

    public void SetParametor(float progresstime,float timescale)
    {
        m_timeScale = timescale;
        m_progressTime = progresstime;
    }

}
