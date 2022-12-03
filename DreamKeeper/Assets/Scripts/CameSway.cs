using UnityEngine;

public class CameSway : MonoBehaviour
{
    private void LateUpdate()
    {
        float x = Mathf.Sin(Time.realtimeSinceStartup) * 0.1f * Time.deltaTime;
        float y = Mathf.Sin(Time.realtimeSinceStartup) * 0.1f * Time.deltaTime;

        transform.position += new Vector3(x, y, 0f);
    }

}