using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EREnemySkill_TriCubeBlock : EREnemySkill
{
    public Transform pivotTransform;
    public BoxCollider shieldCollider;
    public float shieldDurationMin = 2f;
    public float shieldDurationMax = 5f;
    public float shieldTurnSpeed = 180f;

    protected override void Awake()
    {
        base.Awake();
        GetComponentInChildren<ERShieldCollider>().onBlock += (amount) =>
        {
            _self.ApplyDamage(amount / 6f);
        };
    }

    protected override void OnChannelComplete()
    {
        base.OnChannelComplete();
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            var duration = Random.Range(shieldDurationMin, shieldDurationMax);
            _self.StartChannel(duration + 1.0f);
            var rot = Quaternion.Euler(0, Quaternion.LookRotation(_self.target.transform.position - transform.position).eulerAngles.y, 0);

            pivotTransform.rotation = rot;
            for (float t = 0; t < 1; t += Time.deltaTime * 4f)
            {
                pivotTransform.localScale = Vector3.one * t;
                yield return null;
            }

            shieldCollider.enabled = true;


            var startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                if (_self.isStunned) break;

                if (_self.target != null)
                {
                    var desiredRot = Quaternion.Euler(0, Quaternion.LookRotation(_self.target.transform.position - transform.position).eulerAngles.y, 0);

                    pivotTransform.rotation = Quaternion.RotateTowards(pivotTransform.rotation, desiredRot,
                        shieldTurnSpeed * Time.deltaTime);
                }
                yield return null;
            }

            shieldCollider.enabled = false;
            for (float t = 0; t < 1; t += Time.deltaTime * 4f)
            {
                pivotTransform.localScale = Vector3.one * (1f - t);
                yield return null;
            }
        }
    }
}
