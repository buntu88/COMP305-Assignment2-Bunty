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
    private AudioSource[] _audioSources;
    private AudioSource _jumpSound;
    private AudioSource _coinSound;
    private AudioSource _hurtSound;



    public VelocityRange velocityRange;
    public float moveForce;
    public float jumpForce;
    public Transform groundCheck;
    public Transform camera;
    public GameController gameController;

    // Use this for initialization
    void Start()
    {
        //Intialize public variable
        this.velocityRange = new VelocityRange(300f, 30000f);
        this.moveForce = 1000f;
        this.jumpForce = 25000f;
        
        
        //Intialize private variable
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true;

        // Setup AudioSources
        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._jumpSound = this._audioSources[0];
        this._coinSound = this._audioSources[1];
        this._hurtSound = this._audioSources[2];

        // place the hero in the starting position
        this._spawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 curentPosition = new Vector3(this._transform.position.x + 250f, 0f, -10f);
        this.camera.position = curentPosition;
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

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            //this._coinSound.Play();
            Destroy(other.gameObject);
            this.gameController.ScoreValue += 10;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            this._spawn2();
            //this._hurtSound.Play();
            this.gameController.LivesValue--;
        }


        if (other.gameObject.CompareTag("Death"))
        {
            this._spawn();
            //this._hurtSound.Play();
            this.gameController.LivesValue--;
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

    private void _spawn()
    {
        this._transform.position = new Vector3(-250f, -198f, 0);
    }

    private void _spawn2()
    {
        this._transform.position = new Vector3(3340f, -198f, 0);
    }
}
