using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    [SerializeField] GameObject HitText;
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject LoseText;
    [SerializeField] Transform LeftEye;
    [SerializeField] Transform RightEye;
    [SerializeField] float FallTime = 1.5F;
    [SerializeField] float EyeSpeed = 80;


    Vector3 RotationAxe; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Respawn"))
        {
            Debug.Log("Lose");
            LoseText.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        else if (other.CompareTag("Final Block"))
        {
            Debug.Log("Win");
            WinText.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        /*if(other.GetComponent<BlockData>().LookDirection != null)
        {
            //Vector3 LookPos = other.GetComponent<BlockData>().LookDirection.position;
            //float dot = Vector3.Angle(LeftEye.transform.right, (LookPos - LeftEye.transform.position).normalized);
            if (dot < 50)
            {
                HitText.SetActive(true);
                Debug.Log("dd");
            }
            else
            {
                Debug.Log("Lose");
                LoseText.SetActive(true);
                Time.timeScale = 0;
            }

        }*/
        //RotationAxe = other.GetComponent<BlockData>().RotationAxe;
        LeftEye.rotation = Quaternion.identity;
        RightEye.rotation = Quaternion.identity;
        InvokeRepeating("Rotation", 1, 0.0001f);
        InvokeRepeating("Rotation",Mathf.Infinity,0.01f);
        EyeSpeed += 10;
        //other.GetComponent<BlockFall>().FallWaitTime = FallTime;
        FallTime -= 0.2f;
        //other.GetComponent<BlockFall>().DominoFall();
    }
    void Rotation()
    {
        LeftEye.Rotate(RotationAxe * EyeSpeed * Time.deltaTime);
        RightEye.Rotate(RotationAxe * EyeSpeed * Time.deltaTime);
    }

    public void StopRotating()
    {
        CancelInvoke();
    }
}
