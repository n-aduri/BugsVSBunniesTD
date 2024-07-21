using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class TowerBehavior : MonoBehaviour
{

    SphereCollider myCollider;
    public float AttackRadius;
    //per second
    public float fireRate;
    // Start is called before the first frame update
    List<GameObject> targetList = new List<GameObject>(); //We can switch GameObject to instances of the Enemy class
    void Start()
    {
        myCollider=GetComponent<SphereCollider>();
        myCollider.radius=AttackRadius;
        StartCoroutine(FireLoop());
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other+" "+other.GetInstanceID()+" entered");
        targetList.Add(other.gameObject);
        //printList(list);

    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other + " " + other.GetInstanceID()+ " exited");
        targetList.Remove(other.gameObject);
        //printList(list);

    }
    //for testing
    void printList(List<int> num)
    {
        string numbs = "{ ";
        for (int i = 0; i < num.Count; i++)
        {
            numbs = numbs+ (num[i].ToString()+", ");
        }
        numbs += " }";
        Debug.Log(numbs);

    }

    private IEnumerator FireLoop()
    {
        GameObject prefab = GameObject.Find("Bullet");
        while (true)
        {
            if (targetList.Count > 0)
            {

                //pick a random enemy to attack
                int enemyID= Random.Range(0, targetList.Count - 1);
                GameObject unit = targetList[enemyID];
                Debug.Log("PEW! hit enemy " + unit + " at location " + unit.transform.position);
                prefab.GetComponent<bulletBehavior>().enemy = unit;
                //prefab.GetComponent<bulletBehavior>().setTargetPosition(unit);
                //the weird y position is so the bullet shoots from the top instaed of the middle. adjust as needed
                Instantiate(prefab, (this.transform.localPosition+new Vector3 (0,1,0)), Quaternion.identity);
                yield return new WaitForSeconds(fireRate);
            }

            yield return new WaitForEndOfFrame();
        }

        
    }
}
