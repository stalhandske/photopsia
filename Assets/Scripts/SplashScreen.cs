using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplashScreen : MonoBehaviour {

    void Awake()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 1).SetDelay(3);
    }
}
