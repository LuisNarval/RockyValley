using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{

    private Vector3 middlePoint;
    private Camera thisCamera;

    public Vector3 offset;
    private Vector3 speed;
    public float time = 1f;
    public float maxZoom = 10;
    public float minZoom = 60;
    public float zoomLimitter = 40;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    void Start ()
    {
        middlePoint = Vector3.zero;
	}
	
	void Update ()
    {
        if (GameManager.instance.playersInScene.Count == 0)
            return;

        MoveCamera();
        ZoomCamera();
        
	}

    private void MoveCamera()
    {
        middlePoint = GetCenterPoint();
        middlePoint += offset;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, middlePoint, ref speed, time);
    }

    private void ZoomCamera()
    {
        Bounds boundPlayers = new Bounds(GameManager.instance.playersInScene[0].transform.position, Vector3.zero);
        for (int i = 0; i < GameManager.instance.playersInScene.Count; i++)
        {
            boundPlayers.Encapsulate(GameManager.instance.playersInScene[i].transform.position);
        }
        float zoom = Mathf.Lerp(maxZoom, minZoom,  (boundPlayers.size.x + boundPlayers.size.z) / 2 );
        thisCamera.fieldOfView = Mathf.Lerp(thisCamera.fieldOfView, zoom, Time.deltaTime);

    }

    private Vector3 GetCenterPoint()
    {
        if (GameManager.instance.playersInScene.Count == 1)
            return GameManager.instance.playersInScene[0].transform.position;
        else
        {
            Bounds boundPlayers = new Bounds(GameManager.instance.playersInScene[0].transform.position, Vector3.zero);
            for (int i = 0; i < GameManager.instance.playersInScene.Count; i++)
            {
                boundPlayers.Encapsulate(GameManager.instance.playersInScene[i].transform.position);
            }
            return boundPlayers.center;
        }

    }
}
