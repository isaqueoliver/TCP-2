using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
    [SerializeField]
    Button novoJogo, sair;

	// Use this for initialization
	void Start () {
        novoJogo.onClick.AddListener(NewGame);
        sair.onClick.AddListener(SairJogo);
	}
	
	void NewGame()
    {
        SceneManager.LoadScene("aprendendo 1");
    }

    void SairJogo()
    {
        Application.Quit();
    }
}
