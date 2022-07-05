using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBDoubleJumpOrb : MonoBehaviour
{
    public GameObject doubleJumpEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<BBPlayer>().ability.doubleJumpOn = true;
            var abilityOrbEffect = Instantiate(doubleJumpEffect);

            Vector3 bottomPos = transform.position + Vector3.down * transform.lossyScale.y;
            abilityOrbEffect.transform.position = bottomPos;

            Destroy(this.gameObject);
        }
    }
}
