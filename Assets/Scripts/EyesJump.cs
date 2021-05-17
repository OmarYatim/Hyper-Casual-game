using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesJump : MonoBehaviour
{
    [HideInInspector] public Collider Cube;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cube.enabled = false;
            transform.parent = null;
            GetComponent<ParabolaController>().FollowParabola();
            transform.GetChild(0).GetComponent<EyeController>().StopRotating();
            Invoke("ActivateCollider", 0.1f);
        }
    }

    void ActivateCollider()
    {
        Cube.enabled = true;
    }
}
