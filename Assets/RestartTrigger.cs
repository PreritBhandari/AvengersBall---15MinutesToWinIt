using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTrigger : MonoBehaviour
{

    private Scene scene;


    // Start is called before the first frame update
    void Start()
    {
        scene=SceneManager.GetActiveScene();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Application.LoadLevel(scene.name);
        }
        else
        {
            
        }

    }
}
