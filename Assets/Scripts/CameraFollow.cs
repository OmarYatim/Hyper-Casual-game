using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform ObjectToFollow;
    [SerializeField] float FollowSpeed;
    float xOffset;
    // Start is called before the first frame update
    void Start()
    {
        xOffset = Mathf.Abs(ObjectToFollow.position.x - transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(ObjectToFollow.position.x + xOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);
    }
}
