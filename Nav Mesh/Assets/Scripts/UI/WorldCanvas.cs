using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : MonoBehaviour
{
    private Camera camera;

    void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position+camera.transform.forward);
    }
}
