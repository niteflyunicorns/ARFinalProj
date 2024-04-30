using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectMode : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycaster;
    GameObject placedPrefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    TrackingState EarthTrackingState;

    void OnEnable()
    {
        UIController.ShowUI("PlaceObject");
    }

    public void SetPlacedPrefab(GameObject prefab)
    {
        placedPrefab = prefab;
    }

    public void OnPlaceObject(InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();
        PlaceObject(touchPosition);
    }

    void PlaceObject(Vector2 touchPosition)
    {
        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            // logic here to anchor object
            // GeospatialPose Convert( Pose pose );
            placedPrefab.AddComponent<ARAnchor>();


            InteractionController.EnableMode("Main");
        }
    }

    // void AnchorObject( GameObject object )
    // {
    //     // 

    // }
}