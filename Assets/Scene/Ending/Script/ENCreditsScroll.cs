using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ENCreditsScroll : MonoBehaviour
{
    public float toY;
    public float scrollSpeed;
    public float scrollSpeedFast;
    public float startDelay;
    public float endDelay;
    public UnityEvent endCallback;

    private CanvasGroup _group;
    private bool _didEnd;

    private RectTransform _rt;
    private float _startDelayDuration;
    
    private void OnEnable()
    {
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0f;
        _rt = (RectTransform) transform;
    }

    private void Update()
    {
        if (_didEnd) return;
        if (_group.alpha < 0.99f)
        {
            _group.alpha += Time.deltaTime;
            return;
        }

        if (_startDelayDuration < startDelay)
        {
            _startDelayDuration += Time.deltaTime;
            return;
        }
        var newPos = _rt.anchoredPosition;
        var isFast = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Mouse0);
        newPos.y += (isFast ? scrollSpeedFast : scrollSpeed) * Time.deltaTime;
        _rt.anchoredPosition = newPos;
        if (newPos.y > toY)
        {
            _didEnd = true;
            StartCoroutine(EndRoutine());
        }
    }

    private IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(endDelay);
        endCallback?.Invoke();
    }
}
