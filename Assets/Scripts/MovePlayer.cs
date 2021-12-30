using UnityEngine;

public class MovePlayer : MonoBehaviour {
    public float rotationSpeed = 0.8f;
    public float currentRotationX = 0;
    public float currentRotationY = 0;
    private bool notRotating = true;

    private void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool notHorizontal = horizontal != 0f;
        bool notVertical = vertical != 0f;

        if(notHorizontal) {
            currentRotationX += horizontal * rotationSpeed;
        }
        if(notVertical) {
            currentRotationY += vertical * rotationSpeed;
            currentRotationY = Mathf.Max(Mathf.Min(currentRotationY, 30), 1);
        }
        if(notHorizontal || notVertical){
            notRotating = false;
            transform.rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
        }
        if(notRotating){
            currentRotationX += 0.05f;
            transform.rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
        }

    }
}
