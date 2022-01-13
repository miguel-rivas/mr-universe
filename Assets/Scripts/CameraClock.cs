using UnityEngine;
using UnityEngine.SceneManagement;

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
        bool escape = Input.GetKey(KeyCode.Escape);

        if (escape)
        {
            SceneManager.LoadScene("Main");
        }

        if (activateAuto) {
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
      Touch t1, t2;
      float incX, incY, incZ, prevMagn, currMagn;
      Vector2 dt1, dt2;

      if (Input.touchCount > 0) {
        t1 = Input.GetTouch(0);

            if (Input.touchCount > 1)
            {
                t2 = Input.GetTouch(1);
                dt1 = t1.position - t1.deltaPosition;
                dt2 = t2.position - t2.deltaPosition;
                prevMagn = (dt1 - dt2).magnitude;
                currMagn = (t1.position - t2.position).magnitude;
                incZ = (currMagn - prevMagn) * -0.1f;
                counterZ += incZ;
            }
            else
            {
                incX = ((t1.position.x / Screen.width) - 0.5f);
                incY = ((t1.position.y / Screen.height) - 0.5f);

                if (incX > 0.45f && incY < -0.4f) {
                    SceneManager.LoadScene("Main");
                }

                counterX += incX;
                counterY += incY;
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