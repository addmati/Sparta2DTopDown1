using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController contoller;
    private ObjectPool objectPool;
    private Vector2 aimDirection = Vector2.right;

    [SerializeField] private Transform projectileSpawnPosition;

    public GameObject testPrefab;

    private void Awake()
    {
        contoller = GetComponent<TopDownController>();
        objectPool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        contoller.OnAttackEvent += OnShoot;
        contoller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO RangedAttackSO = attackSO as RangedAttackSO;
        float projectilesAngleSpace = RangedAttackSO.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = RangedAttackSO.numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * RangedAttackSO.multipleProjectilesAngel;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-RangedAttackSO.spread, RangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(RangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO RangedAttackSO, float angle)
    {
       
        GameObject obj = objectPool.SpawnFromPool(RangedAttackSO.bulletNameTag);

      
        obj.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), RangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}