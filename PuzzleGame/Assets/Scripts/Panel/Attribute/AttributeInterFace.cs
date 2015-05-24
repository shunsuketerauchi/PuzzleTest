using UnityEngine;
using System.Collections;

public class AttributeInterFace : MonoBehaviour 
{
    protected Rigidbody m_rigitBody;

    public PanelCommonParametor m_common_param { get; protected set; }

    public virtual void Move_Begin(GameObject moveTarget) { }

     public void Explosion()
    {
        Vector3 rand;
        rand.x = Random.Range(0f, 1.0f);
        rand.y = Random.Range(0f, 1.0f);
        rand.z = Random.Range(0f, 1.0f);
        rand.Normalize();
        rand *= Random.Range(1.0f, 3.0f);
        m_rigitBody.AddForce(rand);
    }
}
