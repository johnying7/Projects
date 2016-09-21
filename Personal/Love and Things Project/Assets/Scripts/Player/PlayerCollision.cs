using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
    
	public float pushPower = 1.0f;
	
	private float timer = 0;
	private float coolDown = 0.1f;
	
    void OnControllerColliderHit(ControllerColliderHit hit) {
		if(Time.time >= timer)
		{
			timer = Time.time + coolDown;
	        Rigidbody body = hit.collider.attachedRigidbody;
	        if (body == null || body.isKinematic)
	            return;
			
			Vector3 forceDir = (hit.transform.position - this.transform.position).normalized;
			body.AddForce(this.GetComponent<Rigidbody>().mass*forceDir.normalized/0.5f, ForceMode.Impulse);
		}
    }
}
