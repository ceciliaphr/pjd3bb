using UnityEngine;

public class Inimigo : Personagem
{
    [SerializeField] private int dano = 1;
    
    public float raioDeVisao = 1;
    public CircleCollider2D _visaoCollider2D;

    [SerializeField] private Transform posicaoDoPlayer;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private AudioSource audioSource;
    
    private bool andando = false;
    private bool estaMorto = false; // Nova variável para controlar o estado de morte
    
    public void setDano(int dano)
    {
        this.dano = dano;
    }
    public int getDano()
    {
        return this.dano;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        
        if (posicaoDoPlayer == null)
        {
            posicaoDoPlayer =  GameObject.Find("Player").transform;
        }
        
        raioDeVisao = _visaoCollider2D.radius;
    }

    void Update()
    {
        // Se já está morto, não faz nada
        if (estaMorto) return;

        andando = false;

        if (getVida() > 0)
        {
            if (posicaoDoPlayer.position.x - transform.position.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            if (posicaoDoPlayer.position.x - transform.position.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            if (posicaoDoPlayer != null &&
                Vector3.Distance(posicaoDoPlayer.position, transform.position) <= raioDeVisao)
            {
                Debug.Log("Posição do Player" + posicaoDoPlayer.position);

                transform.position = Vector3.MoveTowards(transform.position,
                    posicaoDoPlayer.transform.position,
                    getVelocidade() * Time.deltaTime);

                andando = true;
            }
        }
        else if (getVida() <= 0 && !estaMorto)
        {
            // Só ativa a morte uma vez
            Morrer();
        }
        
        animator.SetBool("Andando", andando);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && getVida() > 0 && !estaMorto)
        {
            // Causa dano ao Player
            int novaVida = collision.gameObject.GetComponent<Personagem>().getVida() - getDano();
            collision.gameObject.GetComponent<Personagem>().setVida(novaVida);
            
            setVida(0);
        }
    }

    // Método separado para lidar com a morte
    private void Morrer()
    {
        estaMorto = true;
        animator.SetTrigger("Morte");
        
        // Desativa colisores para o inimigo não atrapalhar depois de morto
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        
        // Para de se mover
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    public void playAudio()
    {
        audioSource.Play();
    }

    // Método para ser chamado no final da animação de morte
    public void DestruirInimigo()
    {
        Destroy(gameObject);
    }

    public void desative()
    {
        Destroy(gameObject);
    }
}