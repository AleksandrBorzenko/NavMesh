using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotates the canvas of a bot to camera in order to permanently loos at it
/// </summary>
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
