using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger");
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        var currentTransform = transform;
        var localScale = currentTransform.localScale;
        var xFacing = localScale.x * -1;
        localScale = new Vector2(xFacing, 1);

        currentTransform.localScale = localScale;

        StartCoroutine(PauseBeforeReturning());
    }

    private IEnumerator PauseBeforeReturning()
    {
        var startSpeed = moveSpeed;
        moveSpeed = 0;
        _animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(1.5f);
        moveSpeed = startSpeed;
        _animator.SetBool("isWalking", true);
    }
}
