using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlashUI : MonoBehaviour
{
    public float flashTime = 0.1f;
    public float timeBetweenFlashes;
    public float timeToDevelopePic;
    
    public Image flashImage;


    void Awake()
    {
        FrameController.OnSnap += FrameController_OnSnap;
    }

    void FrameController_OnSnap()
    {
        StartCoroutine(SnapCr());
    }

    IEnumerator SnapCr()
    {
        flashImage.enabled = true;
        yield return new WaitForSeconds(flashTime);
        flashImage.enabled = false;
        //yield return new WaitForSeconds(timeBetweenFlashes);
        //flashImage.enabled = true;
        //yield return new WaitForSeconds(flashTime);
        //flashImage.enabled = false;
        yield return null;
    }
}
