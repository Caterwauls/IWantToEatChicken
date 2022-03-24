using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int size = 5;
    public ParticleSystem explosionParticle;
    private float hp;
    private float power;






    public void TakeDamage(float damage)
    {
        hp = size * 2f;
        power = size;


        hp -= damage;

        if (hp <= 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);
            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();

            Destroy(instance.gameObject, instance.duration);
            gameObject.SetActive(false);
        }
    }
}
