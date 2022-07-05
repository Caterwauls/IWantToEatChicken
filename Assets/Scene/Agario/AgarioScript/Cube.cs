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
    public CubeSpawner cubeSpawner;
    public GameObject newNpcCube;
    public GameObject rangeObject;
    public BoxCollider rangeCollider;




    private Rigidbody _rb;

    private void Awake()
    {
        acceleration = 20f;
        _rb = GetComponent<Rigidbody>();
        

    }


    private void Start()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        rangeObject = cubeSpawner.gameObject;
        rangeCollider = cubeSpawner.GetComponent<BoxCollider>();
        UpdateScale();
        CheckCubeLeaveMap();
        gameObject.name = cubeName;

    }

    private void Update()
    {
        var cubesCanPull = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f + magnetRadius);
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

            if (otherCube.energy <= 0 && otherCube.GetComponent<PlayerMove>() == null)
            {
                string newName = otherCube.cubeName;
                Destroy(other.gameObject);
                if (GetComponent<PlayerMove>() == null)
                {
                    var newSpawnPos = Return_RandomPosition();
                    var viewportX = Camera.main.WorldToViewportPoint(newSpawnPos).x;
                    var viewportY = Camera.main.WorldToViewportPoint(newSpawnPos).y;
                    if ( !(viewportX >=0 && viewportX <= 1) && !(viewportY  <= 1 && viewportY >= 0))
                    {
                        var a = Instantiate(newNpcCube);
                        a.transform.position = newSpawnPos;
                        a.GetComponent<Cube>().energy = Random.RandomRange(1, 3);
                        a.GetComponent<Cube>().cubeName = newName;
                    }
                }
                
            }
            else if (otherCube.energy <= 0 && otherCube.GetComponent<PlayerMove>() != null)
            {
                otherCube.gameObject.SetActive(false);
            }
            else
            {
                otherCube.UpdateScale();
                var a = Instantiate(absorbEffect);
                a.GetComponent<ParticleSystem>().startColor = other.transform.GetComponentInParent<MeshRenderer>().material.color;
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
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
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
