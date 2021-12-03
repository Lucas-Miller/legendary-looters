using System.Collections;
using System.Collections.Generic;
using Unity.AI;
using Unity.AI.Navigation;
using UnityEngine;

public class LinkNavMesh : MonoBehaviour
{
    private bool hasUpdated = false;
    // Start is called before the first frame update
    void Start()
    {
        //hasUpdated = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        var NavLinkData = GetComponent<NavMeshLink>();
        NavLinkData.UpdateLink();
        //var navLink = GetComponent<NavMeshLink>();
    }
}
