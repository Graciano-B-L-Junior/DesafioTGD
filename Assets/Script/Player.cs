using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    public bool podePular = false;
    bool colidiuParede = false;
    bool levouDano = false;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("Parado", true);
    }

    // Update is called once per frame
    void Update()
    {
        Pulo();
    }

    private void FixedUpdate()
    {
        movimentacao();
        Animations();
    }

    void movimentacao()
    {
        if (colidiuParede == false && levouDano == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rb.velocity = new Vector2(-3.5f, _rb.velocity.y);
                transform.localScale = new Vector2(-1.5f, transform.localScale.y);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rb.velocity = new Vector2(3.5f, _rb.velocity.y);
                transform.localScale = new Vector2(1.5f, transform.localScale.y);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) && podePular == true)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) && podePular == true)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
    }
    void Animations()
    {
        if (_rb.velocity.x > 2 || _rb.velocity.x < 0)
        {
            _animator.SetBool("Parado", false);
            _animator.SetBool("Andando", true);
        }
        if (_rb.velocity.x == 0)
        {
            _animator.SetBool("Parado", true);
            _animator.SetBool("Andando", false);
        }
        if (_rb.velocity.y < 0 && _animator.GetBool("Pulo") == true)
        {
            _animator.SetBool("Caida", true);
            _animator.SetBool("Pulo", false);
        }
    }
    void Pulo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && podePular == true && _rb.velocity.y < 1)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 12);
            podePular = false;
            _animator.SetBool("Pulo", true);
            _animator.SetBool("Parado", false);
            _animator.SetBool("Andando", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            podePular = true;
            colidiuParede = false;
            _animator.SetBool("Caida", false);
            _animator.SetBool("Parado", true);
        }
        if (other.gameObject.CompareTag("plataforma") && _rb.velocity.y < 1)
        {
            podePular = true;
            colidiuParede = false;
            _animator.SetBool("Caida", false);
            _animator.SetBool("Parado", true);
        }
        if (other.gameObject.CompareTag("parede"))
        {
            colidiuParede = true;
            _rb.velocity = new Vector2(0, 0);
        }
        if (other.gameObject.CompareTag("tiro"))
        {
            Vector3 direcao = other.gameObject.transform.position - transform.position;
            
            if (direcao.x > 0)
            {
                transform.localScale = new Vector2(1.5f, transform.localScale.y);
                print("Direita");
            }
            if (direcao.x < 0)
            {
                print("Esquerda");
                transform.localScale = new Vector2(-1.5f, transform.localScale.y);
            }
            levouDano = true;
            _animator.SetBool("Dano", true);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            podePular = false;
        }
        if (other.gameObject.CompareTag("plataforma"))
        {
            podePular = false;
        }
        if (other.gameObject.CompareTag("parede"))
        {
            colidiuParede = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("fruta"))
        {
            GameManager.quantidadeFrutas();
            Destroy(other.gameObject);
        }
    }
}
