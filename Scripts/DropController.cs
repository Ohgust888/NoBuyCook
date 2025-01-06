using UnityEngine;

public class DropController : MonoBehaviour
{
    //プレイヤーに持ち上げられる
    public bool Brought()
    {
        bool isBrought;
        GameObject player = GameObject.Find("Player");
        PlayerController controller = player.GetComponent<PlayerController>();
        isBrought = controller.bringItem(this.gameObject);
        return isBrought;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
