using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    PlayerControls controls;
    float direction = 0;
    public float speed = 400;
    private bool isFacingRight = true;
    public float upForce = 6;
    private int numberOfJumps = 0;

    public Rigidbody2D playerRB;
    public Animator animator;
    private int tong = 0;
    private bool nen;
    public Text diemText;
    public GameObject panels, buttons, texts;
    public AudioSource audio_src_vang;
    public AudioSource audio_src_play;
    public AudioSource audio_src_die;
    
    void tinhTong(int score)
    {
        tong += score;
        diemText.text = " " + tong;
    }
    void Start()
    {
        
        tinhTong(0);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "vatcan")
        {
            nen = true;
        }

        if (collision.gameObject.tag == "vatcantrai")
        {
            
            audio_src_die.Play();   
            // // gameover, replace màn chơi
            Time.timeScale = 0; // dừng scene lại
            panels.SetActive(true); // show panel
            buttons.SetActive(true); // show button
            //   texts.SetActive(true); // show text
            diemText.text = " " + tong; // Hiển thị điểm hiện tại

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "vatcanxu")
        {
            audio_src_vang.Play();
            var name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
            tinhTong(1);
        }
    }
    
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };
        controls.Land.Up.performed +=  ctx=> Up();


    }

  

    // Update is called once per frame
    void FixedUpdate()
    {
        
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(direction));

        if (isFacingRight && direction < 0 || !isFacingRight && direction >0)
            Flip();
        
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Up()
    {
        
        playerRB.velocity = new Vector2(playerRB.velocity.x, upForce);
        numberOfJumps++;
    }
    
    
}
