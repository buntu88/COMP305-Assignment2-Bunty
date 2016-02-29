using UnityEngine;
using System.Collections;



public class enemyController : MonoBehaviour {

    private Animator _animator;
    private bool _isEnemy;
    private AudioSource[] _audioSources;
    private AudioSource _jumpSound;
    private AudioSource _coinSound;
    private AudioSource _hurtSound;
    private Transform _transform;



    public Transform enemyCheck;


    // Use this for initialization
    void Start()
    {



        //Intialize private variable
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();


        // Setup AudioSources
        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._jumpSound = this._audioSources[0];
        this._coinSound = this._audioSources[1];
        this._hurtSound = this._audioSources[2];

        // place the hero in the starting position
       
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
