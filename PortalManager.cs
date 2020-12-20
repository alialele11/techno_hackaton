using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PortalManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Sponza;
    private Material[] SponzaMaterials;
    void Start
    {
        SponzaMaterials = Sponza.GetComponent<Renderer>().sharedMaterials;
    }
    void OnTriggerStay(Collider collider)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);
        if(camPositionInPortalSpace.y< 1.0f){
            for(int i = 0; i < SponzaMaterials.Length; ++i)
        {
            SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
        }
        }
    else
    {
        for (int i = 0; i < SponzaMaterials.Length; ++i)
        {
            SponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
        }
    }
    }
}