using UnityEngine;

public class CameraFollow: MonoBehaviour {
    public Transform target;
    public Vector3 offsetLocation;

    void LateUpdate() {
      transform.position = target.position + offsetLocation;
      transform.LookAt(target);
    }
}
