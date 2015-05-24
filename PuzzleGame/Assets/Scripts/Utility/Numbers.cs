using UnityEngine;
using System.Collections;

public class Numbers : MonoBehaviour {

    private MeshRenderer m_renderer;
    private Material m_currentMaterial =null;

    void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_currentMaterial = m_renderer.material;
    }
    public void Disalble()
    {
        m_renderer.enabled = false;
    }

    public void Enable()
    {
        m_renderer.enabled = true;
    }

    public  void SetMaterial(Material insert)
    {
        m_currentMaterial = insert;
        m_renderer.material = m_currentMaterial;
    }

}
