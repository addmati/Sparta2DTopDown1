using UnityEngine;

// TopDownMovement는 캐릭터와 몬스터의 이동에 사용될 예정입니다.
public class TopDownMovement : MonoBehaviour
{
    private TopDownController movementController;
    private Rigidbody2D movementRigidbody;
    private CharacterStatHandler characterStatHandler;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        
        movementController = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
       
        movementController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
       
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        
        movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed;

        movementRigidbody.velocity = direction;
    }
}