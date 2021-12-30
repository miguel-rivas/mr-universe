using UnityEngine;

public class RotateAnimation: MonoBehaviour
{
    public float speed = 1f;
    public float x = 0;
    public float y = 1;
    public float z = 0;
    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime * speed);
    }
}
