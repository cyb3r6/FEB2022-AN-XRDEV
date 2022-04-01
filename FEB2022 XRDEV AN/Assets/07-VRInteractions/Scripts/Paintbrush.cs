using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Paintbrush : XRGrabInteractable
{
    public GameObject paintPrefab;
    public Transform paintbrushTip;

    private Color color = Color.white;
    private bool triggerDown;
    private GameObject spawnedPaint;
    
    void Start()
    {
        activated.AddListener(TriggerPulled);
        deactivated.AddListener(TriggerReleased);
    }

    private void TriggerReleased(DeactivateEventArgs arg0)
    {
        spawnedPaint.transform.position = paintbrushTip.position;
        spawnedPaint = null;
        triggerDown = false;
    }

    private void TriggerPulled(ActivateEventArgs arg0)
    {
        // creating the paint instance in the scene
        spawnedPaint = Instantiate(paintPrefab, paintbrushTip.position, paintbrushTip.rotation);

        // assign the spawned paint material color
        Material trailMaterial = spawnedPaint.GetComponent<TrailRenderer>().material;
        trailMaterial.color = color;

        // triggerDown is set to true
        triggerDown = true;
    }

    void Update()
    {
        if (triggerDown)
        {
            if(spawnedPaint != null)
            {
                spawnedPaint.transform.position = paintbrushTip.position;
            }
        }
    }

    public void ChangePaint(Image image)
    {
        color = image.color;
    }
}
