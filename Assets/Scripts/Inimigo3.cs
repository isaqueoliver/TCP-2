using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigo3 : MonoBehaviour {

    public float vel, range, hitou, atkSpeed, rangeRun;
    public int cura, vida;
    public bool hit;

    public Transform jogador;
    public GameObject aliado, cameraGO;

    public CharacterController controller;

    [SerializeField]
    Slider sliderHealth;

    void Start()
    {
        sliderHealth.maxValue = vida;
    }

    void Update()
    {
        if (!morreu())
        {
            if (!inRangeRun())
            {
                if (!inRange())
                {
                    perserguir();
                }
                else
                {
                    transform.LookAt(aliado.transform.position);
                    GetComponent<Animation>().Play("attack");
                    GetComponent<Animation>()["attack"].speed = 1 + atkSpeed;
                    curar();
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

    void perserguir()
    {
        if (aliado != null)
        {
            transform.LookAt(aliado.transform.position);
            controller.SimpleMove(transform.forward * vel);
            GetComponent<Animation>().CrossFade("run");
        }
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

    bool inRange()
    {
        if (aliado != null)
        {
            if (Vector3.Distance(this.transform.position, aliado.transform.position) < range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void curar()
    {
        if (GetComponent<Animation>()["attack"].time > GetComponent<Animation>()["attack"].length * hitou && !hit && GetComponent<Animation>()["attack"].time < 0.9 * GetComponent<Animation>()["attack"].length)
        {
            if (aliado.GetComponent<Inimigo>())
            {
                if (!aliado.GetComponent<Animation>().IsPlaying("die"))
                {
                    if (aliado.GetComponent<Inimigo>().vida < 100)
                        aliado.GetComponent<Inimigo>().vida += cura;
                    if (aliado.GetComponent<Inimigo>().vida > 100)
                        aliado.GetComponent<Inimigo>().vida = 100;
                }
            }
            if (aliado.GetComponent<Inimigo2>())
            {
                if (!aliado.GetComponent<Animation>().IsPlaying("die"))
                {
                    if (aliado.GetComponent<Inimigo2>().vida < 100)
                        aliado.GetComponent<Inimigo2>().vida += cura;
                    if (aliado.GetComponent<Inimigo2>().vida > 100)
                        aliado.GetComponent<Inimigo2>().vida = 100;
                }
            }
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

    bool inRangeRun()
    {
        if (Vector3.Distance(this.transform.position, jogador.position) < rangeRun)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Run()
    {
            transform.LookAt(jogador.position * -1);
            controller.SimpleMove(transform.forward * vel);
            GetComponent<Animation>().CrossFade("run");
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

    /*void OnTriggerEnter(Collider aliado)
    {
        if (aliado.gameObject.tag == "Enemy")
        {
            this.aliado = aliado.gameObject;
        }
    }*/
}
