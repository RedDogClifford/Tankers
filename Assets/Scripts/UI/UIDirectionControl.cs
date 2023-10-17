using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    //Ensure UI faces the right direction

    public bool useRelationRotation = true;

    private Quaternion relationRotation;

    // Start is called before the first frame update
    void Start()
    {
        relationRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (useRelationRotation) transform.rotation = relationRotation;
    }
}
