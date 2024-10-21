using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    // Player movement (플레이어 이동 관련 변수)
    private Vector3 moveDirection; // 플레이어의 이동 방향
    public Rigidbody2D playerRigidbody; // Rigidbody2D 컴포넌트 참조
    public float pSpeed, maxSpeed; // 플레이어 속도 및 최대 속도
    public Vector2 velocity; // 현재 플레이어의 속도

    // Player stats (플레이어 능력치)
    public float attackPower, attackCooldown, health, maxHealth, critAtk, critRate, damage; // 공격력, 쿨타임, 체력, 최대 체력, 크리티컬 데미지, 크리티컬 확률, 데미지
    public bool isAttacking, isBossEvent, isFadeCoroutineActive; // 공격 여부, 보스 이벤트 여부, 페이드 효과가 진행 중인지 여부

    // Enemy references (적과 관련된 참조)
    private EnemyState currentEnemy; // 현재 적의 상태
    private BossEvent currentBoss; // 현재 보스의 상태
    private EnemySpawn enemySpawner; // 적 스폰 시스템

    // UI and animations (UI 및 애니메이션 관련 변수)
    public Animator playerAnimator; // 플레이어 애니메이션
    public Animator runAnimator; // 달리기 애니메이션
    private FadeManager fadeManager; // 페이드 효과 매니저
    private RebirthButton rebirthButton; // 리버스 버튼 (환생 버튼)
    private Statuse statuseScript; // 스테이터스 관련 스크립트 (층수 등)

    // Initialization (초기 설정)
    void Start()
    {
        isFadeCoroutineActive = true; // 페이드 코루틴 활성화 플래그
        isAttacking = true; // 플레이어가 공격 중인지 여부
        moveDirection = Vector3.right; // 플레이어는 오른쪽으로 이동 (기본 값)

        // Rigidbody2D 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.position = new Vector3(-1.0f, 0.2f, 0.0f); // 초기 위치 설정

        fadeManager = FindObjectOfType<FadeManager>(); // FadeManager 스크립트를 찾음

        // 적 스폰 시스템을 Respawn 태그가 있는 오브젝트에서 가져옴
        GameObject respawnObject = GameObject.FindWithTag("Respawn");
        enemySpawner = respawnObject?.GetComponent<EnemySpawn>();

        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>(); // Statuse 스크립트 참조
        playerAnimator.SetBool("dead", false); // 초기에는 사망 상태가 아님
    }

    // Rebirth 버튼 활성화 함수 (리버스 버튼을 찾아 활성화)
    public void ActivateRebirthButton()
    {
        rebirthButton = GameObject.Find("RebirthButton").GetComponent<RebirthButton>();
    }

    // FixedUpdate: 물리 처리를 매 프레임마다 업데이트
    void FixedUpdate()
    {
        velocity = playerRigidbody.velocity;  // 캐릭터의 속도 값 저장

        // 페이드 코루틴 실행 여부 확인
        if (!isFadeCoroutineActive)
        {
            StartCoroutine(FadeCoroutine());
            isFadeCoroutineActive = true;
        }

        // 플레이어가 공격 중일 때 이동
        if (isAttacking)
        {
            playerRigidbody.AddForce(moveDirection * pSpeed); // 설정된 방향으로 플레이어에 힘을 가함
        }

        // 플레이어 속도가 최대 속도를 초과하지 않도록 제한
        if (velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, 0.0f);
        }

        // 애니메이션 파라미터 설정
        playerAnimator.SetFloat("hp", health); // 현재 체력을 애니메이션 파라미터로 전달
        playerAnimator.SetFloat("run", velocity.x); // 현재 속도를 애니메이션 파라미터로 전달
        runAnimator.SetFloat("run", velocity.x); // 달리기 애니메이션에도 현재 속도 전달
    }

    // 페이드 효과 처리 코루틴 (화면 전환 효과를 구현)
    IEnumerator FadeCoroutine()
    {
        // 화면을 어둡게 (페이드 아웃)
        yield return StartCoroutine(fadeManager.FadeOutCoroutine(0.02f));

        // 플레이어를 시작 위치로 이동시킴
        MoveToStartPosition();

        // 화면을 밝게 (페이드 인)
        yield return StartCoroutine(fadeManager.FadeInCoroutine(0.02f));
    }

    // 충돌 감지
    private void OnTriggerEnter2D(Collider2D col)
    {
        CalculateAttack(); // 공격력 계산

        // 적과 충돌했을 경우
        if (col.CompareTag("Enemy"))
        {
            if (!isBossEvent)
            {
                HandleEnemyCollision(col); // 일반 적과 충돌 처리
            }
            else
            {
                HandleBossCollision(col); // 보스와 충돌 처리
            }
        }
        else if (col.CompareTag("Loop")) // 특정 벽과 충돌했을 경우 (루프 처리)
        {
            HandleLoopCollision();
        }
    }

    // 충돌 종료 시 호출 (공격 가능 상태로 전환)
    private void OnTriggerExit2D(Collider2D col)
    {
        isAttacking = true; // 공격 가능 상태로 전환
    }

    // 적과 충돌 처리
    private void HandleEnemyCollision(Collider2D col)
    {
        currentEnemy = col.GetComponent<EnemyState>();
        currentEnemy.health -= damage; // 적의 체력 감소
        ShowFloatingText(currentEnemy.transform, damage); // 데미지 플로팅 텍스트 표시

        // 적의 체력이 0보다 크면 계속 공격
        if (currentEnemy.health > 0)
        {
            playerAnimator.SetBool("crash", true);
            StartAttackSequence(); // 적이 죽을 때까지 반복적으로 공격
        }
        else
        {
            playerAnimator.SetBool("ign", true);
            ApplyCollisionEffects(col); // 적이 죽었을 때 충돌 효과 적용
            currentEnemy.DeadEvent(); // 적의 죽음 이벤트 호출
        }
    }

    // 보스와 충돌 처리
    private void HandleBossCollision(Collider2D col)
    {
        currentBoss = col.GetComponent<BossEvent>();
        currentBoss.health -= damage; // 보스의 체력 감소
        ShowFloatingText(currentBoss.transform, damage); // 데미지 플로팅 텍스트 표시

        // 보스의 체력이 0보다 크면 계속 공격
        if (currentBoss.health > 0)
        {
            playerAnimator.SetBool("crash", true);
            StartAttackSequence(isBoss: true); // 보스가 죽을 때까지 반복적으로 공격
        }
        else
        {
            playerAnimator.SetBool("ign", true);
            ApplyCollisionEffects(col); // 보스가 죽었을 때 충돌 효과 적용
            currentBoss.DeadEvent(); // 보스의 죽음 이벤트 호출
        }
    }

    // Loop 벽과 충돌 처리 (층수 증가 및 버튼 활성화 여부 체크)
    private void HandleLoopCollision()
    {
        statuseScript.floor++; // 층수 증가

        // 리버스 버튼이 활성화되어 있지 않고, 층수가 100 이상일 때 리버스 버튼 활성화
        if (rebirthButton?.ReturnActiveValue() == true && statuseScript.floor >= 100)
        {
            rebirthButton.OnRebirthButton(); // 리버스 버튼 활성화
        }

        // 층수가 200 이상이고 보스 제한이 풀리지 않았을 때 보스 버튼 활성화
        if (statuseScript.floor >= 200 && !statuseScript.removeBossRestrictions)
        {
            GameObject underbar = GameObject.Find("Underbar");
            underbar?.GetComponent<UnderbarLock>().UnlockBossAnimation(); // 보스 잠금 해제 애니메이션 실행
            underbar?.GetComponent<UnderBar>().ButtonInteractable(); // 보스 버튼 활성화
        }

        isFadeCoroutineActive = false; // 페이드 코루틴을 다시 실행 가능하게 설정
    }

    // 적 공격 시퀀스 시작 (보스 여부에 따라 일반 적 또는 보스 공격 실행)
    private void StartAttackSequence(bool isBoss = false)
    {
        isAttacking = false; // 공격 중인 상태로 변경
        playerRigidbody.velocity = Vector2.zero; // 공격 중 이동 멈춤
        StartCoroutine(isBoss ? AttackBossCoroutine() : AttackEnemyCoroutine());
    }

    // 적 공격 코루틴
    private IEnumerator AttackEnemyCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown); // 공격 쿨타임 대기

        // 적의 체력이 0보다 크고 플레이어가 공격 중일 때 계속 공격
        while (currentEnemy.health > 0 && !isAttacking)
        {
            currentEnemy.health -= damage; // 적의 체력 감소
            if (currentEnemy.health <= 0)
            {
                currentEnemy.DeadEvent(); // 적이 죽으면 죽음 이벤트 호출
            }
            ShowFloatingText(currentEnemy.transform, damage); // 데미지 플로팅 텍스트 표시
            playerAnimator.SetBool("atk", true);
            yield return new WaitForSeconds(attackCooldown); // 공격 쿨타임 대기
        }
    }

    // 보스 공격 코루틴
    private IEnumerator AttackBossCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown); // 공격 쿨타임 대기

        // 보스의 체력이 0보다 크고 플레이어 체력도 0보다 크며 플레이어가 공격 중일 때 계속 공격
        while (currentBoss.health > 0 && !isAttacking && health > 0)
        {
            currentBoss.health -= damage; // 보스의 체력 감소
            if (currentBoss.health <= 0)
            {
                currentBoss.DeadEvent(); // 보스가 죽으면 죽음 이벤트 호출
            }
            ShowFloatingText(currentBoss.transform, damage); // 데미지 플로팅 텍스트 표시
            playerAnimator.SetBool("atk", true);
            yield return new WaitForSeconds(attackCooldown); // 공격 쿨타임 대기
        }
    }

    // 충돌 효과 적용 (적이 사망할 때 적용되는 물리 효과)
    private void ApplyCollisionEffects(Collider2D col)
    {
        Rigidbody2D colRigidbody = col.GetComponent<Rigidbody2D>();
        colRigidbody.AddForce(new Vector2(500f, 250f)); // 충돌한 적에게 힘을 가해 밀어냄
        colRigidbody.AddTorque(100); // 회전력을 추가하여 적이 회전하도록 함
        Destroy(col.transform.GetChild(0).gameObject); // 적의 첫 번째 자식 오브젝트 삭제 (일반적으로 그래픽 요소)
    }

    // 공격 애니메이션 종료 시 호출
    private void OnAttackAnimationEnd()
    {
        playerAnimator.SetBool("atk", false); // 공격 애니메이션 종료
        playerAnimator.SetBool("crash", false); // 충돌 애니메이션 종료
        playerAnimator.SetBool("ign", false); // 무시 애니메이션 종료
    }

    // 플레이어를 시작 위치로 이동 (보스 이벤트 여부에 따라 일반 몹 또는 보스 스폰)
    public void MoveToStartPosition(bool isBoss = false, int bossNumber = 0, int bossLevel = 1)
    {
        playerAnimator.SetBool("dead", false); // 죽음 상태 해제

        // 모든 적 제거
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy); // "Enemy" 태그를 가진 오브젝트 삭제
        }

        rebirthButton?.ReButtonActive(); // 리버스 버튼 상태 업데이트

        if (enemySpawner != null)
        {
            isBossEvent = false; // 보스 이벤트 상태 해제
            playerRigidbody.position = new Vector3(-1.0f, 0.2f, 0.0f); // 플레이어 시작 위치로 이동
            health = maxHealth; // 체력을 최대 체력으로 회복

            // 스테이지 층수 텍스트 업데이트
            GameObject.Find("Ui_text").GetComponent<EditText>().UpdateFloor();

            if (!isBoss)
            {
                enemySpawner.SetEnemySpawn(); // 일반 적 스폰
            }
            else
            {
                enemySpawner.SetBossLv(bossLevel); // 보스 레벨 설정
                enemySpawner.SetBossSpawn(bossNumber); // 보스 스폰
                isBossEvent = true; // 보스 이벤트 상태로 전환
            }
        }
        else
        {
            Debug.LogError("Respawn 태그를 가진 GameObject를 찾을 수 없습니다.");
        }
    }

    // 공격력 계산 (크리티컬 여부에 따라 데미지 결정)
    public void CalculateAttack()
    {
        damage = Random.Range(0, 999) > critRate * 10 ? attackPower : attackPower * 2 + attackPower * critAtk / 100;
    }

    // 플로팅 텍스트 표시 (데미지 텍스트)
    private void ShowFloatingText(Transform target, float damageValue)
    {
        FloatingText.Instance.ShowFloatingText(target, 0f, 0.5f, damageValue.ToString(), GetFloatingTextColor());
    }

    // 플로팅 텍스트 색상 결정 (크리티컬 여부에 따라 색상 변경)
    private Color GetFloatingTextColor()
    {
        return attackPower < damage ? Color.green : Color.white;
    }
}