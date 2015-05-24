using UnityEngine;
using System.Collections;

public class Touch_Collider : MonoBehaviour {

    //public bool m_isActive = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public  bool Is_Touch()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (gameObject.GetComponent<MeshCollider>().Raycast(ray, out hit, 50.0f) == true)
            return true;

        return false;
    }
}
