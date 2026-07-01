using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField] Vector3 direction;
    [SerializeField] float Speed;
    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] Rotation rotation;
    [SerializeField] Animator animator;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        rotation = GetComponent<Rotation>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DisableCamera();
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            Control();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        { 
            Move();

            rotation.RotateY(Rigidbody);
        }
    }

    private void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        if (direction.x > 0 || direction.z>0)
        {
            animator.SetInteger("X", (int)direction.x);
            animator.SetInteger("Y", (int)direction.z);
        }
        direction.Normalize();        
    }

    private void Move()
    {
        Rigidbody.MovePosition(Rigidbody.position + Rigidbody.transform.TransformDirection(direction) * Speed * Time.fixedDeltaTime);
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