using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public int Number = 1000;
    public GameObject Cube;
    void Start()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (Time.timeScale == 1.0f)
        //        Time.timeScale = 0.1f;
        //    else
        //        Time.timeScale = 1.0f;

        //}
        Time.timeScale = 3.0f;
        for (int i = 0; i < Number; i++)
        {
            Vector3 newPos = new()
            {
                x = Random.Range(-10, 10),
                y = Random.Range(-10, 10),
                z = Random.Range(-3, 10)
            };
            // 0 ªº·N«ä¡C
            Quaternion newRota = Quaternion.identity;
            GameObject newCube = Instantiate(Cube, newPos, newRota);
        }
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
    private void Update()
    {
        Debug.Log("Update");
    }
}