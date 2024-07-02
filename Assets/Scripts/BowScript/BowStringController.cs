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

    private Transform interactor;
    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private  void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;

        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
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
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
        }
    }

    private void HandleStringPulledBackToLimit(float midPointLocalZabs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZabs >= bowStringStrengthLimit)
        {
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStrengthLimit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z > 0)
        {
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}