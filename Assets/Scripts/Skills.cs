using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Combate jogador;

    public int mana, manaRaio, manaAtkSpeed, manaHeal;

    public ParticleSystem raio, cura;

    public GameObject raioCol, alvoHeal, rangeSkill, rangeRaio;

    GameObject healAtual;

    private Vector3 posicao;

    bool targetSkill1 = false;

    public bool targetSkill3 = false;

    public float duracaoRaio;
    public int danoRaio, curaJog, danoCura, regenMana;
    
    public float cdRaio, cdAtkSpeed, duracaoAS, cdHeal;

    float cdRaioInicial, cdAtkSpeedInicial, _duracaoAS, cdHealInicial;

    public bool usouRaio = false, usouAtkSpeed = false, usouHeal = false;

    [SerializeField]
    float atkSpeedAtv, atkSpeedPasv, duracaoHeal;

    float curaPosY, rangeRaioY;

    [SerializeField]
    Texture2D cursorHeal;

    List<ParticleCollisionEvent> collisionEvents;
	// Use this for initialization
	void Start ()
    {
        curaPosY = cura.transform.position.y;
        rangeRaioY = rangeRaio.transform.position.y;
        _duracaoAS = duracaoAS;
        cdRaioInicial = cdRaio;
        cdAtkSpeedInicial = cdAtkSpeed;
        cdHealInicial = cdHeal;
        raio.Stop(true);
        cura.Stop(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        RegenMana();
        UsouSkill();
        ReiniciarSkill();		
	}

    void RegenMana()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mana += regenMana;
        }
        if(mana >= 100)
        {
            mana = 100;
        }
    }

    public void ReiniciarSkill()
    {
        if(raio.time > duracaoRaio)
        {
            raio.Stop(true);
            raioCol.GetComponent<CapsuleCollider>().enabled = false;
        }
        if(cura.time > duracaoHeal)
        {
            cura.Stop(true);
        }
    }

    public void UsouSkill()
    {
        #region Skill Raio 1
        if (mana >= manaRaio)
        {
            if (!usouRaio)
            {
                if (Input.GetAxis("Skill") < 0)
                {
                    targetSkill3 = false;
                    targetSkill1 = true;
                    //rangeSkill.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            if (targetSkill1)
            {
                locatePosition();
                rangeRaio.GetComponent<SpriteRenderer>().enabled = true;
                rangeRaio.transform.position = new Vector3(posicao.x, rangeRaioY, posicao.z);
                if (Input.GetMouseButtonDown(1))
                {
                    raio.transform.position = posicao;
                    raioCol.GetComponent<CapsuleCollider>().enabled = true;
                    //rangeSkill.GetComponent<SpriteRenderer>().enabled = false;
                    rangeRaio.GetComponent<SpriteRenderer>().enabled = false;
                    raio.Play(true);
                    targetSkill1 = false;
                    mana -= manaRaio;
                    usouRaio = true;
                }
            }
        }
        if(usouRaio)
        {
            cdRaio -= Time.deltaTime;
            if (cdRaio <= 0)
            {
                cdRaio = cdRaioInicial;
                usouRaio = false;
            }
        }
        #endregion

        #region Skill Ataque Speed 2
        if (mana >= manaAtkSpeed)
        {
            if (!usouAtkSpeed)
            {
                duracaoAS = _duracaoAS;
                if (Input.GetAxis("Skill") > 0)
                {
                    jogador.atkSpeed = atkSpeedAtv;
                    mana -= manaAtkSpeed;
                    usouAtkSpeed = true;
                }
                else
                    jogador.atkSpeed = atkSpeedPasv;
            }
        }
        if(usouAtkSpeed)
        {
            cdAtkSpeed -= Time.deltaTime;
            duracaoAS -= Time.deltaTime;
            if(duracaoAS <= 0)
            {
                jogador.atkSpeed = 0;
            }
            if(cdAtkSpeed <= 0)
            {
                cdAtkSpeed = cdAtkSpeedInicial;
                usouAtkSpeed = false;
            }
        }
        #endregion

        #region Skill Cura 3
        if (mana >= manaHeal)
        {
            if (!usouHeal)
            {
                if (Input.GetAxis("Skill2") < 0)
                {
                    targetSkill1 = false;
                    targetSkill3 = true;
                }
            }
            if (targetSkill3)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Cursor.SetCursor(cursorHeal, Vector2.zero, CursorMode.Auto);
                    if (alvoHeal != null)
                    {
                        healAtual = alvoHeal;
                        cura.transform.position = healAtual.transform.position + new Vector3(0, curaPosY, 0);
                        cura.Play(true);
                        
                        if (healAtual.tag == "Player")
                        {
                            healAtual.GetComponent<Combate>().vida += curaJog;
                        }
                        else
                        {
                            if (healAtual.GetComponent<Inimigo>())
                            {
                                healAtual.GetComponent<Inimigo>().vida -= danoCura;
                            }
                            else if (healAtual.GetComponent<Inimigo2>())
                            {
                                healAtual.GetComponent<Inimigo2>().vida -= danoCura;
                            }
                            else
                            {
                                healAtual.GetComponent<Inimigo3>().vida -= danoCura;
                            }
                        }
                        usouHeal = true;
                        mana -= manaHeal;
                        targetSkill3 = false;
                    }
                }
            }
        }
        if(healAtual != null)
        {
            cura.transform.position = healAtual.transform.position + new Vector3(0, curaPosY, 0);
            if (healAtual == jogador)
                healAtual.GetComponent<Combate>().vida += curaJog;
        }
        if(usouHeal)
        {
            cdHeal -= Time.deltaTime;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            if (cdHeal <= 0)
            {
                cdHeal = cdHealInicial;
                usouHeal = false;
            }
        }
        if(cura.isStopped)
        {
            alvoHeal = null;
            healAtual = null;
        }
        #endregion

        if(Input.GetMouseButtonDown(0))
        {
            targetSkill1 = false;
            targetSkill3 = false;
            rangeRaio.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public void locatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 2000))
        {
            //if (hit.collider.tag != "Player" && (hit.collider.tag != "Objeto"))
            //{
                posicao = new Vector3(hit.point.x, raio.transform.position.y, hit.point.z);
            //}
        }
    }

    void OnMouseOver()
    {
        if (targetSkill3)
        {
            alvoHeal = this.gameObject;
        }
    }
}
