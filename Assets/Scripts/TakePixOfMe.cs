using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePixOfMe : MonoBehaviour
{

    public event System.Action onView;
    public event System.Action onOutOfView;
    public event System.Action onSnap;


    public void SetInView()
    {
        if (onView != null)
        {
            onView();
        }
    }

    public void SetOutOfView()
    {
        if (onOutOfView != null)
        {
            onOutOfView();
        }
    }

    public void TakeSnapShot()
    {
        if (onSnap != null)
        {
            onSnap();
        }
    }
}
