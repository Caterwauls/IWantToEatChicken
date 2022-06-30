using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBAbilirtOrbAbsorb : MonoBehaviour
{
    public GameObject prefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<BBPlayerManager>().teleportAbilityOn = true;
            var abilityOrbEffect = Instantiate(prefab);

            Vector3 bottomPos = transform.position + Vector3.down * transform.lossyScale.y;
            abilityOrbEffect.transform.position = bottomPos;

            Destroy(this.gameObject);
        }
    }
}
