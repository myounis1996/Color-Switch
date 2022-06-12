using UnityEngine;

public class DeActivator : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - 7f)
        {
            gameObject.SetActive(false);
            GameObject obj = ObjectPooling.current.GetpooledObject();
            if (obj == null) return;
            obj.transform.position = Camera.main.transform.position + new Vector3(0f, 7f, 11f);
            obj.SetActive(true);
            for (int i = 1; i < obj.transform.childCount; i++)
            {
                obj.transform.GetChild(i).gameObject.SetActive(true); 
            }
        }
    }
}
