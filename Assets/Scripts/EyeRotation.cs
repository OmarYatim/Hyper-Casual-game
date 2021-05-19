using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotation : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 20;

    [HideInInspector] public Vector3 RotationAxe;
    [HideInInspector] public bool CanRotate = false;
    [HideInInspector] public Transform RotateDirection;
    bool isCorrect = false;
    // Start is called before the first frame update
    void Start()
    {
        //Quaternion rot = Quaternion.FromToRotation(transform.right, LookDirection.position - transform.position);
        RotationAxe = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanRotate)
            Rotate();
        //transform.Rotate(RotationAxe * 50 * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.right, Color.green, Mathf.Infinity);

    }

    void Rotate()
    {
        Vector3 Diraction = (RotateDirection.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.FromToRotation(RotationAxe, Diraction);
        float angle = 0;
        Vector3 axis = Vector3.zero;
        lookRotation.ToAngleAxis(out angle, out axis);
        transform.Rotate(axis);
    }

    public bool CheckLook()
    {
        isCorrect = false;
        Debug.DrawRay(transform.position, transform.right, Color.green, Mathf.Infinity);

        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.right,out hit, 1.5f))
        {
            if (hit.transform.CompareTag("LookDirection"))
            {
                Debug.Log("3asba",gameObject);
                isCorrect = true;
                CanRotate = true;
            }
        }
        return isCorrect;
    }
}
