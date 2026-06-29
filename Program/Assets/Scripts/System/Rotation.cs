using Unity.Hierarchy;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float axis;
    [SerializeField] float speed;

    [SerializeField] float mouseX;
    [SerializeField] float mouseY;

    [SerializeField] Head Head;
    
    private void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X");
        mouseY += Input.GetAxisRaw("Mouse Y");
    }
    public void RotateX(float minAnle, float maxAngle)
    {
        axis = Mathf.Clamp(axis, minAnle, maxAngle);

        transform.localEulerAngles = new Vector3(axis, 0, 0);
    }
    public void RotateY(Rigidbody Rigidbody)
    {
        axis += mouseX * speed * Time.fixedDeltaTime;

        Rigidbody.transform.eulerAngles = new Vector3(0, axis, 0);
    }
}