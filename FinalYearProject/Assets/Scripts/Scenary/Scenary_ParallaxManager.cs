using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenary_ParallaxManager : MonoBehaviour {

    [SerializeField]
    private Anim_AnimationController AnimController;
    [SerializeField]
    private Renderer Sprite;
    [SerializeField]
    private float ScrollMultiplier = 1f;
    
    private Vector2 mOffset = Vector2.zero;

    private void Update()
    {
        mOffset.x += Time.deltaTime * ScrollMultiplier * (AnimController != null ? AnimController.pWalkSpeed : 1f);
        Sprite.material.mainTextureOffset = mOffset;
    }
}
