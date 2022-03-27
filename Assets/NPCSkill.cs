using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkill : MonoBehaviour
{
    public Cube enemy;
    public float BanishCoolTime;
    public float banishPower;
    public ParticleSystem banishParticle;

    private bool _canUseBanish = true;

    private void Start()
    {
        enemy = null;
    }


    // 쿨도 돌았고, 충분히 가까운가?
    public bool CanBanish(Cube other)
    {
        float distance = Vector3.Distance(transform.position, other.transform.position);
        return _canUseBanish && distance <= 3.5f * transform.localScale.x;
    }

    private IEnumerator CoroutineCooldownTimer()
    {
        _canUseBanish = false;
        yield return new WaitForSeconds(BanishCoolTime);
        _canUseBanish = true;
    }

    public void Banish(Cube enemy)
    {
        StartCoroutine(CoroutineCooldownTimer());
        Vector3 dir = enemy.transform.position - transform.position;

        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();

        Vector3 newVelocity = dir * banishPower;
        newVelocity.y = 10f;
        enemyRb.velocity = newVelocity;
        
        ParticleSystem instance = Instantiate(banishParticle, transform.position, transform.rotation);
        AudioSource explosionAudio = instance.GetComponent<AudioSource>();
        explosionAudio.Play();

        Destroy(instance.gameObject, instance.main.duration);
    }

}
