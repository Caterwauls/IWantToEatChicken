using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Cube : MonoBehaviour
{

    public float energy;
    public float cubeSpeed = 8.5f;
    public float acceleration;
    public string cubeName;
    public AudioSource absorbSound;
    public GameObject absorbEffect;
    public float magnetRadius = 1;
    public float magnetPullSpeed = 1f;




    private Rigidbody _rb;

    private void Awake()
    {
        acceleration = 20f;
        _rb = GetComponent<Rigidbody>();
        

    }


    private void Start()
    {

        UpdateScale();
        CheckCubeLeaveMap();
        gameObject.name = cubeName;

    }

    private void Update()
    {
        var cubesCanPull = Physics.OverlapSphere(transform.position, magnetRadius);
        foreach(Collider cube in cubesCanPull)
        {
            if (cube.GetComponent<Cube>() != null && (CanEat(cube.GetComponent<Cube>())))
            {
                cube.transform.position = Vector3.MoveTowards(cube.transform.position, transform.position, magnetPullSpeed * Time.deltaTime);
            }
            
        }
    }

    private void UpdateScale()
    {
        transform.localScale = Vector3.one * Mathf.Pow(energy, 1 / 3f);

    }

    public void AbsorbPowerFrom(Cube otherCube)
    {
        float absorbSpeed = energy / 10 + 3;

        otherCube.energy -= absorbSpeed * Time.fixedDeltaTime;
        energy += absorbSpeed * Time.fixedDeltaTime;


    }


    public bool CanEat(Cube other)
    {
        return other.energy < 0.8f * energy;
    }

    protected void OnTriggerStay(Collider other)
    {
        // other가 CubeCollider가 아닐 경우 무시
        if (other.tag != "CubeCollider") return;

        // 만약 자기 자신의 cubeCollider일 경우 무시
        if (other.transform.parent == transform) return;
        if (CanEat(other.GetComponentInParent<Cube>()))
        {
            Cube otherCube = other.GetComponentInParent<Cube>();
            AbsorbPowerFrom(otherCube);


            UpdateScale();

            if (otherCube.energy <= 0 && !otherCube.GetComponent<PlayerMove>())
            {
                Destroy(otherCube.gameObject);
            }
            else if (otherCube.energy <= 0 && otherCube.GetComponent<PlayerMove>())
            {
                otherCube.gameObject.SetActive(false);
            }
            else
            {
                otherCube.UpdateScale();
                var a = Instantiate(absorbEffect);
                a.transform.position = transform.position;
                if (!absorbSound.isPlaying)
                {
                    absorbSound.Play();
                }
                
                absorbSound.pitch = 1 / (0.5f + energy);
                absorbSound.volume = 0.1f;
                if (otherCube.GetComponent<PlayerMove>()||GetComponent<PlayerMove>())
                {
                    absorbSound.volume = 1f;
                }
            }

        }
    }

    public void MoveMyVelocity(Vector3 targetVelocity)
    {

        Vector3 newVelocity = Vector3.MoveTowards(_rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
    }

    //public void CubeJump()
    //{
    //    _rb.AddForce(Vector3.up.normalized * 10f, ForceMode.Impulse);
    //}

    IEnumerator CheckCubeLeaveMap()
    {
        while (true)
        {
            if (transform.position.y < -10)
            {
                transform.position = new Vector3(0, 0, 0);
            }
            yield return new WaitForSeconds(1f);
        }


    }



}
