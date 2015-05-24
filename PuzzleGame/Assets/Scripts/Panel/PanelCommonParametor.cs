using UnityEngine;
using System.Collections;

public class PanelCommonParametor : MonoBehaviour {

    public GameField m_owner_Field { get; private set; } 
  

	// Use this for initialization
	void Start () 
    {
        m_owner_Field = GameObject.Find("GameField").GetComponent<GameField>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
