using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject projectile;
    public int intensity = 2; 
    private Vector3 startPos = new Vector3();
    private Vector3 inputPos = new Vector3();
    private Vector3 releasePos = new Vector3();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        // click to set the ball

        if (Input.GetMouseButtonDown(1)) {
            inputPos = Input.mousePosition; 
            projectile.GetComponent<Rigidbody>().isKinematic = true;
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 2.0f)) ;

            //adjust starting pos
            projectile.transform.position = startPos ;


            // show trajectory

            //bezier curve

        }

        // drag  to add intensity

        if (Input.GetMouseButtonUp(1))
        {
            // calculation trajectory and momentum
            releasePos =Input.mousePosition; 
            Vector3 direction = (startPos - Camera.main.transform.position).normalized;
            float momentum =  Vector3.Distance (releasePos, inputPos) * intensity;

            //throw
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().AddForce(direction * momentum);

        }
    }
}
