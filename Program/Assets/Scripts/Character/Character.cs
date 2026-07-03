using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] Rotation rotation;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigidBody;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rotation = GetComponent<Rotation>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        DisableCamera();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
           Control();

           Animate();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Move();

            rotation.RotateY(rigidBody);
        }
    }

    void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();
    }

    void Animate()
    {
        animator.SetInteger("X", Mathf.Abs((int)direction.x));
        animator.SetInteger("Y", Mathf.Abs((int)direction.z));
    }

    void Move()
    {
        rigidBody.MovePosition(rigidBody.position + rigidBody.transform.TransformDirection(direction) * speed * Time.fixedDeltaTime);
    }

    private void DisableCamera()
    {
        if(photonView.IsMine)
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
