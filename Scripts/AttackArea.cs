using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public float existingTime = 0.2f; //何秒間オブジェクトが存在するか
    bool isActive; //当たり判定が有効かどうか、trueなら敵にダメージを与える

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(existingTime < 0.0f) {
            Destroy(gameObject);
        }
        existingTime -= Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        AnimalController controller = other.GetComponent<AnimalController>();

        if (controller != null)
        {
            if(isActive)
            {
                controller.ChangeHealth(-1);
                isActive = false;
            }
        }
        Debug.Log("existingTime=" + existingTime);
        Destroy(gameObject);
    }
}
