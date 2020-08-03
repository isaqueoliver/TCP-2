using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Combate : MonoBehaviour {

    public GameObject inimigo;
    //public GameObject fightEnemy;
    //private Inimigo matou;
    public AnimationClip ataque;
    public int dano;
    //em qual tmepo da animação vai hitar
    public double hitou;
    public bool hit;
    public float range;
    public int vida;

    public float atkSpeed;

    bool inicio, fim;

    public Skills raio;

    public Transform chao;

    [SerializeField]
    Button menu;
    [SerializeField]
    RawImage telaMorte;
	// Use this for initialization
	void Start () {
        menu.onClick.AddListener(irMenu);
        MovClick.die = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (inimigo != null)
        {
            if (!(inimigo.tag == "Health" || inimigo.tag == "Mana" || inimigo.tag == "Both"))
            {
                if (inRange())
                {
                    GetComponent<Animation>().Play("Ataque_1");
                    GetComponent<Animation>()["Ataque_1"].speed = 1.0f + atkSpeed;
                    MovClick.attack = true;
                    this.transform.LookAt(inimigo.transform.position);
                }
                else
                    IrAteInimigo();
                if (GetComponent<Animation>().IsPlaying("Ataque_1"))
                {
                    if (/*Input.GetMouseButtonDown(0)*/ /*this.GetComponent<MovClick>().teste &&*/ chao.GetComponent<ChaoClick>().clickChao)
                    {
                        if (!hit)
                        {
                            MovClick.attack = false;
                            inimigo = null;
                        }
                    }
                }
            }
        }
        else
            chao.GetComponent<ChaoClick>().clickChao = false;


        if ((GetComponent<Animation>()["Ataque_1"].time) > (GetComponent<Animation>()["Ataque_1"].length * 0.9))
        {
            //MovClick.attack = false;
            hit = false;
        }
        
        hitouInimigo();
        morreu();

        
	}

    void IrAteInimigo()
    {
        if (Vector3.Distance(transform.position, inimigo.transform.position) > range)
        {
            this.gameObject.GetComponent<MovClick>().posicao = inimigo.transform.position;
            MovClick.attack = false;
        }
    }

    void hitouInimigo()
    {
        if(inimigo != null && GetComponent<Animation>().IsPlaying("Ataque_1") && !hit)
        {
            if((GetComponent<Animation>()["Ataque_1"].time) > hitou && (GetComponent<Animation>()["Ataque_1"].time) < (GetComponent<Animation>()["Ataque_1"].length * 0.9) /*(GetComponent<Animation>()["attack"].length * hitou)*/)
            {
                if(inimigo.GetComponent<Inimigo>())
                    inimigo.GetComponent<Inimigo>().getHit(dano);
                else if (inimigo.GetComponent<Inimigo2>())
                    inimigo.GetComponent<Inimigo2>().getHit(dano);
                else if (inimigo.GetComponent<Inimigo3>())
                    inimigo.GetComponent<Inimigo3>().getHit(dano);
                hit = true;
            }
        }
    }

    public bool inRange()
    {
        if (Vector3.Distance(inimigo.transform.position, this.transform.position) <= range)
            return true;
        else
            return false;
    }

    public void getHit(int dano)
    {
        vida -= dano;
        if (vida < 0)
            vida = 0;
    }

    public bool isDead()
    {
        return (vida <= 0);
    }

    void irMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void morreu()
    {
        if(isDead() && !fim)
        {
            if (!inicio)
            {
                MovClick.die = true;
                GetComponent<Animation>().Play("Death");
                inicio = true;
            }

            if(inicio && !GetComponent<Animation>().IsPlaying("Death"))
            {
                /*Debug.Log("Você morreu!");
                vida = 100;*/
                telaMorte.gameObject.SetActive(true);

                fim = true;
                //inicio = false;
                //MovClick.die = false;
            }
        }
    }
}
