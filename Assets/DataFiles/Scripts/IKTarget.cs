using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 35f;



    public Transform headTarget;
    public bool isRotating = true;

    public Animator ator;
    public float weight = 1f;

    private void Start()
    {
        ator = this.GetComponent<Animator>();
        
        
    }

    private void Update()
    {
        headTarget.parent.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
       
        if (Input.GetKey(KeyCode.UpArrow))
        {
            target.transform.Translate(0, 0.1f * speed * Time.deltaTime, 0);
        }if (Input.GetKey(KeyCode.DownArrow))
        {
            target.transform.Translate(0, -0.1f * speed * Time.deltaTime, 0);
        }if (Input.GetKey(KeyCode.RightArrow))
        {
            target.transform.Translate(-0.1f * speed * Time.deltaTime, 0, 0);
        }if (Input.GetKey(KeyCode.LeftArrow))
        {
            target.transform.Translate(0.1f * speed * Time.deltaTime, 0, 0);
        }if (Input.GetKey(KeyCode.F))
        {
            target.transform.Translate(0, 0, 0.1f * speed * Time.deltaTime);
        }if (Input.GetKey(KeyCode.E))
        {
            target.transform.Translate(0, 0, -0.1f * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R))
        {
            if(!isRotating)
            {
                headTarget.transform.parent.Rotate(new Vector3(0,30f,0));
                isRotating = true;
            }
            else
            {
                //headTarget.parent.Rotate(0,0,0);
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //weight = ator.GetFloat("ASDFasdf")
        ator.SetIKPosition(AvatarIKGoal.RightHand, target.position);
        //ator.SetIKRotation(AvatarIKGoal.RightHand, target.position);
        ator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);

        ator.SetLookAtPosition(headTarget.position);
        ator.SetLookAtWeight(weight);

        
    }
}
