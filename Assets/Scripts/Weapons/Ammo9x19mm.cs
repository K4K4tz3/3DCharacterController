using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo9x19mm : MonoBehaviour
{
    public GameObject _counter;
    private GameObject _creator; //the one who shoot it
    private Rigidbody _rb;

    private float _speed = 200;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _creator = transform.parent.gameObject;
        _counter = GameObject.FindGameObjectWithTag("CounterP");
        if (_counter != null) transform.parent = _counter.transform;

        _rb.velocity = transform.forward * _speed;
        StartCoroutine(LifeTime());
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject != _creator) Destroy(gameObject);
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
