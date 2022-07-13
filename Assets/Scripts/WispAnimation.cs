using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispAnimation : MonoBehaviour
{
    public float speed = 1f;
    public float hoverHeight = 0f;

    private float time = 0f;
    private AnimationCurve hoverAnimation;
    private Vector3 highPosition;
    private Vector3 lowPosition;

    private void Awake()
    {
        hoverAnimation = new AnimationCurve();
        hoverAnimation.postWrapMode = WrapMode.Loop;
        hoverAnimation.AddKey(0f, 0f);
        hoverAnimation.AddKey(0.5f, 1f);
        hoverAnimation.AddKey(1f, 0f);

        highPosition = gameObject.transform.localPosition + Vector3.up * hoverHeight;
        lowPosition = highPosition * -1;

        gameObject.transform.localPosition = lowPosition;
    }

    void Update()
    {
        AnimateHovering();
        AnimateRotation();
    }

    void AnimateHovering()
    {
        time += Time.deltaTime;
        gameObject.transform.localPosition = Vector3.LerpUnclamped(lowPosition, highPosition, hoverAnimation.Evaluate(time));
    }

    void AnimateRotation()
    {
        Vector3 rotation = Vector3.down * speed;
        gameObject.transform.Rotate(rotation, Space.Self);
    }
}
