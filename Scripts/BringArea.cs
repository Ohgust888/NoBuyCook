using UnityEngine;

public class BringArea : MonoBehaviour
{
    public float existingTime = 0.2f; //何秒間オブジェクトが存在するか
    bool isActive; //当たり判定が有効かどうか、trueならアイテムを持ち上げる

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
        DropController controller = other.GetComponent<DropController>();

        if (controller != null)
        {
            if(isActive)
            {
                bool bringSuccess;
                bringSuccess = controller.Brought();
                if(bringSuccess)
                {
                    isActive = false;
                }
            }
        }
        Debug.Log("existingTime=" + existingTime);
        Destroy(gameObject);
    }
}
