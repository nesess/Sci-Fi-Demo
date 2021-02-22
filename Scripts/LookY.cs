using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1.5f;
    [SerializeField]
    private float maxRotation = 45f;
    private float verticalRotation;

    
    
    
    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation + _mouseY * sensitivity, -maxRotation, maxRotation);
        transform.localEulerAngles = Vector3.left * verticalRotation;
    }
}
