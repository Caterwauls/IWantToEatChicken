using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBDeadLine : MonoBehaviour
{
    public GameObject prefab;

    private GameObject _dieEffect;
    private int _deadNum;

    private void Start()
    {
        _deadNum = PlayerPrefs.GetInt("deadNum");
    }
    private void Update()
    {
        waitDie();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _deadNum++;
            PlayerPrefs.SetInt("deadNum", _deadNum);
            _dieEffect = Instantiate(prefab);
            _dieEffect.transform.position = other.transform.position;
            other.gameObject.SetActive(false);
        }
    }

    void waitDie()
    {
        if (_dieEffect == null) return;
        if (!_dieEffect.GetComponent<ParticleSystem>().isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

    }
}
