using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 100f;
    public Vector3 axis = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis, spinSpeed * Time.deltaTime);
    }
}
