using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRaio : MonoBehaviour {

    List<Collider> inimigos;

    public GameObject raio;

    public ParticleSystem raioSkill;
	// Use this for initialization
	void Start () {
        inimigos = new List<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        ClearList();
	}

    void ClearList()
    {
        if(raioSkill.isStopped)
        {
            inimigos.Clear();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Collider alvoAtual = other;
        if(!inimigos.Contains(alvoAtual))
        {
            inimigos.Add(alvoAtual);
            if (alvoAtual.gameObject.GetComponent<Inimigo>())
            {
                alvoAtual.gameObject.GetComponent<Inimigo>().vida -= raio.GetComponent<Skills>().danoRaio;
            }
            if (alvoAtual.gameObject.GetComponent<Inimigo2>())
            {
                alvoAtual.gameObject.GetComponent<Inimigo2>().vida -= raio.GetComponent<Skills>().danoRaio;
            }
            if (alvoAtual.gameObject.GetComponent<Inimigo3>())
            {
                alvoAtual.gameObject.GetComponent<Inimigo3>().vida -= raio.GetComponent<Skills>().danoRaio;
            }
        }
    }
}
