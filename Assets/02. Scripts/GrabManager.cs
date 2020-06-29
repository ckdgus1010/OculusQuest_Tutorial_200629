using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    private FixedJoint joint;
    private GameObject grabObject;
    private bool isTouched = false;

    void Start()
    {
        joint = this.gameObject.GetComponent<FixedJoint>();
    }


    private void Update()
    {
        //물건을 잡을 때
        if (isTouched && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            joint.connectedBody = grabObject.GetComponent<Rigidbody>();
        }

        //잡은 물건을 던질 때
        if (isTouched && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            //오른쪽 Controller의 속도 산출
            Vector3 _velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            grabObject.GetComponent<Rigidbody>().velocity = _velocity;
            joint.connectedBody = null;
            isTouched = false;
        }

        //햅틱 효과
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BALL"))
        {
            isTouched = true;
            grabObject = other.gameObject;
        }
    }
}
