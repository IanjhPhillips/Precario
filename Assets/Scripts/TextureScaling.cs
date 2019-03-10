using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextureScaling : MonoBehaviour
{
	public float scaleFactor = 0.5f;
	private Renderer rend;
	private Vector2 tiling;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.gameObject.GetComponent<Renderer>();    
        tiling = new Vector2 (transform.localScale.x, transform.localScale.z);
        rend.sharedMaterial.mainTextureScale = tiling*scaleFactor;
    }

    void Update () {
    	if (tiling.x != transform.localScale.x || tiling.y != transform.localScale.z ) {
    		tiling = new Vector2 (transform.localScale.x, transform.localScale.z);
        	rend.material.mainTextureScale = tiling*scaleFactor;
    	}
    }

}
