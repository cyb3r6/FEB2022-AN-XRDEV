using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public GameObject prefab;

    private GameObject spawnedPrefab;
    private ARRaycastManager arRaycastmanager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    
    void Start()
    {
        arRaycastmanager = GetComponent<ARRaycastManager>();
    }

    
    void Update()
    {
        if(spawnedPrefab == null)
        {
            // detect a touch on the screen
            if(Input.touchCount > 0)
            {
                // save the position of the touch on the screen
                Vector2 touchPosition = Input.GetTouch(0).position;

                // raycast from the touch position
                if(arRaycastmanager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;
                    spawnedPrefab = Instantiate(prefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
