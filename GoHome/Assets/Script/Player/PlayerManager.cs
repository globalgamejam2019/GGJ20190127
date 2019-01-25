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
    leftWalk = 0,
    rightWalk = 1,
    jump = 2,
    scrunch = 3,
    idle = 4,
}
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float _firstBlood = 100.0f;

    private Animator _animator;
    private Dictionary<int, int> _animatorHashIdList;
    private Collider2D _collider2D;

    private PlayerData _playerData;
    private playerStatus _currentPlayerStatus = playerStatus.idle;
    private playerStatus _lastPlayerStatus = playerStatus.idle;

    void Awake()
    {
        _playerData = new PlayerData(_firstBlood);
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();

        _animatorHashIdList = new Dictionary<int, int>();
        _animatorHashIdList.Add((int) playerStatus.idle, Animator.StringToHash("idle"));
        _animatorHashIdList.Add((int) playerStatus.rightWalk, Animator.StringToHash("rightWalk"));
        _animatorHashIdList.Add((int) playerStatus.jump, Animator.StringToHash("jump"));
        _animatorHashIdList.Add((int) playerStatus.leftWalk, Animator.StringToHash("leftWalk"));
        _animatorHashIdList.Add((int) playerStatus.scrunch, Animator.StringToHash("scrunch"));
    }

    void Update()
    {
        //controller 
        UpdateControllerListen();
    }

    void FixedUpdate()
    {
        UpdatePlayerView();
    }

    #region Controller

    private void UpdateControllerListen()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _lastPlayerStatus = _currentPlayerStatus;
            _currentPlayerStatus = playerStatus.leftWalk;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _lastPlayerStatus = _currentPlayerStatus;
            _currentPlayerStatus = playerStatus.rightWalk;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            _lastPlayerStatus = _currentPlayerStatus;
            _currentPlayerStatus = playerStatus.scrunch;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
        {
            _lastPlayerStatus = _currentPlayerStatus;
            _currentPlayerStatus = playerStatus.jump;
        }
        else
        {
            _lastPlayerStatus = _currentPlayerStatus;
            _currentPlayerStatus = playerStatus.idle;
        }
    }

    private void UpdatePlayerView()
    {
        //状态机
        _animator.ResetTrigger(_animatorHashIdList[(int) _lastPlayerStatus]);
        _animator.SetTrigger(_animatorHashIdList[(int)_currentPlayerStatus]);

        //对应状态设置
        switch (_currentPlayerStatus)
        {
            case playerStatus.idle:
                SetupIdle();
                break;
            case playerStatus.jump:
                SetupJump();
                break;
            case playerStatus.leftWalk:
                SetupLeftWalk();
                break;
            case playerStatus.rightWalk:
                SetupRightWalk();
                break;
            case playerStatus.scrunch:
                SetupScrunch();
                break;
        }
    }

    private void SetupIdle()
    {

    }
    private void SetupJump()
    {

    }
    private void SetupLeftWalk()
    {

    }
    private void SetupRightWalk()
    {

    }
    private void SetupScrunch()
    {

    }

    #endregion

    #region Collider

    

    #endregion
}
