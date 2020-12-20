using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour {
	//we will fill this list with the planes ARCore detected in the current frame
	private List<TrackedPlane> m_NewTrackedPlanes = new List<TrackedPlane>();

	public GameObject GridPrefab;

	public GameObject Portal;
	public GameObject ARCamera;
	//use this for initialization
	void Start() {

    }

	// update is called once per frame
	void Update ()
    {
		//check ARCore session status
		if(Session.Status != SessionStatus.Tracking)
        {
			return;
        }
		//the following function will fill m_NewTrackedPlanes with the planes thaat ARCore detected in current frame
		Session.GetTrackables(m_NewTrackedPlanes, TrackableQueryFilter.New);
		//instantiate a Grid for each TrackedPlanes in m_NewTrackedPlanes
		for(int i = 0; i < m_NewTrackedPlanes.Count; ++i) {
			GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);
			//this function will set the position of grid and modify the vertices of the attached mesh
			grid.GetComponent<GridVisualiser>().Initialize(m_NewTrackedPlanes[i]);		
		}
		//check if the used touches the screen
		Touch touch;
		if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
			return;
        }
		TrackableHit hit;
		if(Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
			Portal.SetActive(true);

			Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
			Portal.transform.position = hit.Pose.position;
			Portal.transform.rotation = hit.Pose.rotation;
			Vector3 cameraPosition = ARCamera.transform.position;
			cameraPosition.y = hit.Pose.position.y;
			Portal.transform.LookAt(cameraPosition, Portal.transform.up);
			Portal.transform.parent = anchor.transform;
        }

    }
}
