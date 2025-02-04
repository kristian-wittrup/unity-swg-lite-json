using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{
    public float zoomSpeed = 100f;
    public float minFOV = 15f;
    public float maxFOV = 120f;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            Debug.Log("Mouse ScrollWheel input: " + scrollInput);
            float fov = Camera.main.fieldOfView;
            Debug.Log("Current FOV before change: " + fov);
            fov -= scrollInput * zoomSpeed;
            fov = Mathf.Clamp(fov, minFOV, maxFOV);
            Camera.main.fieldOfView = fov;
            Debug.Log("Updated FOV: " + fov);
        }
    }
}