using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameField : MonoBehaviour {

    [SerializeField, HeaderAttribute("縦マス目")]
    private int m_numSquares_Y = 3;

    [SerializeField, HeaderAttribute("横マス目")]
    private int m_numSquares_X = 3;

    [SerializeField, HeaderAttribute("マスのプロトタイプ")]
    GameObject m_square_Prototype;

    private List<Squares> m_squares_List;

    public GameObject m_square_Root { get; private set; }
    public GameObject m_panel_Root{get ; private set;}

	// Use this for initialization
	void Start () 
    {
        m_squares_List = new List<Squares>();
        Vector3 scale = new Vector3((float)m_numSquares_X, (float)m_numSquares_Y, 1.0f);
        this.transform.localScale = scale;
        m_square_Root = this.transform.FindChild("SquareRoot").gameObject;
        m_panel_Root = GameObject.Find("PanelRoot");
        Initialize_Squares();
        Initlize_Panel();
	}

    private void Initialize_Squares()
    {
        //とりえあず作成
        int create_num = m_numSquares_X * m_numSquares_Y;
        for (int i = 0; i < create_num; i++)
        {
            GameObject insert = GameObject.Instantiate(m_square_Prototype);
            m_squares_List.Add(insert.GetComponent<Squares>());
            insert.transform.parent = m_square_Root.transform;
        }

        //配置
        int count = 0;
        int x_adjust = m_numSquares_X / 2;
        int y_adjust = m_numSquares_Y / 2;
        for (int x = 0; x < m_numSquares_X; x++)
        {
            for (int y = 0; y < m_numSquares_Y; y++)
            {
                Vector3 position;
                position.x = x - x_adjust;
                position.y = y - y_adjust;
                position.z = 0;

                //やっつけ
                if (m_numSquares_X % 2 == 0)
                    position.x += 0.5f;
                if (m_numSquares_Y % 2 == 0)
                    position.y += 0.5f;

                m_squares_List[count].transform.position = position;
                count++;
            }
        }
    }

    void Initlize_Panel()
    {
        float most_Near = Mathf.Infinity;
        Vector3 position = new Vector3(0, 0, 0);
        for (int i = 0; i < m_panel_Root.transform.childCount; i++)
        {
            for (int j = 0; j < m_square_Root.transform.childCount; j++)
            {
                float d = (m_panel_Root.transform.GetChild(i).position - m_square_Root.transform.GetChild(j).position).magnitude;
                if (d < most_Near)
                {
                    most_Near = d;
                    position = m_square_Root.transform.GetChild(j).position;
                }
            }
            //ここでパネルが所属する場所が決まってる
            m_panel_Root.transform.GetChild(i).position = position;
            var attributeManager = m_panel_Root.transform.GetChild(i).GetComponent<PanelAttributeManager>();
            // attributeManager.Initialize_LocalScale(this.transform.localScale);
            most_Near = Mathf.Infinity;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

}
