using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREnemySkill_SwordSwipe : EREnemySkill
{
    public bool sheathSword = true;
    public Transform pivotTowardsTarget;
    public bool useFlatRotForPivot = false;
    public Transform swordTransform;
    
    public Transform channelStartTransform;
    public Transform channelEndTransform;
    public Transform swipeEndTransform;

    public Collider swipeCollider;
    public float swipeDuration = 0.5f;

    private Coroutine _currentRoutine;

    protected override void OnChannelStart()
    {
        base.OnChannelStart();
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            if (pivotTowardsTarget != null)
            {
                if (useFlatRotForPivot)
                    pivotTowardsTarget.rotation = Quaternion.Euler(0, Quaternion.LookRotation(_self.target.transform.position - transform.position).eulerAngles.y, 0);
                else
                    pivotTowardsTarget.rotation = Quaternion.LookRotation(_self.target.transform.position - transform.position);
            }
                
            if (sheathSword) StartCoroutine(ShowSwordRoutine());
            AnimateSword(channelStartTransform, channelEndTransform, channelTime);
            yield break;
        }
    }

    protected override void OnChannelCancel()
    {
        base.OnChannelCancel();
        StopAnimateSword();
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

    private void AnimateSword(Transform from, Transform to, float duration)
    {
        if (_currentRoutine != null) StopCoroutine(_currentRoutine);
        _currentRoutine = StartCoroutine(AnimateSwordRoutine(from, to, duration));
    }
    
    private IEnumerator AnimateSwordRoutine(Transform from, Transform to, float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            swordTransform.position = Vector3.Lerp(from.position, to.position, t / duration);
            swordTransform.rotation = Quaternion.Slerp(from.rotation, to.rotation, t / duration);
            yield return null;
        }
    }
}
