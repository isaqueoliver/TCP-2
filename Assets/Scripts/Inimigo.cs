using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour {

    public float vel, range, rangeBattle;
    public Transform jogador;

    public CharacterController controller;

    //em qual tempo da animção vai hitar
    public double hitou;
    private bool hit;
    private Combate oponente;
    public int dano;

    public float atkSpeed;

    public GameObject ronda, comecoRonda, finalRonda, cameraGO;

    public int vida;
    private bool trocaRonda;

    [SerializeField]
    Slider sliderHealth;


    // Use this for initialization
    void Start () {
        oponente = jogador.GetComponent<Combate>();
        this.transform.position = new Vector3(ronda.transform.position.x, this.transform.position.y, ronda.transform.position.z);
        sliderHealth.maxValue = vida;
    }
	
	// Update is called once per frame
	void Update () {
        if (!morreu())
        {
            if (!inRangeBattle())
            {
                Ronda();
            }
            else if(!inRange())
            {
                perserguir();
            }
            else
            {
                GetComponent<Animation>().Play("Ataque_1");
                GetComponent<Animation>()["Ataque_1"].speed = 1 + atkSpeed;
                ataque();
                if (GetComponent<Animation>()["Ataque_1"].time > 0.9 * GetComponent<Animation>()["Ataque_1"].length)
                    hit = false;
            }
        }
        else
            dieMethod();
        sliderHealth.transform.LookAt(cameraGO.transform);
        sliderHealth.value = vida;
    }

    void Ronda()
    {
        Vector3 relativePos = finalRonda.transform.position - this.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        newRotation.x = 0;
        newRotation.z = 0;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
        if (Vector3.Distance(finalRonda.transform.position, this.transform.position) > 1)
        {
            controller.SimpleMove(transform.forward * vel);
            GetComponent<Animation>().Play("Correndo");
        }
        //this.transform.position = ronda.transform.position;
        //GetComponent<Animation>().CrossFade("idle");

        /*if(trocaRonda)
        {

            Vector3 newRotate = this.transform.position - finalRonda.transform.position;
            float angleRotate = Vector3.Angle(newRotate, this.transform.forward);

            if(this.transform.eulerAngles.y > angleRotate)
            {
                this.transform.eulerAngles = new Vector3(0, vel * Time.deltaTime, 0);
            }

            controller.SimpleMove(transform.forward * vel);
        }
        else if(!trocaRonda)
        {
            Vector3 relativePos = finalRonda.transform.position - this.transform.position;
            Quaternion newRotation = Quaternion.LookRotation(relativePos);
            newRotation.x = 0;
            newRotation.z = 0;
            //this.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

            controller.SimpleMove(transform.forward * vel);
            GetComponent<Animation>().CrossFade("run");
            if (/*Vector3.Distance(finalRonda.transform.position, this.transform.position) >= 6 && !trocaRonda this.transform.position.z >= comecoRonda.transform.position.z)
            {
                Debug.Log("fodeu2");
                this.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
                trocaRonda = true;
            }
        }*/
    }

    bool inRangeBattle()
    {
        if (Vector3.Distance(transform.position, jogador.position) < rangeBattle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool inRange()
    {
        if(Vector3.Distance(transform.position, jogador.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ataque()
    {
        if(GetComponent<Animation>()["Ataque_1"].time > GetComponent<Animation>()["Ataque_1"].length * hitou && !hit && GetComponent<Animation>()["Ataque_1"].time < 0.9 * GetComponent<Animation>()["Ataque_1"].length)
        {
            oponente.getHit(dano);
            hit = true;
        }
    }

    public void getHit(int dano)
    {
        vida -= dano;
        if (vida < 0)
            vida = 0;
        Debug.Log(vida);
    }

    void perserguir()
    {
        transform.LookAt(jogador.position);
        controller.SimpleMove(transform.forward * vel);
        GetComponent<Animation>().Play("Correndo");
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            jogador.GetComponent<Combate>().inimigo = this.gameObject;
        }
        if (jogador.GetComponent<Skills>().targetSkill3)
        {
            jogador.GetComponent<Skills>().alvoHeal = this.gameObject;
        }
    }

    void dieMethod()
    {
        GetComponent<Animation>().Play("Death");
        if(GetComponent<Animation>()["Death"].time > GetComponent<Animation>()["Death"].length * 0.9)
        {
            vida = 100;
            this.transform.position = new Vector3(ronda.transform.position.x, this.transform.position.y, ronda.transform.position.z);
        }
    }

    public bool morreu()
    {
        if(vida <= 0)
        {
            return true;
        }
        return false;
    }
}
