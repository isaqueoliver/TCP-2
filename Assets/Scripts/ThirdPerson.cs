using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour {

    [SerializeField]
    Transform player;
    public int dist, alt, vel;

    public LayerMask layerMask;
    public Color transpColor;
    //public Color normalColor;
    //private Transform o;

    //teste
    public List<Transform> hiddenObjects;
    private List<Color> hiddenColors;

    bool wasHit = true;

    private void Start()
    {
        //Initialize the list
        hiddenObjects = new List<Transform>();
        hiddenColors = new List<Color>();
    }

    void Update()
    {
        /*RaycastHit hit;
        
        if(Physics.Raycast(this.transform.position, transform.forward, out hit, layerMask))
        {
            if (hit.collider.tag == "Objeto")
            {
                hit.collider.GetComponent<Renderer>().material.color = transpColor;
                if (wasHit)
                {
                    o = hit.collider.GetComponent<Transform>();
                    wasHit = false;
                }
                if (o.transform != hit.collider.transform)
                {
                    o.GetComponent<Renderer>().material.color = normalColor;
                    wasHit = true;
                }
            
            }

            if(hit.collider.tag == "Player")
            {
                if (o != null)
                {
                    o.GetComponent<Renderer>().material.color = normalColor;
                    o = null;
                    wasHit = true;
                }
            }
        }
        else
        {
            if(o != null)
            {
                o.GetComponent<Renderer>().material.color = normalColor;
                o = null;
            }
        }*/

        //Find the direction from the camera to the player
        Vector3 direction = player.position - this.transform.position;

        //The magnitude of the direction is the distance of the ray
        float distance = direction.magnitude;

        //Raycast and store all hit objects in an array. Also include the layermaks so we only hit the layers we have specified
        RaycastHit[] hits = Physics.RaycastAll(this.transform.position, direction, distance, layerMask);

        //Go through the objects
        for (int i = 0; i < hits.Length; i++)
        {
            Transform currentHit = hits[i].transform;

            //Only do something if the object is not already in the list
            if (!hiddenObjects.Contains(currentHit))
            {
                //Add to list and disable renderer

                hiddenObjects.Add(currentHit);
                hiddenColors.Add(currentHit.GetComponent<Renderer>().material.color);
                currentHit.GetComponent<Renderer>().material.color = transpColor;
            }
        }

        //clean the list of objects that are in the list but not currently hit.
        for (int i = 0; i < hiddenObjects.Count; i++)
        {
            bool isHit = false;
            //Check every object in the list against every hit
            for (int j = 0; j < hits.Length; j++)
            {
                if (hits[j].transform == hiddenObjects[i])
                {
                    isHit = true;
                    break;
                }
            }

            //If it is not among the hits
            if (!isHit)
            {
                //Enable renderer, remove from list, and decrement the counter because the list is one smaller now
                Transform wasHidden = hiddenObjects[i];
                wasHidden.GetComponent<Renderer>().material.color = hiddenColors[i];
                hiddenObjects.RemoveAt(i);
                hiddenColors.RemoveAt(i);
                i--;
            }
        }
    }

    private void LateUpdate()
    {
        if (!player)
            return;
        float camPosFut = player.position.y + alt;
        float camPosNow = transform.position.y;

        camPosNow = Mathf.Lerp(camPosNow, camPosFut, vel * Time.deltaTime);

        transform.position = player.position;
        transform.position -= Vector3.forward * dist;

        this.transform.position = new Vector3(transform.position.x, camPosNow, transform.position.z);

        transform.LookAt(player);
    }
}
