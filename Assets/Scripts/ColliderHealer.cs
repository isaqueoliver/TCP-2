using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHealer : MonoBehaviour {

    [SerializeField]
    GameObject healer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider aliado)
    {
        if (aliado.gameObject.tag == "Enemy")
        {
            healer.GetComponent<Inimigo3>().aliado = aliado.gameObject;
        }
    }
}
