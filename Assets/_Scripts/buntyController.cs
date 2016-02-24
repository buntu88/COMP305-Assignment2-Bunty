using UnityEngine;
using System.Collections;


//VELOCITYRANGE UTILITY CLASS

    [System.Serializable]
public class VelocityRange
{
    // PUBLIC INSTANCE VARIABLES
    public float minimum;
    public float maximum;

    public VelocityRange (float min, float max)
    {
        this.minimum = min;
        this.maximum = max;
    }
}

public class buntyController : MonoBehaviour {

    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _facingRight;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;


    public VelocityRange velocityRange;
    public float moveForce;
    public float jumpForce;
    public Transform groundCheck;

    // Use this for initialization
    void Start()
    {
        //Intialize public variable
        this.velocityRange = new VelocityRange(300f, 30000f);
        this.moveForce = 950f;
        this.jumpForce = 25000f;
        
        
        //Intialize private variable
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this._isGrounded = Physics2D.Linecast(this._transform.position, this.groundCheck.position, 1<<LayerMask.NameToLayer("ground"));
        Debug.DrawLine(this._transform.position, this.groundCheck.position);


        float forceX = 0f;
        float forceY = 0f;

        float abVelX = Mathf.Abs(this._rigidbody2D.velocity.x);
        float abVelY = Mathf.Abs(this._rigidbody2D.velocity.y);

        //Get a number between -1 to 1 for both Horizontal and Vertical



        if (this._isGrounded)
        {

            this._move = Input.GetAxis("Horizontal");
            this._jump = Input.GetAxis("Vertical");


            if (this._move != 0)
            {
                if (this._move > 0)
                {
                    if (abVelX < this.velocityRange.maximum)
                    {
                        forceX = this.moveForce;
                    }
                    this._facingRight = true;
                    this._flip();
                }
                if (this._move < 0)
                {
                    if (abVelX < this.velocityRange.maximum)
                    {
                        forceX = -this.moveForce;
                    }
                    this._facingRight = false;
                    this._flip();
                }

                // call the walk clip
                this._animator.SetInteger("AnimState", 1);
                if (this._jump > 0)
                {
                    if (abVelY < this.velocityRange.maximum)
                    {
                        forceY = this.jumpForce;
                    }
                    // call the "jump" clip
                    this._animator.SetInteger("AnimState", 2);

                }
                this._rigidbody2D.AddForce(new Vector2(forceX, forceY));
                Debug.Log(forceX);
            }
            else {


                // set default animation state to "idle"
                this._animator.SetInteger("AnimState", 0);
            }

        }
        else
        {
            
        }




        


    }

    // PRIVATE METHODS
    private void _flip()
    {
        if (this._facingRight)
        {
            this._transform.localScale = new Vector2(1, 1);
        }
        else {
            this._transform.localScale = new Vector2(-1, 1);
        }
    }
}
