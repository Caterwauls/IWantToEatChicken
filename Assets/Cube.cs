using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Cube : MonoBehaviour
{

    public float energy;
    public float cubeSpeed = 8.5f;
    public float acceleration = 20f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        UpdateScale();
    }

    private void UpdateScale()
    {
        transform.localScale = Vector3.one * Mathf.Pow(energy, 1 / 3f);
    }

    public void AbsorbPowerFrom(Cube otherCube)
    {
        float absorbSpeed = energy / 10 + 10;

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

            if (otherCube.energy <= 0)
            {
                Destroy(otherCube.gameObject);
            }
            else
            {           
                otherCube.UpdateScale();
            }

        }
    }
    
    public void MoveMyVelocity(Vector3 targetVelocity)
    {
        Vector3 newVelocity = Vector3.MoveTowards(_rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
    }
}
