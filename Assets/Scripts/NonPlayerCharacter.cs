using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelSpriteGenerator;

public class NonPlayerCharacter : MonoBehaviour
{
    public Color color;
    public Vector2 sizeRange;

    SpriteRenderer _spriteRenderer;
    public float size { get; private set; }

    Texture2D _tex;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        size = Random.Range(sizeRange.x, sizeRange.y);
        transform.localScale =Vector3.one*size;


        PsgMask mask = new PsgMask(new int[]
        {
            0, 0, 0, 0,
            0, 1, 1, 1,
            0, 1, 2, 2,
            0, 0, 1, 2,
            0, 0, 0, 2,
            1, 1, 1, 2,
            0, 1, 1, 2,
            0, 0, 0, 2,
            0, 0, 0, 2,
            0, 1, 2, 2,
            1, 1, 0, 0
        }, 4, 11, true, false);

        float spritePadding = 1f;

        PsgOptions options = new PsgOptions()
        {
            Colored = false,
            EdgeBrightness = 0.3f,
            ColorVariations = 0.2f,
            BrightnessNoise = 0.3f,
            Saturation = 0.5f,
            MonochromeColor = color,
        };

        PsgSprite psgSprite = new PsgSprite(mask,options);

        _tex = psgSprite.texture;
        _tex.wrapMode = TextureWrapMode.Clamp;
        _tex.filterMode = FilterMode.Bilinear;
        psgSprite.width *= 5;
        psgSprite.height *= 5;

        TextureScale.Point(_tex,psgSprite.width*2,psgSprite.height);

        _tex.Apply();

        _spriteRenderer.sprite = Sprite.Create(_tex, new Rect(0, 0, psgSprite.width, psgSprite.height), new Vector2(0.5f, 0), 64f);

    }

    void Update()
    {
        
    }
}