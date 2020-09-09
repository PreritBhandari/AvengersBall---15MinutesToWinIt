using UnityEngine;
using System.Collections;

public class CharacterBlood : MonoBehaviour {

public Material[] mat;  //Array of materials on the gameObject
public GameObject bloodFX;

private bool  startFade = false;  //For letting the script know it's time to start fading out

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


    bloodFX.SetActive(false);
     
}

 
void Update (){

    if (Input.GetButtonDown("Fire1"))
    {

        // Set all materials to color red and Fade rendering mode so they can be faded out

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

			mat[setfade].SetColor("_Color", Color.red);
            mat[setfade].SetColor("_SpecColor", Color.red);
            

        }


        // Start death particle effects
        bloodFX.SetActive(true);

        // Fade out the character
        startFade = true; 

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
            color.a -= 0.1f;  // Decrease the colors alpha by 0.1f - change this value to control the fade speed
            mat[rend].color = color;  // Update the renderers material color
        }
    }

}


}