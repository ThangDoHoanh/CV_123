using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Vector3 offset;
    private void Update()
    {
        Vector3 pos = player.position + offset;
        pos.y = Camera.main.transform.position.y;
        pos.z = Camera.main.transform.position.z;
        Camera.main.transform.position = pos;
    }
}
