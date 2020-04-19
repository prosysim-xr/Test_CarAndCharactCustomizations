using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeControls : MonoBehaviour
{
    SkinnedMeshRenderer[] skinnedMeshRenderers;
    Mesh head;

    float speed = 100;
    //enum states for mouth open 
    enum Blinkstates
    {
        BlinkOpen,
        BlinkMid,
        BlinkClosed
    }

    public float eyeOpenValue=0;//0
    public float mouthOpenValue;//2
    public float mouthOpenRoundValue;//7
    public float nostrilsWideValue;//20
    public float smileValue;//1
    public float m_SoundValue;//4
    public float d_SoundValue;//6
    public float u_SoundValue;//5

    private void Awake()
    {
        skinnedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        

        for (var v = 0; v < skinnedMeshRenderers.Length; v++)
        {
            print(v + "th things is " + skinnedMeshRenderers[v]);
        }
        head =skinnedMeshRenderers[10].sharedMesh;//10th skinned mesh rendereer is head

    }
    void Start()
    {
        print("total number of blend shapes are" +head.blendShapeCount);
        for(var v =0; v<head.blendShapeCount; v++)
        {
            print("the "+head.GetBlendShapeName(v)+" is "+head.GetBlendShapeIndex(head.GetBlendShapeName(v)));
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            eyeOpenValue += 25f;
            if (eyeOpenValue <= 100) 
                skinnedMeshRenderers[10].SetBlendShapeWeight(0, eyeOpenValue);  
        }if (Input.GetKeyDown(KeyCode.H))
        {
            eyeOpenValue -= 25f;
            if (eyeOpenValue >=25 ) 
                skinnedMeshRenderers[10].SetBlendShapeWeight(0, eyeOpenValue);  
        }
        /*if (Blinkstates ==Binkstates.BlinkOpen)
        {

        }*/
    }
}
