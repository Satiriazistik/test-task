using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorTarget : MonoBehaviour, IDetectorTarget
{
    public Action OnFullDetected;

    [Header("Cached Links")]
    public Image DetectionProgressBar;

    [Header("Enemy Detection Settings")]
    public float DetectDuration = 3f;

    public bool FullDetected { get; set; }

    private float _currentDetectAmount;

    void Start()
    {
        DetectionProgressBar.fillAmount = 0f;
    }

    public void DetectingProcess()
    {
        if (FullDetected)
            return;

        _currentDetectAmount += Time.deltaTime;

        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        DetectionProgressBar.fillAmount = _currentDetectAmount / DetectDuration;

        if (DetectionProgressBar.fillAmount == 1)
        {
            OnFullDetected?.Invoke();
            FullDetected = true;
        }
    }
}
