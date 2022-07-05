using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREnemySkill_SwordSwipe : EREnemySkill
{
    public bool sheathSword = true;
    public Transform pivotTowardsTarget;
    public bool useFlatRotForPivot = false;
    public float rotFollowWhileChannelSpeed = 0;
    public Transform swordTransform;
    
    public Transform channelStartTransform;
    public Transform channelEndTransform;
    public Transform swipeEndTransform;

    public Collider swipeCollider;
    public float swipeDuration = 0.5f;

    private Coroutine _currentRoutine;

    private Quaternion GetDesiredPivotRotation()
    {
        if (useFlatRotForPivot)
            return Quaternion.Euler(0, Quaternion.LookRotation(_self.target.transform.position - transform.position).eulerAngles.y, 0);
        
        return Quaternion.LookRotation(_self.target.transform.position - transform.position);
    }
    
    protected override void OnChannelStart()
    {
        base.OnChannelStart();
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            if (pivotTowardsTarget != null)
            {
                pivotTowardsTarget.rotation = GetDesiredPivotRotation();
            }
                
            if (sheathSword) StartCoroutine(ShowSwordRoutine());
            AnimateSword(channelStartTransform, channelEndTransform, channelTime, Mathf.Sqrt);
            if (rotFollowWhileChannelSpeed < 0.01f) yield break;
            for (float t = 0; t < channelTime; t += Time.deltaTime)
            {
                yield return null;
                pivotTowardsTarget.rotation = Quaternion.RotateTowards(pivotTowardsTarget.rotation,
                    GetDesiredPivotRotation(), rotFollowWhileChannelSpeed * Time.deltaTime);
            }
        }
    }

    protected override void OnChannelCancel()
    {
        base.OnChannelCancel();
        StopAnimateSword();
        swipeCollider.enabled = false;
        if (sheathSword) StartCoroutine(HideSwordRoutine());
    }

    protected override void OnChannelComplete()
    {
        base.OnChannelComplete();
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            AnimateSword(channelEndTransform, swipeEndTransform, swipeDuration);
            swipeCollider.enabled = true;
            yield return new WaitForSeconds(swipeDuration);
            swipeCollider.enabled = false;
            yield return new WaitForSeconds(postDelay - swipeDuration);
            if (sheathSword) StartCoroutine(HideSwordRoutine());
        }
    }

    private IEnumerator ShowSwordRoutine()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 4f)
        {
            swordTransform.localScale = Vector3.one * t;
            yield return null;
        }

        swordTransform.localScale = Vector3.one;
    }

    private IEnumerator HideSwordRoutine()
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 4f)
        {
            swordTransform.localScale = Vector3.one * (1 - t);
            yield return null;
        }

        swordTransform.localScale = Vector3.zero;
    }

    private void StopAnimateSword()
    {
        StopCoroutine(_currentRoutine);
    }

    private void AnimateSword(Transform from, Transform to, float duration, System.Func<float, float> valueFunc = null)
    {
        if (_currentRoutine != null) StopCoroutine(_currentRoutine);
        _currentRoutine = StartCoroutine(AnimateSwordRoutine(from, to, duration, valueFunc));
    }
    
    private IEnumerator AnimateSwordRoutine(Transform from, Transform to, float duration, System.Func<float, float> valueFunc = null)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            var val = valueFunc == null ? t / duration : valueFunc(t / duration);
            swordTransform.position = Vector3.Lerp(from.position, to.position, val);
            swordTransform.rotation = Quaternion.Slerp(from.rotation, to.rotation, val);
            yield return null;
        }
    }
}
