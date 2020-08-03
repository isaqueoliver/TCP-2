using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {
    [SerializeField]
    Slider vidaSlider, manaSlider, raioSlider, atkSpeedSlider, healSlider;
    [SerializeField]
    Text timeCD, timeCD2, timeCD3, raioMenu, asMenu, healMenu, raioMana, raioCD, raioDano, asMana, asCD, asDuracao, healMana, healCD, healDano, healCura, vidaMax, vidaAtual, manaMax, manaAtual, danoJog, atkspdJog, pocaoHP, pocaoMP, pocaoBoth;

    public GameObject jogador, bauHP, bauMP, bauBoth;

    [SerializeField]
    Button voltar, irMenu, sair, raioButton, asButton, healButton;
    [SerializeField]
    RawImage escMenu, morteMenu, skillMenu, statusMenu;

	// Use this for initialization
	void Start () {
        vidaSlider.maxValue = jogador.GetComponent<Combate>().vida;
        manaSlider.maxValue = jogador.GetComponent<Skills>().mana;
        raioSlider.maxValue = jogador.GetComponent<Skills>().cdRaio;
        atkSpeedSlider.maxValue = jogador.GetComponent<Skills>().cdAtkSpeed;
        healSlider.maxValue = jogador.GetComponent<Skills>().cdHeal;
        raioSlider.value = 0;
        atkSpeedSlider.value = 0;
        healSlider.value = 0;

        voltar.onClick.AddListener(Voltar);
        irMenu.onClick.AddListener(Menu);
        sair.onClick.AddListener(SairJogo);

        raioButton.onClick.AddListener(RaioMenu);
        asButton.onClick.AddListener(AtkSpd_Menu);
        healButton.onClick.AddListener(HealMenu);

        raioCD.text = jogador.GetComponent<Skills>().cdRaio.ToString();
        raioMana.text = jogador.GetComponent<Skills>().manaRaio.ToString();
        raioDano.text = jogador.GetComponent<Skills>().danoRaio.ToString();

        asCD.text = jogador.GetComponent<Skills>().cdAtkSpeed.ToString();
        asMana.text = jogador.GetComponent<Skills>().manaAtkSpeed.ToString();
        asDuracao.text = jogador.GetComponent<Skills>().duracaoAS.ToString();

        healCD.text = jogador.GetComponent<Skills>().cdHeal.ToString();
        healMana.text = jogador.GetComponent<Skills>().manaHeal.ToString();
        healDano.text = jogador.GetComponent<Skills>().danoCura.ToString();
        healCura.text = jogador.GetComponent<Skills>().curaJog.ToString();

        vidaMax.text = jogador.GetComponent<Combate>().vida.ToString();
        manaMax.text = jogador.GetComponent<Skills>().mana.ToString();
        danoJog.text = jogador.GetComponent<Combate>().dano.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        vidaSlider.value = jogador.GetComponent<Combate>().vida;
        manaSlider.value = jogador.GetComponent<Skills>().mana;

        atkspdJog.text = (1 + jogador.GetComponent<Combate>().atkSpeed).ToString();
        vidaAtual.text = jogador.GetComponent<Combate>().vida.ToString();
        manaAtual.text = jogador.GetComponent<Skills>().mana.ToString();

        pocaoHP.text = bauHP.GetComponent<Bau>().quantidade.ToString();
        pocaoMP.text = bauMP.GetComponent<Bau>().quantidade.ToString();
        pocaoBoth.text = bauBoth.GetComponent<Bau>().quantidade.ToString();

        if (jogador.GetComponent<Skills>().usouRaio)
        {
            raioSlider.value = jogador.GetComponent<Skills>().cdRaio;
            timeCD.gameObject.SetActive(true);
            timeCD.text = ((int)jogador.GetComponent<Skills>().cdRaio + 1).ToString();
        }
        else
        {
            raioSlider.value = 0;
            timeCD.gameObject.SetActive(false);
            timeCD.text = ((int)jogador.GetComponent<Skills>().cdRaio).ToString();
        }
        if(jogador.GetComponent<Skills>().usouAtkSpeed)
        {
            atkSpeedSlider.value = jogador.GetComponent<Skills>().cdAtkSpeed;
            timeCD2.gameObject.SetActive(true);
            timeCD2.text = ((int)jogador.GetComponent<Skills>().cdAtkSpeed + 1).ToString();
        }
        else
        {
            atkSpeedSlider.value = 0;
            timeCD2.gameObject.SetActive(false);
        }
        if (jogador.GetComponent<Skills>().usouHeal)
        {
            healSlider.value = jogador.GetComponent<Skills>().cdHeal;
            timeCD3.gameObject.SetActive(true);
            timeCD3.text = ((int)jogador.GetComponent<Skills>().cdHeal + 1).ToString();
        }
        else
        {
            healSlider.value = 0;
            timeCD3.gameObject.SetActive(false);
        }

        if (!morteMenu.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (escMenu.IsActive())
                {
                    escMenu.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    MovClick.attack = false;
                }
                else
                {
                    escMenu.gameObject.SetActive(true);
                    skillMenu.gameObject.SetActive(false);
                    statusMenu.gameObject.SetActive(false);
                    Time.timeScale = 0;
                    MovClick.attack = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                if(skillMenu.IsActive())
                {
                    skillMenu.gameObject.SetActive(false);
                    MovClick.attack = false;
                }
                else
                {
                    skillMenu.gameObject.SetActive(true);
                    MovClick.attack = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (statusMenu.IsActive())
                {
                    statusMenu.gameObject.SetActive(false);
                    MovClick.attack = false;
                }
                else
                {
                    statusMenu.gameObject.SetActive(true);
                    MovClick.attack = true;
                }
            }
        }
        else
        {
            if(escMenu.IsActive())
            {
                escMenu.gameObject.SetActive(false);
            }
            if(skillMenu.IsActive())
            {
                skillMenu.gameObject.SetActive(false);
            }
            if(statusMenu.IsActive())
            {
                statusMenu.gameObject.SetActive(false);
            }
        }
    }

    void Voltar()
    {
        escMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        MovClick.attack = false;
    }
    void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    void SairJogo()
    {
        Application.Quit();
    }
    void RaioMenu()
    {
        if(asMenu.IsActive())
        {
            asMenu.gameObject.SetActive(false);
        }
        else if (healMenu.IsActive())
        {
            healMenu.gameObject.SetActive(false);
        }

        raioMenu.gameObject.SetActive(true);
    }
    void AtkSpd_Menu()
    {
        if (raioMenu.IsActive())
        {
            raioMenu.gameObject.SetActive(false);
        }
        else if (healMenu.IsActive())
        {
            healMenu.gameObject.SetActive(false);
        }

        asMenu.gameObject.SetActive(true);
    }
    void HealMenu()
    {
        if (asMenu.IsActive())
        {
            asMenu.gameObject.SetActive(false);
        }
        else if (raioMenu.IsActive())
        {
            raioMenu.gameObject.SetActive(false);
        }

        healMenu.gameObject.SetActive(true);
    }
}
