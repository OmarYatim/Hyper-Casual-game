using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // The target marker.
    public Transform target;
    Quaternion test;

    // Angular speed in radians per sec.
    public float speed = 1.0f;
    private void Start()
    {
        test = new Quaternion();

        //Time.timeScale = 0.2f;
    }

    void Update()
    {
        //Quaternion target = Quaternion.Euler(0, 0, 70);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, speed * Time.deltaTime);
        Vector3 direction = target.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);
    }
}
