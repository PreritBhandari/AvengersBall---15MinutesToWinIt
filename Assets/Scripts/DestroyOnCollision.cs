using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnCollision : MonoBehaviour
{   
    public ParticleSystem marekoPart;
    

    private void OnCollisionEnter(Collision collision)
    {   

        if(collision.collider.CompareTag("Player"))
        {   
            Instantiate(marekoPart,transform.position,Quaternion.identity);
            Destroy();
        }
        

    }


    public void Destroy()
    {
        Destroy(gameObject);

    }



}
