using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Range(100, 1000)] [SerializeField] int sensitivityHor = 300;
    [Range(100, 1000)] [SerializeField] int sensitivityVer = 300;

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

    public void AddRecoil(float recoilAmount)
    {
        xRotation -= recoilAmount;
    }

    public int GetSensitivityHor()
    {
        return sensitivityHor;
    }

    public int SetSensitivityHor(int amt)
    {
        sensitivityHor = amt;
        return sensitivityHor;
    }

    public int GetSensitivityVer()
    {
        return sensitivityVer;
    }

    public int SetSensitivityVer(int amt)
    {
        sensitivityVer = amt;
        return sensitivityVer;
    }
}
