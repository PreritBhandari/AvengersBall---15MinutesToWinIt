using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{   
    private Animator Anim;
    
    // [SerializeField]
    // KeyCode keyLefttira;

    // [SerializeField]
    // KeyCode keyRighttira;


    // Start is called before the first frame update
    void Start()
    {
        Anim=gameObject.GetComponent<Animator> ();

    }

    // Update is called once per frame
    void Update()
    {
        
    if (Input.GetKeyDown (KeyCode.UpArrow))
    {
        Anim.SetBool("isWalking",true);
         
    }
    else if (Input.GetKeyUp (KeyCode.UpArrow))
    {
        Anim.SetBool("isWalking",false);
    }

    if (Input.GetKeyDown (KeyCode.Space))
    { 
        Anim.SetBool("isJumping",true);
    }
   
    else if (Input.GetKeyUp (KeyCode.Space))
    { 
        Anim.SetBool("isJumping",false);
    }

    
    
   
    }
}
