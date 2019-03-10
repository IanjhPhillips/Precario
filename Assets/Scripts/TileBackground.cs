using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBackground : MonoBehaviour
{

	Renderer rend;
	Vector2 textureOffSet;
	Vector2 scale;
	public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        scale = new Vector2 (gameObject.transform.localScale.x, gameObject.transform.localScale.z);
        scale/=scrollSpeed;
        textureOffSet = new Vector3 (gameObject.transform.position.x/scale.x, gameObject.transform.position.z/scale.y);
    }

    // Update is called once per frame
    void Update()
    {
        textureOffSet = new Vector3 (gameObject.transform.position.x/scale.x, gameObject.transform.position.z/scale.y);
        rend.material.mainTextureOffset =  textureOffSet;
    }
}
