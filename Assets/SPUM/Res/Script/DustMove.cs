using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMove : MonoBehaviour
{
    private ParticleSystem _particle;
    [SerializeField] private float moveSpeed = 0.2f;
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _particle.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime*0.2f;
    }
    public void SetMoveSpeed(float _speed)
    {
        moveSpeed = _speed;
    }
}
