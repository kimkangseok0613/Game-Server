using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    Vector3 direction = Vector3.zero;

    private void Start()
    {
        DisableCamera();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Control()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            direction.x++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction.x--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction.z++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction.z--;
        }
    }
    private void Move()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(direction);
    }
    private void DisableCamera()
    {
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            Camera eyes = transform.GetComponentInChildren<Camera>();

            eyes.GetComponent<AudioListener>().gameObject.SetActive(false);

            eyes.gameObject.SetActive(false);
        }
    }
}
