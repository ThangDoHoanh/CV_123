using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector2 offset;
    [SerializeField] float speed;

    // Update is called once per frame
    private void Update()
    {
        Vector3 pos = player.position + (Vector3)offset;
        pos.z = Camera.main.transform.position.z;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, speed * Time.deltaTime);
    }
}