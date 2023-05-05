using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] int sensitivityHor = 3000;
    [SerializeField] int sensitivityVer = 3000;

    [Range(-90, 0)]
    [SerializeField] int lockVerMin = -70;
    [Range(0, 90)]
    [SerializeField] int lockVerMax = 90;

    [SerializeField] bool invertY = false;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityVer;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityHor;

        //Get vertical rotation and clamp it
        xRotation += invertY ? mouseY : -mouseY;
        xRotation = Mathf.Clamp(xRotation, lockVerMin, lockVerMax);

        //Rotate the camera vertically on the x-axis
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Rotate the player horizontally
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
