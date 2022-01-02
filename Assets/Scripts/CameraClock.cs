using UnityEngine;

public class CameraClock : MonoBehaviour {
    public float speed = 1f;
    private bool autoRotation = true;

    public float counterX = 0f;
    public float counterY = 30f;
    public float counterZ = 160f;

    public float originX = 0f;
    public float originY = 0f;
    public float originZ = 0f;

    public float targetX = 0f;
    public float targetY = 0f;
    public float targetZ = 0f;

    private void keyboardInputs() {
      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");

      bool isHorizontal = horizontal != 0f;
      bool isVertical = vertical != 0f;
      bool zoomIn = Input.GetKey(KeyCode.Z);
      bool zoomOut = Input.GetKey(KeyCode.X);
      bool activateAuto = Input.GetKey(KeyCode.C);

      if(activateAuto) {
          autoRotation = true;
      }

      if(isHorizontal || isVertical || zoomIn ||zoomOut){
        autoRotation = false;

        if(isHorizontal) {
            counterX += 0.5f * speed * -horizontal;
        }
        if(isVertical) {
            counterY += 0.5f * speed * vertical;
        }
        if(zoomIn) {
            counterZ -= 0.5f  * speed;
        }
        if(zoomOut) {
            counterZ += 0.5f * speed;
        }
      }
    }

    private void touchInputs() {
      Vector3 dragStartPos;
      Vector3 dragCurrentPos;
      Touch touch;
      bool DragStart;
      bool Dragging;
      bool DragRelease;
      // float dX;
      // float dY;

      if(Input.touchCount > 0) {
        touch = Input.GetTouch(0);

        DragStart = touch.phase == TouchPhase.Began;
        Dragging = touch.phase == TouchPhase.Moved;
        DragRelease = touch.phase == TouchPhase.Ended;

        if(DragStart) {
          dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
          dragStartPos.z = 0f;
          Debug.Log("start");
        }
        if(Dragging) {
          dragCurrentPos = Camera.main.ScreenToWorldPoint(touch.position);
          dragStartPos.z = 0f;
          Debug.Log("on");

          // dX = dragCurrentPos.x - dragStartPos.x;
          // dY = dragCurrentPos.y - dragStartPos.y;

          // counterX += dX;
          // counterY += dY;
        }
      }
    }

    private void rotateObject() {
      if(autoRotation){
          counterX += 0.1f * speed;
      }

      if(counterX >= 360f){
          counterX = 0;
      } else if(counterX <= 0f){
          counterX = 360f;
      }
      counterY = Mathf.Max(Mathf.Min(counterY, 70), 30);
      counterZ = Mathf.Max(Mathf.Min(counterZ, 160), 8);

      targetX = originX + Mathf.Cos(counterX * Mathf.Deg2Rad) * counterZ;
      targetZ = originZ + Mathf.Sin(counterX * -Mathf.Deg2Rad) * counterZ;
      targetY = originY + counterY;
      
      Vector3 origin = new Vector3(originX, originY, originZ);
      Vector3 target = new Vector3(targetX, targetY, targetZ);

      transform.position = target;
      transform.rotation = Quaternion.LookRotation(origin - target, Vector3.up);
    }

    private void Update() {
      keyboardInputs();
      touchInputs();
      rotateObject();
    }
}