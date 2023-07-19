using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spider : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;

    public BoxCollider2D upCol, downCol, leftCol, rightCol;

    public bool up, down, left, right;

    public ContactFilter2D contactFilter2D;
    
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    [SerializeField]
    private Direction direction;
    
    private float speed;

    private                 Animator animator;

    public void SetData(SpiderData data)
    {
        animator = this.GetComponent<Animator>();
        
        this.speed = Random.Range(data.speed - 1, data.speed + 1);

        this.animator.runtimeAnimatorController = data.animator;
        
        this.animator.Play(0);
        
        this.direction            = Direction.Up;
        this.up                   = true;
        this.down                 = true;
        this.left                 = true;
        this.right                = true;
    }
    
    private void Update()
    {
        this.CheckObstacle();
        
        switch (this.direction)
        {
            case Direction.Up:
                if (!this.up)
                {
                    this.ChangeDir(!this.right ? Direction.Left : Direction.Right);
                }
                break;
            case Direction.Down:
                if (!this.down)
                {
                    this.ChangeDir(!this.right ? Direction.Left : Direction.Right);
                }
                break;
            case Direction.Right:
                if (!this.right)
                {
                    this.ChangeDir(!this.up ? Direction.Down : Direction.Up);
                }
                break;
            case Direction.Left:
                if (!this.left)
                {
                    this.ChangeDir(!this.up ? Direction.Down : Direction.Up);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }    
        
        this.UpdateVeclocity();
    }

    private void ChangeDir(Direction dir)
    {
        this.direction = dir;
    }

    private void CheckObstacle()
    {
        up    = CheckDir(this.upCol);
        down  = CheckDir(this.downCol);
        right = CheckDir(this.rightCol);
        left  = CheckDir(this.leftCol);
    }
    
    private bool CheckDir(BoxCollider2D dirCol)
    {
        Collider2D[] result = new Collider2D[1];

        dirCol.OverlapCollider(this.contactFilter2D, result);
        
        if (result[0] != null)
        {
            return false;
        }

        return true;
    }

    private void UpdateVeclocity()
    {   
        Vector2 movingVec = this.rigidbody2D.velocity;
        
        switch (this.direction)
        {
            case  Direction.Up:
                if (movingVec != Vector2.up.normalized * this.speed)
                {
                    this.rigidbody2D.velocity = Vector2.up.normalized * this.speed;   
                    
                    this.animator.SetTrigger("Up");
                }
                break;
            case  Direction.Down:
                if (movingVec != Vector2.down.normalized * this.speed)
                {
                    this.rigidbody2D.velocity = Vector2.down.normalized * this.speed;
                    
                    this.animator.SetTrigger("Down");
                }
                break;
            case  Direction.Right:
                if (movingVec != Vector2.right.normalized * this.speed)
                {
                    this.rigidbody2D.velocity = Vector2.right.normalized * this.speed;   
                    
                    this.animator.SetTrigger("Right");
                }
                break;
            case  Direction.Left:
                if (movingVec != Vector2.left.normalized * this.speed)
                {
                    this.rigidbody2D.velocity = Vector2.left.normalized * this.speed;  
                    
                    this.animator.SetTrigger("Left");
                }
                break;
        }     
    }
    
}
