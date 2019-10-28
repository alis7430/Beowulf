using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCam : MonoBehaviour
{
    private Transform Cam;
    public Transform CameraGoal;
    public float Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cam = this.transform;   
    }

    // Update is called once per frame
    void Update()
    {
        Cam.position = Vector3.Lerp(Cam.position, CameraGoal.position, Speed * Time.deltaTime);
        Cam.rotation = Quaternion.Slerp(Cam.rotation, CameraGoal.rotation, Speed * Time.deltaTime);
    }
}
