using System.Collections.Generic;
using UnityEngine;

public class CreditRoll : MonoBehaviour
{
    [System.Serializable]
    struct Credit
    {
        public string name;
        public string role;
    }
    [System.Serializable]
    struct CreditList
    {
        public List<Credit> credits;
    }
    public TextAsset credits_file;
    public Canvas canvas;
    public GameObject credit_prefab;
    // Start is called before the first frame update
    void Start()
    {
        CreditList credit_list = JsonUtility.FromJson<CreditList>(credits_file.text);
        credit_list.credits.Sort((lhs, rhs) => {
            return rhs.name.CompareTo(lhs.name);
        });
        Vector3 position = new Vector3(0, 100, 0);
        foreach (var credit in credit_list.credits)
        {
            GameObject c = Instantiate(credit_prefab, canvas.transform.Find("CreditRoll"));
            c.transform.Find("Name").GetComponent<TMPro.TMP_Text>().text = credit.name;
            c.transform.Find("Role").GetComponent<TMPro.TMP_Text>().text = credit.role;
            c.transform.position = position;
            position.y += 45;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
