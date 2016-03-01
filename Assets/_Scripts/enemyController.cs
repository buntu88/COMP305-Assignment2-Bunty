using UnityEngine;
using System.Collections;

//Last Modified by      Vishal Guleria
//Date last Modified    February 29,2016
//Program description   COMP305 - Assignment 2 - Bunty; 2D Platform game.

public class enemyController : MonoBehaviour
{

    private Animator _animator;
    private bool _isEnemy;
    private Transform _transform;


    public Transform enemyCheck;


    // Use this for initialization
    void Start()
    {



        //Intialize private variable
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        this._isEnemy = Physics2D.Linecast(this._transform.position, this.enemyCheck.position, 1 << LayerMask.NameToLayer("Enemy"));
        Debug.DrawLine(this._transform.position, this.enemyCheck.position);



        if (this._isEnemy)
        {
            this._animator.SetInteger("DragonState", 1);
        }

    }
}
