using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public Rigidbody2D rigidbody2d; //EarthEffectで使用したいが、publicで良いのか?

    public int maxHealth = 1;
    int currentHealth;
    public float speed = 3.0f;
    public int isRight = 1; //左と右のどちらに動くか、1なら右、-1なら左
    public float changeDirectionTime = 3.0f; //移動方向を変える時間 
    float countTime;

    public GameObject dropPrefab; //倒されたときにドロップするアイテムを格納する
    Vector2 dropPlace = new Vector2(0, 0.5f); //ドロップアイテムがどこから出現するか(相対座標)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        countTime = changeDirectionTime;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        countTime -= Time.deltaTime;
        if (countTime < 0)
        {
            isRight *= -1;
            countTime = changeDirectionTime;
        }

        if(currentHealth <= 0)
        {
            drop();
        }
    }

    void FixedUpdate()
    {
        //移動
        Vector2 position = rigidbody2d.position;
        Vector2 velocity = rigidbody2d.linearVelocity;

        velocity.x = speed * isRight;
        rigidbody2d.linearVelocity = velocity;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void drop()
    {
        Instantiate(dropPrefab, rigidbody2d.position + dropPlace, Quaternion.identity);
        Destroy(gameObject);
    }
}
