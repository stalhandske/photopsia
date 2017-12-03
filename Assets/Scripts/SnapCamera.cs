using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCamera : MonoBehaviour {

    Texture2D _lastSnap;
    bool _doGrab;

    public Transform photoParent;
    public GameObject photoPrefab;
    public Transform photoPropOrigin;
    public GameObject photoPropPrefab;
    public RenderTexture renderTexture;

    void Awake()
    {
        FrameController.OnSnap += FrameController_OnSnap;
    }

    void FrameController_OnSnap()
    {
	    _doGrab = true;
    }

    void Update()
    {
        if (_doGrab)
        {
            RenderTexture.active = renderTexture;

            // Take snap
            _lastSnap = new Texture2D(256, 256);
            _lastSnap.ReadPixels(new Rect(0,0,256,256), 0, 0);
            _lastSnap.Apply();

            // Create prefab
            StaticImage si = Instantiate(photoPrefab, photoParent,false).GetComponent<StaticImage>();
            si.Initialize(_lastSnap);

            //_lastSnap.Resize(10, 10);
            Sprite s = Sprite.Create(_lastSnap,new Rect(0,0,256,256),new Vector2(0.5f,0.5f),256);
            PhotoProp pp = Instantiate(photoPropPrefab,(Vector2)photoPropOrigin.position,Quaternion.identity,null).GetComponent<PhotoProp>();
            pp.Initialize(s);

            _doGrab = false;
        }
    }
}
