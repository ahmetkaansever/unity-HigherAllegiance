using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float destroyDelay = 3.5f;
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
