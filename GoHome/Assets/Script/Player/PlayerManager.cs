using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

/*
 *  玩家管理类
 *  by mingkai.zheng
 */
public enum playerStatus
{
    Run = 0,
    JumpStart = 1,
    JumpEnd =  2,
    JumpAuto = 3,
    SrounchStart = 4,
    SrounchEnd = 5,
    Idle = 6,
}

public enum movingDirection
{
    None = 0,
    Left = 1,
    Right = 2,
}
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float _firstBlood = 100.0f;
    [SerializeField]
    private float _jumpForce = 1.0f;
    [SerializeField]
    private float _jumpDownForce = 1.0f;
    [SerializeField]
    private float _jumpMovingForce = 1.0f;
    [SerializeField]
    private float _MovingSpeed = 1.0f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Dictionary<int, int> _animatorHashIdList;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    private PlayerData _playerData;

    private movingDirection _movingSatus = movingDirection.None;
    private bool _jumping = false;
    private bool _scrunching = false;

    void Awake()
    {
        _playerData = new PlayerData(_firstBlood);
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _animatorHashIdList = new Dictionary<int, int>();
        _animatorHashIdList.Add((int) playerStatus.Idle, Animator.StringToHash("Idle"));
        _animatorHashIdList.Add((int) playerStatus.JumpStart, Animator.StringToHash("JumpStart"));
        _animatorHashIdList.Add((int) playerStatus.JumpEnd, Animator.StringToHash("JumpEnd"));
        _animatorHashIdList.Add((int) playerStatus.JumpAuto, Animator.StringToHash("JumpAuto"));
        _animatorHashIdList.Add((int) playerStatus.SrounchStart, Animator.StringToHash("SrounchStart"));
        _animatorHashIdList.Add((int) playerStatus.SrounchEnd, Animator.StringToHash("SrounchEnd"));
        _animatorHashIdList.Add((int) playerStatus.Run, Animator.StringToHash("Run"));
    }

    void Update()
    {
        //controller 
        UpdateControllerListen();
    }

    #region Controller

    private void UpdateControllerListen()
    {
        //跳 + 
        if (_jumping)
        {
            //停止跑动
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _movingSatus = movingDirection.None;
                SetupMoving(false);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _movingSatus = movingDirection.None;
                SetupMoving(false);
            }
            return;
        }

        //蹲 + 
        if (_scrunching)
        {
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl) &&
                !Input.GetKey(KeyCode.DownArrow))
            {
                _scrunching = false;
                SetupScrunchEnd();
                //不蹲
            }
            else
            {
                _scrunching = true;
                //保持蹲
                return;
            }
        }
        else//普通蹲
        {
            if (Input.GetKey(KeyCode.LeftControl) ||
                Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.DownArrow))
            {
                _scrunching = true;
                SetupScrunchStart();
                //开始蹲
                return;
            }
        }

        //走 + 跳/蹲
        if (_movingSatus != movingDirection.None)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _jumping = true;
                SetupJumpStart(playerStatus.JumpStart);
                //开始斜跳
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) ||  Input.GetKeyDown(KeyCode.DownArrow))
            {
                _scrunching = true;
                SetupScrunchStart();
                //开始蹲
                return;
            }
        }


        //普通跳
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _jumping = true;
            SetupJumpStart(playerStatus.JumpAuto);
            //普通跳
            return;
        }

        //普通走
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movingSatus = movingDirection.Left;
            SetupMoving(true);
            //向左跑
        }else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _movingSatus = movingDirection.None;
            SetupMoving(false);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            _movingSatus = movingDirection.Right;
            SetupMoving(true);
            //向右跑
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _movingSatus = movingDirection.None;
            SetupMoving(false);
        }
        return;
    }

    private void SetAnimatorTrigger(playerStatus playerStatus)
    {
        _animator.SetTrigger(_animatorHashIdList[(int)playerStatus]);
    }

    private void SetAnimatorBool(playerStatus playerStatus, bool active)
    {
        _animator.SetBool(_animatorHashIdList[(int) playerStatus], active);
    }

    private void SetupJumpStart(playerStatus jumpStatus)
    {

        if (_movingSatus == movingDirection.Left)
        {
            //SetAnimatorBool(playerStatus.Run, false);
            _rigidbody2D.AddForce(-transform.right * _jumpMovingForce);
        }
        else if (_movingSatus == movingDirection.Right)
        {
            //SetAnimatorBool(playerStatus.Run, false);
            _rigidbody2D.AddForce(transform.right * _jumpMovingForce);
        }
            _rigidbody2D.AddForce(transform.up * _jumpForce);
        SetAnimatorTrigger(jumpStatus);

    }

    private void SetupJumpEnd()
    {
        if (_jumping)
        {
            _jumping = false;
            SetAnimatorTrigger(playerStatus.JumpEnd);
        }
    }

    private void SetupMoving(bool active)
    {
        SetAnimatorBool(playerStatus.Run, active);

        if (active)
        {
            if (_movingSatus == movingDirection.Left)
            {
                _spriteRenderer.flipX = true;
                _rigidbody2D.MovePosition(_rigidbody2D.position + _MovingSpeed * Vector2.left);
            }
            else if (_movingSatus == movingDirection.Right)
            {
                _spriteRenderer.flipX = false;
                _rigidbody2D.MovePosition(_rigidbody2D.position + _MovingSpeed * Vector2.right);
            }
        }
    }

    private void SetupScrunchStart()
    {
        if (_movingSatus != movingDirection.None)
            SetAnimatorBool(playerStatus.Run, false);
        SetAnimatorTrigger(playerStatus.SrounchStart);
    }

    private void SetupScrunchEnd()
    {
        _scrunching = false;
        SetAnimatorTrigger(playerStatus.SrounchEnd);
    }

    #endregion

    #region Collider

    void OnCollisionEnter2D(Collision2D Collision2D)
    {
        //接触到地板
        if (Collision2D.transform.tag == "floor")
        {
            SetupJumpEnd();
            return;
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        //接触到门

        //接触到其他物品
        GoodEffect goodEffect = GoodEffect.None;
        if (collider2D.tag == "Buff")
        {
            goodEffect = GoodEffect.Buff;
        }
        else if (collider2D.tag == "DeBuff")
        {
            goodEffect = GoodEffect.Debuff;
        }
        collider2D.GetComponent<Good>().GetInteractiveGood(goodEffect);

        
    }

    #endregion


    #region Animation Callback

    public void JumpSpeedUpDown()
    {
        _rigidbody2D.AddForce(-transform.up * _jumpDownForce);
        return;
    }

    #endregion
}
