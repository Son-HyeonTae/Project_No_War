using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // car LineState line
    private Vector2[] line = new Vector2[4];

    // 현재 자동차가 위치에 있는 라인 인덱스
    private int lineIndex;
    // 자동차가 움직일 위치
    private Vector2 movePos;
    // car 움직이는 속도
    [SerializeField] private float carVelocity = 5f;

    // 목숨 3개
    [HideInInspector] public static int life = 3;

    private SpriteRenderer carRenderer;

    [HideInInspector] public bool rightMove;
    [HideInInspector] public bool leftMove;

    public enum State 
    {
        Normal,
        Wounded
    }

    [HideInInspector] public State carState = State.Normal;
    
    public float test = 3f;

    private float lastMoveTime = 0f;

    private AudioSource carAudio;
    private Animator animator;
    [SerializeField] private Sprite[] lineSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //배열 line 초기화
        line[0] = new Vector2( -3.7f, -4.3f );
        line[1] = new Vector2( -1.3f, -4.3f );
        line[2] = new Vector2( 1.05f, -4.3f );
        line[3] = new Vector2( 3.4f, -4.3f );

        lineIndex = 2; // line 3
        movePos = line[lineIndex];
        // 필요한 컴포넌트 가져오기
        carRenderer = GetComponent<SpriteRenderer>();

        Vector2 o = transform.localPosition;

        // AudioSource
        // carAudio = GetComponent<AudioSource>();
        // carAudio.enabled = false;
        // Animator
        animator = GetComponent<Animator>();
        // sprite Renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GMScene5.isStart = true;
        }
        if(!GMScene5.isGameover && GMScene5.isStart)
        {
            UpdateMove();
            float dis = Time.time - lastMoveTime;
            // 속도 조절.
            if(dis < 0.1f)
            {
                carVelocity = 4f;
            }
            else if(dis < 0.3f)
            {
                carVelocity = 5f;
            }
            else
            {
                carVelocity = 3f;
            }
            transform.position = Vector2.MoveTowards(transform.position, movePos, carVelocity * Time.deltaTime);
        }

    }

    private void UpdateMove()
    {
        rightMove = Input.GetKeyDown(KeyCode.RightArrow);
        leftMove = Input.GetKeyDown(KeyCode.LeftArrow);
        //move left
        if(leftMove && lineIndex != 0)
        {
            lineIndex += -1;
            lastMoveTime = Time.time;
        } // move right
        if(rightMove && lineIndex != 3)
        {
            lineIndex += 1;
            lastMoveTime = Time.time;
        }
        movePos = line[lineIndex];
        // changeAnim(lineIndex + 1);
        changeSprite(lineIndex);
    
    }

    // 현재 lineIndex에 따라 animation을 변경
    // private void changeAnim(int line)
    // {
    //     animator.SetInteger("Line", line);
    //     Debug.Log(line);
        
    // }
    
    // sprite 변경
    private void changeSprite(int line)
    {
        spriteRenderer.sprite = lineSprite[line];
    }

    // 장애물과 충돌시
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌함수 호출");
        // 장애물과 부딪히면
        if(other.tag == "Obstacle" && carState == State.Normal)
        {
            life--;
            GMScene5.instance.BreakHeart(life);
            GMScene5.instance.Shake();
            if(life <= 0)
            {
                GMScene5.isGameover = true;
            }                
            else
            {
                StartCoroutine(DamageView());
            }
        }

    }

    // BulletSpawner에서 호출.
    public int Damage()
    {

        if (carState == State.Normal)
        {
            life--;
            if(life <= 0)
            {
                GMScene5.isGameover = true;
            }
            else
            {
                GMScene5.instance.Shake();
                GMScene5.instance.BreakHeart(life);
                StartCoroutine(DamageView());
            }
            
        }
        
        return life;

    }

    // 투명화 처리.
    private IEnumerator DamageView()
    {
        carState = State.Wounded;
        Color carColor = carRenderer.color;
        // carAudio.enabled = true;
        for(int i = 0; i < 3; i++)
        {
            // 0 is transparent(투명한), 1 is opaque(불투명한)
            carColor.a -= 0.8f;
            carRenderer.color = carColor;
            yield return new WaitForSeconds(0.4f);
            carColor.a += 0.8f;
            carRenderer.color = carColor;
            yield return new WaitForSeconds(0.4f);
        }
        // carAudio.enabled = false;
        carState = State.Normal;
    }
}
