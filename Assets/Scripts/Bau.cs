using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour {

    public Transform jogador;

    [SerializeField]
    float rangeBau;

    public int quantidade;

    [SerializeField]
    int hpPot, mpPot, bothPot;

    void Update()
    {
        DarItemJogador();
        UsarItem();
    }

    void UsarItem()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(quantidade > 0)
            {
                if(this.gameObject.tag == "Health")
                {
                    jogador.GetComponent<Combate>().vida += hpPot;
                    if (jogador.GetComponent<Combate>().vida > 100)
                        jogador.GetComponent<Combate>().vida = 100;
                    this.quantidade--;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (quantidade > 0)
            {
                if (this.gameObject.tag == "Mana")
                {
                    jogador.GetComponent<Skills>().mana += mpPot;
                    this.quantidade--;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (quantidade > 0)
            {
                if (this.gameObject.tag == "Both")
                {
                    jogador.GetComponent<Combate>().vida += bothPot;
                    if (jogador.GetComponent<Combate>().vida > 100)
                        jogador.GetComponent<Combate>().vida = 100;
                    jogador.GetComponent<Skills>().mana += bothPot;
                    this.quantidade--;
                }
            }
        }
    }

    void DarItemJogador()
    {
        if (jogador.GetComponent<Combate>().inimigo != null)
        {
            if (jogador.GetComponent<Combate>().inimigo == this.gameObject)
            {
                if (inRangeJogador())
                {
                    quantidade++;
                    if (jogador.GetComponent<Combate>().inimigo == this.gameObject)
                    {
                        jogador.GetComponent<Combate>().inimigo = null;
                    }
                }
            }
        }
    }

    bool inRangeJogador()
    {
        return (Vector3.Distance(this.transform.position, jogador.position) < rangeBau);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jogador.GetComponent<Combate>().inimigo = this.gameObject;
        }
    }
}
