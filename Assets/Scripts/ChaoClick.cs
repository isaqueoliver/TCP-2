﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoClick : MonoBehaviour {

    public bool clickChao;

    public Transform jogador;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickChao = true;
            //jogador.GetComponent<Combate>().inimigo = null;
        }
    }
}
