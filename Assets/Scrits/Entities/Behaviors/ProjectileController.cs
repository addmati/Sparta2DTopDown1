using UnityEngine;

public class ProjectileController : MonoBehaviour
{
   
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackSO attackData;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        rigidbody.velocity = direction * attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
           
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * .2f;
            DestroyProjectile(destroyPosition, fxOnDestory);
        }
     
        else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
           
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        UpdateProjectileSprite();
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = attackData.projectileColor;

        transform.right = this.direction;

        isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
           
        }
        gameObject.SetActive(false);
    }
}
