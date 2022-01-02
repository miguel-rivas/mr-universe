using UnityEngine;

public class ClockRotation : MonoBehaviour {
    public float speed = 1f;

    public float counterX = 0f;
    public float counterY = 30f;
    public float counterZ = 160f;

    public float originX = 0f;
    public float originY = 0f;
    public float originZ = 0f;

    public float targetX = 0f;
    public float targetY = 0f;
    public float targetZ = 0f;

    private void Update() {
        counterX += 0.1f * speed;

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
}