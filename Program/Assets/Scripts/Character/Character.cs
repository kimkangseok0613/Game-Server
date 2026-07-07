using UnityEngine;
using Photon.Pun;
using UnityEngine.tvOS;

public class Character : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float speed;
    [SerializeField] float health = 100;
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

           Pause();
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

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MouseManager.Instance.SetMouse(true);

            PanelManager.Instance.Open(Panel.Pause);
        }
    }

    void Control()
    {
        rotation.MouseX = Input.GetAxisRaw("Mouse X");

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
        rigidBody.linearVelocity = rigidBody.transform.TransformDirection(direction).normalized * speed;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Robot"))
        {
            PhotonView View = other.GetComponent<PhotonView>();

            if (View != null)
            {
                Debug.Log("Robot Object does not have PhotonView");
            }
            if(View.IsMine || PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(View.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 내 오브젝트라면 다른 클라이언트에게 데이터를 전송합니다.
            stream.SendNext(health);
        }
        else
        {
            // 다른 클라이언트의 데이터를 받습니다.
            health = (float)stream.ReceiveNext();
        }
    }
}
