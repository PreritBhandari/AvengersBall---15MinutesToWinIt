using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerControlledVelocity : MonoBehaviour
{   
    [SerializeField]
    Vector3 v3forcesphereko;

    [SerializeField]
    KeyCode keyLefttira;

    [SerializeField]
    KeyCode keyRighttira;

    // Update is called once per frame
    void FixedUpdate()
    {

    if (Input.GetKey(keyLefttira))

        GetComponent<Rigidbody>().velocity+=v3forcesphereko;
    
    
    if (Input.GetKey(keyRighttira))

        GetComponent<Rigidbody>().velocity-=v3forcesphereko;
    
    
    }
}
