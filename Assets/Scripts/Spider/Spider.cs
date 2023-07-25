using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spider : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;

    public BoxCollider2D upCol, downCol, leftCol, rightCol;

    public bool up, down, left, right;

    public ContactFilter2D contactFilter2D;

    private Direction priorityDirection;
    
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
    
    private void FixedUpdate()
    {
        this.CheckObstacle();
        
        switch (this.direction)
        {
            case Direction.Up:
                if (!this.up)
                {
                    this.rigidbody2D.velocity = Vector2.zero;
                    
                    if (this.left && this.right)
                    {
                        if (this.priorityDirection is Direction.Left)
                        {
                            ChangeDir(this.priorityDirection);
                        }
                        else
                        {
                            ChangeDir(Direction.Right);     
                        }
                    }
                    else
                    {
                        if (!this.left)
                        {
                            ChangeDir(Direction.Right);
                            if (!this.right)
                            {
                                ChangeDir(Direction.Down);      
                            }
                        }
                        else if(!this.right)
                        {
                            ChangeDir(Direction.Left);      
                        }
                    }
                }
                break;
            case Direction.Down:
                if (!this.down)
                {
                    this.rigidbody2D.velocity = Vector2.zero;
                    
                    if (this.left && this.right)
                    {
                        if (this.priorityDirection is Direction.Left)
                        {
                            ChangeDir(this.priorityDirection);
                        }
                        else
                        {
                            ChangeDir(Direction.Right);     
                        }
                    }
                    else
                    {
                        if (!this.left)
                        {
                            ChangeDir(Direction.Right);
                            
                            if (!this.right)
                            {
                                ChangeDir(Direction.Up);      
                            }
                        }
                        else
                        {
                            ChangeDir(Direction.Left);      
                        }
                    }
                }
                break;
            case Direction.Right:
                if (!this.right)
                {
                    this.rigidbody2D.velocity = Vector2.zero;
                    
                    if (this.up && this.down)
                    {
                        if (this.priorityDirection is Direction.Down)
                        {
                            ChangeDir(this.priorityDirection);
                        }
                        else
                        {
                            ChangeDir(Direction.Up);
                        }
                    }
                    else
                    {
                        if (!this.up)
                        {
                            ChangeDir(Direction.Down);  
                            
                            if(!this.down)
                            {
                                ChangeDir(Direction.Left);     
                            }
                        }
                        else 
                        {
                            ChangeDir(Direction.Up);      
                        }
                    }
                }
                break;
            case Direction.Left:
                if (!this.left)
                {
                    this.rigidbody2D.velocity = Vector2.zero;
                    
                    if (this.up && this.down)
                    {
                        if (this.priorityDirection is Direction.Down)
                        {
                            ChangeDir(this.priorityDirection);
                        }
                        else
                        {
                            ChangeDir(Direction.Up);
                        }
                    }
                    else
                    {
                        if (!this.up)
                        {
                            ChangeDir(Direction.Down);

                            if (!this.down)
                            {
                                ChangeDir(Direction.Right);   
                            }
                        }
                        else 
                        {
                            ChangeDir(Direction.Up);      
                        }
                    }
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
        switch (this.direction)
        {
            case Direction.Up:
            case Direction.Down:
                if (!this.right && this.CheckDir(this.rightCol))
                {
                    this.direction = Direction.Right;
                }
                else if (!this.left&&CheckDir(this.leftCol))
                {
                    this.priorityDirection = Direction.Left; 
                }
                break;
            case Direction.Right:
            case Direction.Left:
                if (!this.up && this.CheckDir(this.upCol))
                {
                    this.direction = Direction.Up;
                }
                else if (!this.down&&CheckDir(this.downCol))
                {
                    this.priorityDirection = Direction.Down; 
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }    
        
        this.up    = this.CheckDir(this.upCol);
        this.down  = this.CheckDir(this.downCol);
        this.right = this.CheckDir(this.rightCol);
        this.left  = this.CheckDir(this.leftCol);
    }
    
    private bool CheckDir(BoxCollider2D dirCol)
    {
        var result = new List<Collider2D>();

        dirCol.OverlapCollider(this.contactFilter2D, result);
        
        if (result.Count != 0)
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
