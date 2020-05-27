using UnityEngine;

public class flat_madamController : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private bool walk = true;
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        if (walk)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                Time.deltaTime*9);
            transform.LookAt(player.transform.position);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            
            GetComponent<Animator>().SetBool("Walk", false);
            walk = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetBool("Walk", true);
            walk = true;
        }
    }


}
