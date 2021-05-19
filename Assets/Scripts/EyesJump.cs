using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesJump : MonoBehaviour
{
    [SerializeField] GameObject LoseText;
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject HitText;
    [SerializeField] GameObject ReplayButton;

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
        Screen.SetResolution(1080, 1920,true); //Force aspect ratio to 9:16
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanJump)
        {
            transform.SetParent(null);
            GetComponent<ParabolaController>().FollowParabola();
            // Stop eyes from rotating when jumping
            rb.constraints = RigidbodyConstraints.FreezeRotationZ & RigidbodyConstraints.FreezeRotationY & RigidbodyConstraints.FreezeRotationX; 
            LeftEye.CanRotate = false;
            RightEye.CanRotate = false;
            //transform.GetChild(0).GetComponent<EyeController>().StopRotating();
        }
    }

    int i = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("FallBlock"))
        {
            CanJump = true;
            rb.constraints = RigidbodyConstraints.None; 
            LeftEye.RotateDirection = collision.gameObject.GetComponent<BlockData>().LeftRotationDirection;
            RightEye.RotateDirection = collision.gameObject.GetComponent<BlockData>().RightRotationDirection;
            i++;
            LeftEye.RotationSpeed += 10;
            RightEye.RotationSpeed += 10;
            collision.gameObject.GetComponent<BlockFall>().WaitForFall = MaxTime;
            if(MaxTime > 0.5f) MaxTime -= 0.2f;
            collision.gameObject.GetComponent<BlockFall>().DominoFall();
            Vector3 NewJumpPos = collision.gameObject.GetComponent<BlockData>().JumpPosition.position;
            ChangeJumpPosition(NewJumpPos);
            transform.SetParent(collision.gameObject.transform);
        }
        if(collision.gameObject.CompareTag("Respawn"))
        {
            LoseText.SetActive(true);
            ReplayButton.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.gameObject.CompareTag("Final Block"))
        {
            CheckRotation(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (i == 1)
        {
            CheckRotation(collision.gameObject);
            i++;
        }
    }

    void CheckRotation(GameObject Block)
    {

        if (Block.layer != 8)
        {
            if (LeftEye.CheckLook() && RightEye.CheckLook())
            {
                if(Block.CompareTag("Final Block"))
                {
                    WinText.SetActive(true);
                    ReplayButton.SetActive(true);
                    Time.timeScale = 0;
                    return;
                }
                HitText.SetActive(true);
            }
            else
            {
                LoseText.SetActive(true);
                ReplayButton.SetActive(true);
                Time.timeScale = 0;
            }
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
        i = 0;
    }
}
