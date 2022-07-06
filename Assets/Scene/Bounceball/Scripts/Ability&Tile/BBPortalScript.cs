using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBPortalScript : MonoBehaviour
{
    public Effect portalEffect;

    private bool _didActivate;
    private void OnTriggerEnter(Collider other)
    {
        if (_didActivate) return;
        if (other.tag != "Player") return;
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            foreach (var ren in other.GetComponentsInChildren<Renderer>())
            {
                ren.enabled = false;
            }
            _didActivate = true;
            portalEffect.Play();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
