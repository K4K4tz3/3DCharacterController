using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    PlayerStateMachine _parent;

    public void Awake()
    {
        _parent = transform.parent.GetComponent<PlayerStateMachine>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") _parent.GetColliderMessage(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground") _parent.GetColliderMessage(false);
    }
}
