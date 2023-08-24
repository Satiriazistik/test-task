using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
    }
}
