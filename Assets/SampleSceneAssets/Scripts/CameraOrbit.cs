using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]


public class CameraOrbit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 2f;
    public float distanceMax = 55f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    bool orbitable;


    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


  
    void LateUpdate()
    {

        // controls
        if (Input.GetMouseButtonDown(0)) //|| Input.GetMouseButtonDown(1) ) //only left click 
            orbitable = true; 
        else if (Input.GetMouseButtonUp(0)) // || Input.GetMouseButtonUp(1)  )//only left click 
            orbitable = false;



         // orbit around target 
        if (orbitable || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (target)
            {
                Debug.Log("orbiting");
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {

                    distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 15, distanceMin, distanceMax);
                   // Debug.Log("Mouse ScrollWheel: " + Input.GetAxis("Mouse ScrollWheel"));
                }


                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position;

                transform.rotation = rotation;
                transform.position = position;
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

