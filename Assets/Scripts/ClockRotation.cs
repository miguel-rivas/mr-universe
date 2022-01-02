using UnityEngine;

public class ClockRotation : MonoBehaviour {
    public float speed = 0.01f;
    private bool autoRotation = true;

    public float counterX = 0f;
    public float counterY = 4f;
    public float counterZ = 160f;

    public float point0x = 0f;
    public float point0y = 0f;
    public float point0z = 0f;

    public float point1x = 0f;
    public float point1y = 0f;
    public float point1z = 0f;

    public float rotX = 0f;
    public float rotY = 0f;
    public float rotZ = 0f;

    private float ang = Mathf.PI / 180;
    private float maxAngles = 360;

    private void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isHorizontal = horizontal != 0f;
        bool isVertical = vertical != 0f;
        bool zoomIn = Input.GetKey(KeyCode.Z);
        bool zoomOut = Input.GetKey(KeyCode.X);

        if(isHorizontal) {
            counterX += 0.1f * -horizontal;
            autoRotation = false;
        }
        if(isVertical) {
            counterY += 0.5f * vertical;
            autoRotation = false;
        }
        if(zoomIn) {
            counterZ -= 0.5f;
            autoRotation = false;
        }
        if(zoomOut) {
            counterZ += 0.5f;
            autoRotation = false;
        }
        if(autoRotation){
            counterX += 0.05f;
        }

        if(counterX >= 100f){
            counterX = 0;
        } else if(counterX <= 0f){
            counterX = 100f;
        }
        counterY = Mathf.Max(Mathf.Min(counterY, 70), 30);
        counterZ = Mathf.Max(Mathf.Min(counterZ, 160), 8);

        point1x = Mathf.Cos(maxAngles * counterX * ang * speed) * counterZ;
        point1z = Mathf.Sin(maxAngles * counterX * -ang * speed) * counterZ;
        point1y = point0y + counterY;
        
        Vector3 point0 = new Vector3(point0x, point0y, point0z);
        Vector3 point1 = new Vector3(point1x, point1y, point1z);

        transform.position = point1;
        transform.rotation = Quaternion.LookRotation(point0 - point1, Vector3.up);

        rotX = transform.rotation.x;
        rotY = transform.rotation.y;
        rotX = transform.rotation.z;
    }
}