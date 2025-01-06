using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3.0f;
    float horizontal;
    float vertical;

    Rigidbody2D rigidbody2d;

    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject attackPrefab;
    public GameObject bringPrefab;

    const int canHasItems = 3;
    int countHavingItems; // 今アイテムをいくつ持ち上げているか
    public GameObject[] hasItem = new GameObject[canHasItems]; //持ち上げているゲームオブジェクト
    Rigidbody2D[] itemController = new Rigidbody2D[canHasItems]; //持ち上げているゲームオブジェクトのスクリプトを入れる

    //攻撃をする
    void Attack()
    {
        //主人公が向いている方向に近接範囲攻撃
        GameObject attackObject = Instantiate(attackPrefab, rigidbody2d.position + lookDirection * 1.0f, Quaternion.identity);

        //AttackArea attackArea = attackObject.GetComponent<AttackArea>();
        //attackArea.Attack(lookDirection, 300);

        
    }

    //アイテムを持ち上げる
    void bringCheck()
    {
        GameObject bringObject = Instantiate(bringPrefab, rigidbody2d.position, Quaternion.identity);
    }

    public bool bringItem(GameObject other)
    {
        bool canBring = true;
        if(countHavingItems < canHasItems)
        {
            //canBring = true;
            for(int i = 0; i < countHavingItems; i++)
            {
                if(hasItem[i] == other)
                {
                    canBring = false;
                    break;
                }
            }
            if(canBring){
                hasItem[countHavingItems] = other;
                itemController[countHavingItems] = other.GetComponent<Rigidbody2D>();
                countHavingItems++;
                Debug.Log("countHavingItems=" + countHavingItems);
            }
        }
        return canBring;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        countHavingItems = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //キー入力の取得
        horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        vertical = 0.0f; //縦方向へ自由に移動はできない

        //攻撃(FixedUpdateに入れると上手く動作しない)
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                bringCheck();
            }
            else{
                Attack();
            }
        }


    }

    void FixedUpdate()
    {
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        //移動
        Vector2 position = rigidbody2d.position;
        Vector2 velocity = rigidbody2d.linearVelocity;
        //Debug.Log("velocity=" + velocity);
        /*if(Mathf.Abs(rigidbody2d.linearVelocity.x) < speed)
        {
            Vector2 force = new Vector2 (speed * speed * horizontal, 0.0f);
            rigidbody2d.AddForce(force);
        }
        //移動入力が無いときは、強引に停止
        if(Mathf.Abs(horizontal) < 0.1f) {
            velocity.x = 0.0f;
            rigidbody2d.linearVelocity = velocity;
        }//*/
        velocity.x = speed * horizontal;
        rigidbody2d.linearVelocity = velocity;
        //position.x = position.x + speed * horizontal * Time.deltaTime;
        //position.y = position.y + speed * vertical * Time.deltaTime;
        //rigidbody2d.MovePosition(position);
        //rigidbody2d.velocity = velocity; //座標を直接いじると速度の計算が上手くいかない(?)ため、座標変更前の速度に戻す

        //持ち上げた物体の座標操作
        for(int i = 0; i < countHavingItems; i++){
            Vector2 itPos = itemController[i].position;
            Vector2 itVel = itemController[i].linearVelocity;
            itPos.x = position.x;
            itPos.y = position.y + 1.0f + (i*0.2f);
            itVel.y = 0.0f;
            itemController[i].position = itPos;
            itemController[i].linearVelocity = itVel;
            Debug.Log("itPos=" + itPos);
        }
    }
}
