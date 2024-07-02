using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BowStringController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;

    private XRGrabInteractable interactable;

    [SerializeField]
    private Transform midPointGrabObject, midPointVisualObject, midPointParentObject;

    [SerializeField]
    private float bowStringStrengthLimit = 0.3f;

    private float strength;
    private Transform interactor;

    public UnityEvent OnBowPulled;
    public UnityEvent<float> OnBowReleased;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        OnBowReleased?.Invoke(strength);
        strength = 0;

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;

        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
        OnBowPulled?.Invoke();
    }

    private void Update()
    {
        if (interactor != null)
        {
            Vector3 midPointLocalSpace = midPointParentObject.InverseTransformPoint(midPointGrabObject.position);
            float midPointLocalZabs = Mathf.Abs(midPointLocalSpace.z);

            HandleStringPushedBackToStart(midPointLocalSpace);
            HandleStringPulledBackToLimit(midPointLocalZabs, midPointLocalSpace);
            HandlePullingString(midPointLocalZabs, midPointLocalSpace);

            bowStringRenderer.CreateString(midPointVisualObject.position);
        }
    }

    private void HandlePullingString(float midPointLocalZabs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZabs < bowStringStrengthLimit)
        {
            strength = Remap(midPointLocalZabs, 0, bowStringStrengthLimit, 0, 1);
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
        }
    }

    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    private void HandleStringPulledBackToLimit(float midPointLocalZabs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZabs >= bowStringStrengthLimit)
        {
            strength = 1;
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStrengthLimit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z > 0)
        {
            strength = 0;
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}
