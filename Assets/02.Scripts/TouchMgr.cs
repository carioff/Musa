using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMgr : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        #if UNITY_ANDROID
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, 100.0f, 1 << 8))
            {
                Destroy(hit.collider.gameObject);
                ExpBox();
            }
        }
        #endif

        #if UNITY_EDITOR
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 100.0f, 1 << 8))
        {
            Destroy(hit.collider.gameObject);
            ExpBox();
        }
        #endif
	}

    void ExpBox()
    {
        //순간적으로 오버랩 
        Collider[] colls = Physics.OverlapSphere(hit.point, 10.0f, 1 << 9);
        foreach (var coll in colls)
        {
            coll.GetComponent<Rigidbody>().AddExplosionForce(1500.0f, hit.point, 10.0f, 1200.0f);
        }
    }
}
