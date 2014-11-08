using System;
using Assets.Controls;
using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour {

	// Use this for initialization
    public Vector2 movement { get; set; }
    void Start ()
    {
        MovementVariables.MoveSpeed = 4;
        MovementVariables.MaxSpeed = MovementVariables.MoveSpeed*1.10f;
        MovementVariables.TurnaroundTime = 0.1f;
        MovementVariables.Controller = ControlMethod.Keyboard;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (MovementVariables.Controller == ControlMethod.Keyboard)
	    {
            keyboardMovement();
            keyboardLookAt();   
	    }
	    if (MovementVariables.Controller == ControlMethod.Joystick)
	    {
            joystickMovement();
	        joystickLookAt();
	    }
        Animate();
	}
    /*Animate Method needs to be moved, it's here for demo purposes.*/
    void Animate()
    {
        if (transform.rigidbody2D.velocity != Vector2.zero)
        {
            gameObject.GetComponent<Animator>().Play("Walking");
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("Idle");   
        }
    }
   
    private void joystickMovement()
    {
        float x = Input.GetAxis("HorizontalJS");
        float y = Input.GetAxis("VerticalJS");

        float speed = MovementVariables.MoveSpeed;
        float deltaX = (x / 3) * speed + (2*(y / 3)) * speed;
        float deltaY = (y / 3) * speed - (2*(x / 3)) * speed;
        transform.rigidbody2D.velocity = new Vector2(deltaX, deltaY);
    }

    private void joystickLookAt()
    {
        Vector3 diff = new Vector3(Input.GetAxis("VerticalRight") * 100, Input.GetAxis("HorizontalRight") * 100, 0f);
        if (Mathf.Abs(diff.x) > 0.2 || Mathf.Abs(diff.y) > 0.2)
        {
            var angle = Mathf.Atan2(diff.x, diff.y)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 135);
        }
        else
        {
            float x = Input.GetAxis("HorizontalJS");
            float y = Input.GetAxis("VerticalJS");
            var angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle-45), 0.6f);
        }
    }
    private void keyboardMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float speed = MovementVariables.MoveSpeed;
        float deltaX = (x / 2)*speed + (y / 2)*speed;
        float deltaY = (y / 2)*speed - (x / 2)*speed;
        transform.rigidbody2D.velocity = new Vector2(deltaX,deltaY);
            //new Vector2(x * MovementVariables.MoveSpeed, y * MovementVariables.MoveSpeed);
    }

	private void keyboardLookAt()
	{
	    Vector3 targetPosition = new Vector3();
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;
	    if (Input.mousePosition.x < Screen.width ||
            Input.mousePosition.x > 0 ||
            Input.mousePosition.y < Screen.height||
	        Input.mousePosition.y > 0)
	    {
	        if (Physics.Raycast(ray, out hit))
	        {
	            targetPosition = hit.point - transform.position;
	            targetPosition.Normalize();
	            var angle = Mathf.Atan2(targetPosition.x, targetPosition.y)*Mathf.Rad2Deg;
	            transform.rotation = Quaternion.Euler(0, 0, -angle);
	        }
	    }
	    else
	    {
	        float x = Input.GetAxis("Horizontal");
	        float y = Input.GetAxis("Vertical");
	        var angle = Mathf.Atan2(x, y)*Mathf.Rad2Deg;
	        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle - 45), 0.4f);
	    }

	    //Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	    //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
	    //Debug.Log(String.Format("X:{0}, Y{1} Z:{2}",diff.x, diff.y, rot_z));
	    //transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, rot_z - 135);
	}
}
