using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurFade : MonoBehaviour
{
    public bool fadeOn = false;
    public float blurAlpha;

    private PostProcessVolume postProcessVolume;
    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = transform.GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        var dof = postProcessVolume.profile.GetSetting<DepthOfField>();

        if (dof.focalLength.value >= 64)
        {
            dof.focalLength.value = 64;
            return;
        }
        dof.focalLength.value += Time.unscaledDeltaTime;

    }
}
