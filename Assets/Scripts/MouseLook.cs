using UnityEngine;

[AddComponentMenu("Camera-Control/Custom Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public bool invertY = false;

    float rotationY = 0F;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        float ySens = sensitivityY;
        if (invertY) { ySens *= -1f; }

        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + GetMouseX() * sensitivityX;

            rotationY += GetMouseY() * ySens;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, GetMouseX() * sensitivityX, 0);
        }
        else
        {
            rotationY += GetMouseY() * ySens;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    float GetMouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    float GetMouseY()
    {
        return Input.GetAxis("Mouse Y");
    }
}