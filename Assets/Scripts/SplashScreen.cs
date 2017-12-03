using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplashScreen : MonoBehaviour
{

    public float timeToResplash;

    bool _isDone;

    void Awake()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 3).SetDelay(5);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > timeToResplash && !_isDone)
        {
            _isDone = true;
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1, 6).OnComplete(()=>StartCoroutine(Quit()));
        }
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
