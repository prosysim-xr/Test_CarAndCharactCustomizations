using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAtPosition : MonoBehaviour
{
    public Transform headRotateRigTransform;
    public Transform mainCameraTransform;
    public Transform bodyRotateRigTransform;

    //private Vector3 towardsSomeObject;//here in this case main camera
    public float speed =10;

    //about body speed
    public float bodyRotateSpeed=10f;

    private void Start()
    {
        headRotateRigTransform = GameObject.FindGameObjectWithTag("HeadRotateRig").GetComponent<Transform>();
        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        bodyRotateRigTransform = GameObject.FindGameObjectWithTag("bodyRotateRig").GetComponent<Transform>();
    }

    private void Update()
    {
        //LookAtPlayer(mainCameraTransform, headRotateRigTransform);
        //LookAtPlayer2(towardsSomeObject, Vector3.up);
        LookAtPlayer2(mainCameraTransform, headRotateRigTransform, bodyRotateRigTransform);

    }

    private void LookAtPlayer(Transform mainCameraTransform, Transform headRotateRigTransform)
    {
        headRotateRigTransform.LookAt(mainCameraTransform);
    }

    private void LookAtPlayer2(Transform mainCameraTransform, Transform headRotateRigTransform, Transform bodyRotateRigTransform)
    {

        //for head
        Vector3 headtargetDir = mainCameraTransform.position - headRotateRigTransform.position;
        float theta = Vector3.Angle(headtargetDir, headRotateRigTransform.parent.parent.forward);
        //float theta = AngleBetweenVector2(headtargetDir, headRotateRigTransform.parent.parent.forward);
        var headtargetRotationAngle = Quaternion.LookRotation(headtargetDir);

        //for body
        var tempCameraTransform = mainCameraTransform;
        Vector3 pos = new Vector3(mainCameraTransform.position.x, bodyRotateRigTransform.position.y, mainCameraTransform.position.z);
        tempCameraTransform.SetPositionAndRotation(pos, mainCameraTransform.rotation);

        Vector3 bodyTargetDir = (tempCameraTransform.position - bodyRotateRigTransform.position);
        //float theta1 = Vector3.Angle(bodyTargetDir, bodyRotateRigTransform.forward);

        float theta1 = AngleBetweenVector2(bodyTargetDir, bodyRotateRigTransform.forward);
        //var bodytargetRotationAngle = Quaternion.LookRotation(bodyTargetDir);
        //Quaternion qua = Quaternion.Euler(0, theta1, 0);

        //smoothly rotate corresponding to the field of view
        if (75>= theta &&theta >=-75) 
        {
            var smoothTargerRotationAngle = Quaternion.Slerp(headRotateRigTransform.rotation, headtargetRotationAngle, speed * Time.deltaTime);
            headRotateRigTransform.rotation = smoothTargerRotationAngle; 
        }
        else if (135>= theta && theta > 75) 
        {
            //var smoothTargerRotationAngle_body = Quaternion.Slerp(headRotateRigTransform.rotation, bodytargetRotationAngle, speed * Time.deltaTime);
            //bodyRotateRigTransform.rotation = smoothTargerRotationAngle_body;
            bodyRotateRigTransform.Rotate(Vector3.forward, theta1/**Time.deltaTime*/);

            /*var smoothTargetRotationAngle = Quaternion.Slerp(headRotateRigTransform.rotation, headtargetRotationAngle, speed*3 * Time.deltaTime);
            headRotateRigTransform.rotation = smoothTargetRotationAngle;*/
        }
        else if (180>= theta && theta >135) { }
        else if (135> theta && theta >=-180) { }
        else if (-75> theta && theta >=-135) { }

        float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
        {
            Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
            float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
            return Vector2.Angle(vec1, vec2) * sign;
        }
    }
}
