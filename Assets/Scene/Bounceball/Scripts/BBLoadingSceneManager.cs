using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBLoadingSceneManager : MonoBehaviour
{
    public GameObject player;
    public float waitTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(waitTime);
        player.SetActive(false);
        //이 사이에 효과음 넣기
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BBallScene0");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
