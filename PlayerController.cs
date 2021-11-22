using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;


    private bool isMoving;
    private Vector2 input;
    private Animator animator;


    private void Awake()
    {

        animator = GetComponent<Animator>();
    }
    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;
            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                    StartCoroutine(move(targetPos));

            }
        }

        animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }


    void Interact()
    {
        var faceingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + faceingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.black, 0.5f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<interactable>()?.Interact();
            if(collider.tag =="Boss")
            {
                Debug.Log("Trigger by enemy");
                collider.GetComponent<interactable>()?.Boss();
            }
        }
    }


    IEnumerator move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;

        
    }
    private bool IsWalkable(Vector3 targetPos)
    {

        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactableLayer ) != null)
        {
            return false;
        }

        return true;
    }
}
