using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public Rigidbody2D rigidbody2d; //EarthEffect�Ŏg�p���������Apublic�ŗǂ��̂�?

    public int maxHealth = 1;
    int currentHealth;
    public float speed = 3.0f;
    public int isRight = 1; //���ƉE�̂ǂ���ɓ������A1�Ȃ�E�A-1�Ȃ獶
    public float changeDirectionTime = 3.0f; //�ړ�������ς��鎞�� 
    float countTime;

    public GameObject dropPrefab; //�|���ꂽ�Ƃ��Ƀh���b�v����A�C�e�����i�[����
    Vector2 dropPlace = new Vector2(0, 0.5f); //�h���b�v�A�C�e�����ǂ�����o�����邩(���΍��W)

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
        //�ړ�
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
