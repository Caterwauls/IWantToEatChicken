using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBDeadLine : MonoBehaviour
{
    public GameObject prefab;

    private GameObject dieEffect;

    private void Update()
    {
        waitDie();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dieEffect = Instantiate(prefab);
            dieEffect.transform.position = other.transform.position;
            other.gameObject.SetActive(false);
        }
    }

    void waitDie()
    {
        if (dieEffect == null) return;
        if (!dieEffect.GetComponent<ParticleSystem>().isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

    }
}
