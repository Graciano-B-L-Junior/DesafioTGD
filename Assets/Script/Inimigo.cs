using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private Animator _animator;
    private bool InimigoMiraDireita;
    [SerializeField]
    private GameObject tiro;
    [SerializeField]
    private Transform posicaoTiro;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (transform.localScale.x < 0)
        {
            InimigoMiraDireita = true;
        }
        else
        {
            InimigoMiraDireita = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    void Animacaoes()
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("Parado", true);
            _animator.SetBool("ataque", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("Parado", false);
            _animator.SetBool("ataque", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D _rbPlayer = other.gameObject.GetComponent<Rigidbody2D>();
            if (_rbPlayer.velocity.y < 0)
            {
                _rbPlayer.velocity = new Vector2(_rbPlayer.velocity.x, 6);
                _animator.SetBool("Parado", false);
                _animator.SetBool("dano", true);
                Destroy(this.gameObject, 0.4f);
            }

        }
    }

    public void dispararTiro()
    {
        var ObjetoTiro = Instantiate(tiro, posicaoTiro.position, Quaternion.identity);
        if (InimigoMiraDireita)
        {
            ObjetoTiro.transform.localScale = new Vector2(-ObjetoTiro.transform.localScale.x, ObjetoTiro.transform.localScale.y);
            ObjetoTiro.GetComponent<TiroInimigo>().velocidadeX = 15;
        }
        else
        {
            ObjetoTiro.transform.localScale = new Vector2(ObjetoTiro.transform.localScale.x, ObjetoTiro.transform.localScale.y);
            ObjetoTiro.GetComponent<TiroInimigo>().velocidadeX = -15;
        }
    }
}
