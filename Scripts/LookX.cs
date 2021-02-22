using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1.5f;

    // Update is called once per frame
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * sensitivity;
        transform.localEulerAngles = newRotation;
    }
}
