using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFall : MonoBehaviour
{
    [SerializeField] float Force;
    [SerializeField] Transform ForcePoint;
    
    [HideInInspector] public float WaitForFall;
    
    Rigidbody rb;
    MeshRenderer mesh;
    bool StartTiming = false;
    float TimePassed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StartTiming)
        {
            Timer();
        }
    }

    void Timer()
    {
        TimePassed += Time.deltaTime;
        float value = TimePassed / WaitForFall;
        Color MatColor = new Color(1, 1 - value, 1 - value);
        mesh.material.color = MatColor;
        if (TimePassed >= WaitForFall)
        {
            StartTiming = false;
        }
    }

    public void DominoFall()
    {
        StartTiming = true;
        Invoke("Fall", WaitForFall);
    }

    void Fall()
    {
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        Vector3 Dir = Vector3.left * Force;
        rb.AddForceAtPosition(Dir, ForcePoint.position);
    }

}
