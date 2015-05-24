using UnityEngine;
using System.Collections;

public class PanelAttributeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void Initialize_LocalScale(Vector3 parentScale)
    {
        Vector3 scale = new Vector3(1, 1, 1);

        scale.x = 1.0f / parentScale.x;
        scale.y = 1.0f / parentScale.y;
        this.transform.localScale = scale;
    }

    ////とりあえず
    //private void Move()
    //{


    ////public void Move_Begin(GameObject moveTarget)
    ////{
    ////    m_current_Movetarget = moveTarget;
    ////}

}
