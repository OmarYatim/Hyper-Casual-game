using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesJump : MonoBehaviour
{
    [SerializeField] Transform JumpPosition;
    [SerializeField] float DistanceBetweenBlocks;
    [SerializeField] float MaxTime = 3f;
    [SerializeField] EyeRotation LeftEye, RightEye;
    Rigidbody rb;
    bool CanJump = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.sleepThreshold = 0.0f;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanJump)
        {
            transform.SetParent(null);
            GetComponent<ParabolaController>().FollowParabola();
            LeftEye.CanRotate = false;
            RightEye.CanRotate = false;
            //transform.GetChild(0).GetComponent<EyeController>().StopRotating();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("FallBlock"))
        {
            CanJump = true;
            //LeftEye.RotationAxe = LeftEye.transform.right;
            LeftEye.RotateDirection = collision.gameObject.GetComponent<BlockData>().LeftRotationDirection;
            //RightEye.RotationAxe = RightEye.transform.right;
            RightEye.RotateDirection = collision.gameObject.GetComponent<BlockData>().RightRotationDirection;
            CheckRotation(collision.gameObject);
            collision.gameObject.GetComponent<BlockFall>().WaitForFall = MaxTime;
            if(MaxTime > 0.5f) MaxTime -= 0.2f;
            //collision.gameObject.GetComponent<BlockFall>().DominoFall();
            Vector3 NewJumpPos = collision.gameObject.GetComponent<BlockData>().JumpPosition.position;
            ChangeJumpPosition(NewJumpPos);
            transform.SetParent(collision.gameObject.transform);
        }
    }

    void CheckRotation(GameObject Block)
    {
        if (Block.layer != 8)
        {
            if (LeftEye.CheckLook() && RightEye.CheckLook())
                Debug.Log("YOu Won");
            else Debug.Log("You Lose");
        }
        else
        {
            LeftEye.CanRotate = true;
            RightEye.CanRotate = true;
        }

    }

    void ChangeJumpPosition(Vector3 NewPosition)
    {
        JumpPosition.position = NewPosition;
    }
    

    private void OnCollisionExit(Collision collision)
    {
        CanJump = false;
    }
}
