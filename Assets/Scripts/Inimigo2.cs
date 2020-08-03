using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo2 : MonoBehaviour
{

    public float vel, range, atkSpeed, rangeRun, rangeBattle;
    public double hitou;
    public Transform jogador;
    private bool hit;
    public int dano, vida;

    private Combate oponente;

    public CharacterController controller;

    public GameObject flecha, ronda;

    public GameObject raioSkill, cameraGO;

    [SerializeField]
    Slider sliderHealth;

    void Start()
    {
        oponente = jogador.GetComponent<Combate>();
        sliderHealth.maxValue = vida;
    }

    void Update()
    {
        if (!morreu())
        {
            if (!inRangeRun())
            {
                if (!inRangeBattle())
                {
                    Ronda();
                }
                else if (!inRange())
                    perserguir();
                else
                {
                    transform.LookAt(jogador.position);
                    GetComponent<Animation>().Play("attack");
                    GetComponent<Animation>()["attack"].speed = 1 + atkSpeed;
                    ataque();
                    if (GetComponent<Animation>()["attack"].time > 0.9 * GetComponent<Animation>()["attack"].length)
                    {
                        hit = false;
                    }
                }
            }
            else
                Run();
        }
        else
            dieMethod();

        sliderHealth.transform.LookAt(cameraGO.transform);
        sliderHealth.value = vida;
    }

    void Ronda()
    {
        Vector3 relativePos = ronda.transform.position - this.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(relativePos);
        newRotation.x = 0;
        newRotation.z = 0;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
        if (Vector3.Distance(ronda.transform.position, this.transform.position) > 1)
        {
            controller.SimpleMove(transform.forward * vel);
            GetComponent<Animation>().CrossFade("run");
        }
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

    void perserguir()
    {
        transform.LookAt(jogador.position);
        controller.SimpleMove(transform.forward * vel);
        GetComponent<Animation>().CrossFade("run");
    }

    void Run()
    {
            transform.LookAt(jogador.position * -1);
            controller.SimpleMove(transform.forward * vel);
            GetComponent<Animation>().CrossFade("run");
    }

    void ataque()
    {
        if (GetComponent<Animation>()["attack"].time > GetComponent<Animation>()["attack"].length * hitou && !hit && GetComponent<Animation>()["attack"].time < 0.9 * GetComponent<Animation>()["attack"].length)
        {
            oponente.getHit(dano);
            hit = true;
        }
    }

    bool inRangeRun()
    {
        if (Vector3.Distance(transform.position, jogador.position) < rangeRun)
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
        if (Vector3.Distance(transform.position, jogador.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void getHit(int dano)
    {
        vida -= dano;
        if (vida < 0)
            vida = 0;
        Debug.Log(vida);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jogador.GetComponent<Combate>().inimigo = this.gameObject;
        }
        if(jogador.GetComponent<Skills>().targetSkill3)
        {
            jogador.GetComponent<Skills>().alvoHeal = this.gameObject;
        }
    }

    void dieMethod()
    {
        GetComponent<Animation>().Play("die");
        if (GetComponent<Animation>()["die"].time > GetComponent<Animation>()["die"].length * 0.9)
        {
            vida = 100;
        }
    }

    public bool morreu()
    {
        if (vida <= 0)
        {
            return true;
        }
        return false;
    }
}