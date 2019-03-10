using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public float accelMod;
	private Vector2 accelDir;
	public float deadZone;
	public float maxSpeed;

	public float jumpForce;
	private bool canJump;
	private int platCount = 0;

	private Rigidbody rb;

	private Vector2 mouseInitial;

	private Vector3 spawn;
	private GameObject deathPoint;

	public GameObject lineObject;
	private LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
    	deathPoint = GameObject.FindWithTag("DeathPoint");
    	spawn = this.transform.position;
    	canJump = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        lineRend = lineObject.GetComponent<LineRenderer>();
        lineRend.enabled = false;
        mouseStart(false);

    }

    void OnTriggerEnter (Collider other) {

    	if (other.gameObject.transform.tag == "Platform") {
    		canJump = true;
    		platCount++;
    	}

    }
    void OnTriggerExit (Collider other) {

    	if (other.gameObject.transform.tag == "Platform") {
    		platCount--;
    		if(platCount <= 0)
    			canJump = false;
    	}

    }

    // Update is called once per frame
    void Update()
    {
    	//Respawning
    	if (this.gameObject.transform.position.y <= deathPoint.transform.position.y) {	
    		Respawn();
    	}


    	//movements

    	//mouse movement
        if (Input.GetKeyDown(KeyCode.Mouse0)) { 
        	lineRend.enabled = true;      	
        	mouseStart(true);
        }
        else if (Input.GetKey(KeyCode.Mouse0)) {

        	Vector2 target = getMouseInput ();
        	lineRend.SetPosition(0, new Vector3 (mouseInitial.x, 0f, mouseInitial.y) + transform.position);
        	lineRend.SetPosition(1, new Vector3 (target.x, 0f, target.y) + transform.position);

        	if (!canJump)
        		mouseForce(accelMod*0.5f*Time.deltaTime, target);
        	else
        		mouseForce(accelMod*Time.deltaTime, target);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
			mouseStart (false);   
			lineRend.enabled = false;
		}

        //jumping
        if (Input.GetKeyDown("space")) {
    		
        	jumpCheck();
    	}
    	
        
    }

    private void mouseStart (bool init) {

    	if (init) {
    		mouseInitial = getMouseInput ();
    		
    	}
    	else {
    		accelDir = new Vector2(0f, 0f); 
    		
    	}

    }
    private void mouseForce (float accel, Vector2 target) {

    	Vector2 direction = new Vector2(rb.velocity.x, rb.velocity.z);
    	accelDir = target - mouseInitial;

    	print (accelDir);

    	if (accelDir.magnitude >= deadZone && (direction.magnitude < maxSpeed)) {
    		rb.AddForce (new Vector3(accelDir.x*accel, 0f, accelDir.y*accel));
    	}
    	else if (rb.velocity.magnitude > maxSpeed) {
    		
    		direction.Normalize();
    		direction*=maxSpeed*0.98f;
    		rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.y);
    	}
    }
    private void jumpCheck () {

    	if (canJump) {

    		rb.AddForce(Vector3.up*jumpForce);

    	}

    }

    private Vector2 getMouseInput () {

    	print("attempt");
    	RaycastHit hit;
    	if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
    		return new Vector2 (hit.point.x - transform.position.x, hit.point.z - transform.position.z);
    	}
    	else return new Vector2(0f, 0f);

    }

    private void Respawn () {

    	this.transform.position = spawn;
    	rb.velocity = Vector3.zero;
    	rb.angularVelocity = Vector3.zero;
    	mouseStart(false);

    }

    public void SetSpawn (Vector3 newSpawn) {spawn = newSpawn;}
}
