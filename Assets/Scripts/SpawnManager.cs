using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    ARRaycastManager manager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;

    GameObject spawnedObj;
    
    void Start()
    {
        spawnedObj = null;
    }

    void Update()
    {
        if ( Input.touchCount == 0 ) return;

        if ( manager.Raycast( Input.GetTouch( 0 ).position, hits ) ) {
            if ( Input.GetTouch( 0 ).phase == TouchPhase.Began ) {
                SpawnPrefab( hits[ 0 ].pose.position );
            }
            else if ( Input.GetTouch( 0 ).phase == TouchPhase.Moved && spawnedObj != null ) {
                spawnedObj.transform.position = hits[ 0 ].pose.position;
            }
            if ( Input.GetTouch( 0 ).phase == TouchPhase.Ended ) {
                spawnedObj = null;
            }
        }
    }

    private void SpawnPrefab( Vector3 spawnPos )
    {
        spawnedObj = Instantiate( spawnablePrefab, spawnPos, Quaternion.identity );
    }
}
