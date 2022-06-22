using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerMovementController : MonoBehaviour, IPunObservable
{
    public Rigidbody rb;
    private Joystick fj;

    public InputAction playerControls;

    float moveSpeed = 5f;

    private Vector3 randomPosition;
    private Vector3 networkPosition;

    private PhotonView view;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        fj = Joystick.FindObjectOfType<Joystick>();

        view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE)
        if (view.IsMine)
        {
            Vector2 v = playerControls.ReadValue<Vector2>();
            Vector3 moveDir = new Vector3(v.x, 0, v.y);
            moveDir.Normalize();

            //rb.velocity = new Vector3(moveDir.x * moveSpeed, 0, moveDir.z * moveSpeed);

            transform.Translate(moveDir * 5 * Time.deltaTime, Space.World);

            if (moveDir != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 500 * Time.deltaTime);
            }
        }

        if (!view.IsMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
        }
        
#endif
#if UNITY_ANDROID
        if (view.IsMine)
        {
            //rb.velocity = new Vector3(fj.Horizontal * moveSpeed, rb.velocity.y, fj.Vertical * moveSpeed);
            Vector3 moveDirAndroid = new Vector3(fj.Horizontal * moveSpeed, 0, fj.Vertical * moveSpeed);
            transform.Translate(moveDirAndroid * Time.deltaTime, Space.World);

            if (moveDirAndroid != Vector3.zero)
            {
                Quaternion Arotation = Quaternion.LookRotation(moveDirAndroid, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Arotation, 500 * Time.deltaTime);
            }
        }

        if (!view.IsMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
        }
#endif
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
        }

        else if (stream.IsReading)
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            rb.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += rb.velocity * lag;
        }
    }
}
