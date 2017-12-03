using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
[ExecuteInEditMode]
public class IK : MonoBehaviour {

    public System.Action DidUpdate;

    public Transform targetPos;
    public Transform elbow;
    public Transform hand;
    [HideInInspector]
    public bool invert;
     float d1;
      float d2;
    [HideInInspector]
    public bool toggleIK=false;

    public void Start()
    {

        UpdateBones();
        targetPos.GetComponent<Limb>().DidUpdate += MyUpdate;
    }

    public void UpdateBones()
    {
        float rot1, rot2;
        rot1 = transform.localEulerAngles.z;
        rot2 = transform.GetChild(0).localEulerAngles.z;

        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
        
        transform.localEulerAngles = invert ? new Vector3(0, 0, 180): new Vector3(0, 0, 0);
      

        //d1 = GetComponent<SpriteRenderer>().bounds.size.x;
        //d2 = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

        d1 = (elbow.position - transform.position).magnitude;
        d2 = (hand.position - elbow.position).magnitude;


        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);

        transform.localEulerAngles = new Vector3(0, 0, rot1) ;
        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, rot2);
    }

    float theta1 = 0;
    float theta2 = 0;

    bool outOfRange;
    public void MyUpdate()
    {

        float dx = targetPos.position.x - transform.position.x;
        float x = dx >0 ? Mathf.Abs( dx) : -Mathf.Abs(dx);
        float dy = targetPos.position.y - transform.position.y;
        float y = dy > 0 ? Mathf.Abs(dy) : -Mathf.Abs(dy);
       
        float num = Mathf.Pow(x, 2) + Mathf.Pow(y, 2) - Mathf.Pow(d1, 2) - Mathf.Pow(d2, 2);
        float denom = 2 * d1 * d2;

        outOfRange = (Mathf.Abs(num / denom) > 1);

        float costheta2 = Mathf.Clamp(num / denom, -1, 1);
    
        theta2 = Mathf.Acos(costheta2);

        if (invert)
            theta2 = -theta2;

        float atx = y * (d1 + d2 * Mathf.Cos(theta2)) - x * (d2 * Mathf.Sin(theta2));
        float aty = x * (d1 + d2 * Mathf.Cos(theta2)) + y * (d2 * Mathf.Sin(theta2));

         theta1 = y==0 && x==0? 0 : Mathf.Atan2(atx, aty);
        if (toggleIK)
        {
            transform.localEulerAngles = new Vector3(0, 0, (Mathf.Rad2Deg * theta1));
            transform.GetChild(0).localEulerAngles = new Vector3(0, 0, (Mathf.Rad2Deg * theta2));
        }

        if (DidUpdate != null)
            DidUpdate();
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Selection.Contains(targetPos.gameObject)&& toggleIK )
        {
            Gizmos.DrawIcon(targetPos.position, "IKHandle.png", false);

            Gizmos.color =  outOfRange ? Color.red: Color.green;
            Gizmos.DrawLine(transform.GetChild(0).position, targetPos.position);
            Gizmos.DrawLine(transform.position, targetPos.position);
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + d1 * Mathf.Cos(theta1), transform.position.y + d1 * Mathf.Sin(theta1), 0));
        }
#endif

    }

}
