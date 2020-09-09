using UnityEngine;
using System.Collections;

public class CharacterFire : MonoBehaviour {

public Material[] mat;  //Array of materials on the gameObject
public GameObject fireFX;
public Light ExplodeLight;

private bool  startFade = false;  //For letting the script know it's time to start fading out
private float t = 0.0f;
private float fadeStart = 4;
private float fadeEnd = 0;
private float fadeTime = 1;
private float pauseTime = 0;
private Color32 defaultCol;

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


    fireFX.SetActive(false);
 
}

 
void Update (){

    if (Input.GetButtonDown("Fire1"))
    {

        // Set all materials to Fade rendering mode so they can be faded out

        for (int setfade = 0; setfade <= mat.Length - 1; setfade++) 
        {
      
			mat[setfade].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			mat[setfade].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat[setfade].EnableKeyword("_ALPHABLEND_ON");
            mat[setfade].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat[setfade].SetInt("_ZWrite", 0);
            mat[setfade].DisableKeyword("_ALPHATEST_ON");
            mat[setfade].renderQueue = 3000;
            mat[setfade].SetFloat("_Mode", 2);

        }



        // Fade out the explosion light
	    StartCoroutine("FadeLight"); 

        // Fade out the character
        startFade = true; 

        fireFX.SetActive(true);

    }
     
 
    if (startFade) 
    { 
        FadeOut();
    }


}


void FadeOut (){

    // Fade out each material on the character model

    for (int rend = 0; rend <= mat.Length - 1; rend++)
    { 
        Color color = mat[rend].color;  //The variable "color" is the renderers material color
        if (color.a >= 0) // If the colors alpha is greater than 0
        { 
            color.a -= 0.05f;  // Decrease the colors alpha by 0.05f - change this value to control the fade speed
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
               
         ExplodeLight.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
         yield return 0;

     }              
            
t = 0;
    
}
}