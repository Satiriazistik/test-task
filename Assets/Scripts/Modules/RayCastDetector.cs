using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDetector : MonoBehaviour
{
    [Header("Detector Settings")]
    public Transform DetectorTransform;

    public LayerMask DetectionLayer;

    public float DetectionDistance = 10f;

    private void Update()
    {
        if (Physics.Raycast(DetectorTransform.position, DetectorTransform.forward, out var hit, DetectionDistance, DetectionLayer))
        {
            var target = hit.collider.GetComponentInParent<IDetectorTarget>();
            if (target != null)
                target.Detected();
        }
    }

}
