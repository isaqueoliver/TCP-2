using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovClick : MonoBehaviour {

    public Vector3 posicao;
    public float mov;
    public CharacterController controller;

    public Animation animar;

    public static bool attack;
    public static bool die;

    private Inimigo inimigo;

    // Use this for initialization
    void Start () {
        posicao = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!attack && !die)
        {
            if (Input.GetMouseButton(0))
            {
                locatePosition();
            }
                movToPosition();
        }
        else
        {

        }
	}

    public void locatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy" && hit.collider.tag != "Objeto")
            {
                posicao = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }
    }
    void movToPosition()
    {
        if (Vector3.Distance(transform.position, posicao) > 1)
        {
            Quaternion newRotation = Quaternion.LookRotation(posicao - transform.position);

            newRotation.x = 0f;
            newRotation.z = 0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
            controller.SimpleMove(transform.forward * mov);
            animar.CrossFade("Run");
            animar = GetComponent<Animation>();
        }
       else
        {
            animar.CrossFade("Idle");
            animar = GetComponent<Animation>();
        }
    }
}
