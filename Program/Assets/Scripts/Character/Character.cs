using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    Vector3 direction = Vector3.zero;
    public float moveSpeed = 5f;

    private void Start()
    {
        DisableCamera();
    }

    private void Update()
    {
        Control();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Control()
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }

        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }
    }

    private void Move()
    {
        if (!photonView.IsMine) return;

        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 movement = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
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
            if (eyes != null)
            {
                eyes.GetComponent<AudioListener>().enabled = false;
                eyes.gameObject.SetActive(false);
            }
        }
    }
}