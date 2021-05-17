using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFall : MonoBehaviour
{
    [SerializeField] Transform CoM;
     public float FallWaitTime;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CoM.localPosition;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DominoFall()
    {
        Invoke("Fall",FallWaitTime);
    }

    void Fall()
    {
        GetComponent<Collider>().enabled = true;
        rb.isKinematic = false;
    }
}
