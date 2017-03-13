using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MusaCtrl : MonoBehaviour {

    private Transform tr;
    private Animator anim;
    private NavMeshAgent nv;

    public float damping = 10.0f;
    public float speed = 1.0f;

    private Ray ray;
    private RaycastHit hit;
    private int floorLayer;
   
    public Vector3 movePos = Vector3.zero;
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        nv = GetComponent<NavMeshAgent>();
        floorLayer = LayerMask.NameToLayer("FLOOR");

        movePos = tr.position;
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 100.0f, 1 << floorLayer))
        {
            movePos = hit.point;
            Debug.Log(movePos);
            nv.SetDestination(movePos);
            nv.Resume();
        }
            
        if (nv.velocity.magnitude >= 0.1f && nv.remainingDistance >= 0.2f)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }   
//        if ((movePos - tr.position).sqrMagnitude >= 0.2f)
//        {
//            Quaternion rot = Quaternion.LookRotation(movePos - tr.position);
//            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
//            tr.Translate(Vector3.forward * Time.deltaTime * speed);
//            anim.SetBool("isRun", true);
//           
//        }
//        else
//        {
//            anim.SetBool("isRun", false);
//        }
	}
}
