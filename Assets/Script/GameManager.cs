using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Transform[] posicoes = new Transform[3];
    GameObject player;
    static int QuantidadeFrutas;
    static TextMeshProUGUI _texto;
    private void Awake()
    {
        player = GameObject.Find("Player");
        posicoes[0] = GameObject.Find("gold_3").GetComponent<Transform>();
        posicoes[1] = GameObject.Find("gold_2").GetComponent<Transform>();
        posicoes[2] = GameObject.Find("gold_1").GetComponent<Transform>();
        float x = Random.Range(posicoes[0].transform.position.x + 0.5f, posicoes[2].transform.position.x - 1);
        player.transform.position = new Vector2(x, -4.5f);
        QuantidadeFrutas = GameObject.FindGameObjectsWithTag("fruta").Length;
        _texto = GameObject.Find("Parabens").GetComponent<TextMeshProUGUI>();
        _texto.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(QuantidadeFrutas==0){
            _texto.enabled=true;
        }
    }

    public static void ReiniciarFase()
    {
        SceneManager.LoadScene(0);
    }
    public static void quantidadeFrutas()
    {
        QuantidadeFrutas--;
    }
}
