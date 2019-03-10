using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	private static CheckPoint currentPoint;
	private bool isChecked = false;
	private bool isCurrent = false;

	private Mesh unCheckedMesh; //mesh if unchecked
	private Mesh checkedMesh;	//mesh if current checkpoint
	private Mesh currentMesh;	//mesh if visited but not current checkpoint
	private MeshFilter meshFilter;

	private Vector3 spawn;

	private ParticleSystem particleSys;



    // Start is called before the first frame update
    void Start()
    {
    	currentPoint = null;

    	spawn = transform.GetChild(0).transform.position;

    	meshFilter = gameObject.GetComponent<MeshFilter> ();
    	particleSys = gameObject.GetComponent<ParticleSystem>();

        unCheckedMesh = (Mesh) Resources.Load<Mesh> ("Meshes/Meshes_Flags/mesh_Flag_01");
        checkedMesh = (Mesh) Resources.Load<Mesh> ("Meshes/Meshes_Flags/mesh_Flag_02");
        currentMesh = (Mesh) Resources.Load<Mesh> ("Meshes/Meshes_Flags/mesh_Flag_03");
        meshUpdate();
        
    }

    void OnTriggerEnter (Collider other) {

    	//print ("triggering checkpoint");
    	if (other.transform.tag == "PlayerBall" && !isCurrent) {		
    		if (currentPoint != null) {
    			currentPoint.setIsCurrent (false);
    			print(currentPoint.gameObject.name + " is current");
 		  		currentPoint.meshUpdate();
 		  	}

 		  	currentPoint = this;
 		  	print(currentPoint.gameObject.name);
 		  	isCurrent = true;
 		  	isChecked = true;
 		  	particleSys.Play();
 		  	meshUpdate();
 		  	other.GetComponent<Ball>().SetSpawn(spawn);
    	  	
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void meshUpdate () {

    	print (gameObject.name + " is updating!");

    	if (isCurrent) {
    		meshFilter.mesh = currentMesh;
    		print ("now current");
    	}
    	else if (isChecked) {
    		meshFilter.mesh = checkedMesh;
    		print ("now checked");
    	}
    	else {
    		meshFilter.mesh = unCheckedMesh;
    		print ("now UNchecked");
    	}

    }

    public void setIsCurrent (bool value) {
    	isCurrent = value;
    }
}
