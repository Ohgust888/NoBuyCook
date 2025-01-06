using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public float existingTime = 0.2f; //���b�ԃI�u�W�F�N�g�����݂��邩
    bool isActive; //�����蔻�肪�L�����ǂ����Atrue�Ȃ�G�Ƀ_���[�W��^����

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
