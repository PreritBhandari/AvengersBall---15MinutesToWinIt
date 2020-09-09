using UnityEngine;
using System.Collections;

public class CharacterIce : MonoBehaviour {


public float frozenWaitTime;  // Time to wait before the frost shatters
public Material[] mat;  //Array of materials on the gameObject

public GameObject freezeFX;
public GameObject shatterFX;
public Light freezeLight;
public GameObject rootNode;
public GameObject characterSkeleton;
public Texture iceTexture;

private bool  startFade = false;  //For letting the script know it's time to start fading out
private float t = 0.0f;
private float fadeStart = 4;
private float fadeEnd = 0;
private float fadeTime = 1;
private float pauseTime = 0;
private bool  freezeWait = false; 



void Start (){

    
    // Reset all character materials to opaque untextured matte grey - good for testing purposes
   

    for (int rend = 0; rend <= mat.Length - 1; rend++)  //For each material
    {  
   
        mat[rend].SetOverrideTag("RenderType", "");
		mat[rend].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		mat[rend].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat[rend].SetInt("_ZWrite", 1);
        mat[rend].DisableKeyword("_ALPHATEST_ON");
        mat[rend].DisableKeyword("_ALPHABLEND_ON");
        mat[rend].DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat[rend].renderQueue = -1;

	    mat[rend].SetColor("_Color", Color.gray);
        mat[rend].SetColor("_SpecColor", Color.grey);
        mat[rend].SetFloat("_Glossiness", 0.141f);
        mat[rend].mainTexture = null;

    }
   

    freezeFX.SetActive(false);
    shatterFX.SetActive(false);


 
}


void Update (){

    if (Input.GetButtonDown("Fire1"))
    {

        freezeFX.SetActive(true);

        // Set each material to frozen ice

        for (int rend = 0; rend <= mat.Length - 1; rend++)
        {  
        
			 mat[rend].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			 mat[rend].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
             mat[rend].EnableKeyword("_ALPHABLEND_ON");
             mat[rend].DisableKeyword("_ALPHAPREMULTIPLY_ON");
             mat[rend].SetInt("_ZWrite", 0);
             mat[rend].DisableKeyword("_ALPHATEST_ON");
             mat[rend].renderQueue = 3000;
             mat[rend].SetFloat("_Mode", 2);

			 mat[rend].SetColor("_Color", Color.white);
             mat[rend].SetFloat("_Glossiness", 0.842f);
			 mat [rend].mainTexture = iceTexture;


        }

        
        // Freeze the animation

        rootNode.GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        characterSkeleton.transform.parent = null;

       
        // Wait to shatter the frozen character
		StartCoroutine("FreezeWait");

        startFade = true; 

    }


    // Fade out the frozen character
 
    if (startFade) 
    {  
  
        if (freezeWait)
        {
            FadeOut ();  //Run the FadeOut function
        }

    }
   
 
}


IEnumerator FreezeWait (){

	yield return new  WaitForSeconds (frozenWaitTime);

    freezeWait = true;

    GetComponent<Rigidbody>().detectCollisions = false;
    freezeLight.transform.parent = null;

	StartCoroutine("FadeLight");
    shatterFX.SetActive(true);

}


void FadeOut (){

    // Fade out each material

    for (int rend = 0; rend <= mat.Length - 1; rend++)
    { 
        Color color = mat[rend].color;  //The variable "color" is the renderers material color
        if (color.a >= 0) // If the colors alpha is greater than 0
        { 
            color.a -= 0.5f;  // Decrease the colors alpha by 0.05f - change this value to control the fade speed
            mat[rend].color = color;  // Update the renderers material color
        }
    }
  
 
}


IEnumerator FadeLight (){
   
     while (t < fadeTime) 
     {

         if (pauseTime == 0)
         {
             t += Time.deltaTime;
         }
               
         freezeLight.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
         yield return 0;

     }              
            
t = 0;
    
}


}