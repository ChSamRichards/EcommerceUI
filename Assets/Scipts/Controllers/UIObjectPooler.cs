using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using kiksAr.EcommerceFilter.Views;

namespace kiksAr.EcommerceFilter.Controllers
{

    public class UIObjectPooler : MonoBehaviour
    {

        [SerializeField] private bool enableLogs;
        public static UIObjectPooler uIObjectPoolerInstance;
        [SerializeField] private DropdownController dropdownController;



         private int selectedCategory;// dropdown value of  selected category
        //bselected list has items that are selected or unselected
        [SerializeField] private List<GameObject> selectedList = new List<GameObject>();
        //items belonging to particular category are assigned unter respective parents
        [SerializeField] private List<RectTransform> parents = new List<RectTransform>();

        private List<List<string>> categories = new List<List<string>>();
        private Color[] color = {Color.cyan,Color.magenta, Color.yellow};
          //total number of items
        [SerializeField] private int[] itemsCount = new int[3];
        public Dictionary<Image, int> selectedlistValues = new Dictionary<Image, int>(); //manipulates color and parents of selected items
        //items list contains all the items
        [SerializeField] private List<List<GameObject>> items = new List<List<GameObject>>(){new List<GameObject>(){},
                                                                                            new List<GameObject>(){},
                                                                                            new List<GameObject>(){}};


        [SerializeField] private Button applyFilter;
        [SerializeField] private Button resetFilter;
        [SerializeField] private Button closeButton;
        //parent of selected items
        [SerializeField] private Transform selectedListTranfrom;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Text text;


        public bool transferred;
       

        private void Awake()
        {
            uIObjectPoolerInstance = this;
            GetCategories();
            applyFilter.onClick.AddListener(ApplyFilter);
            resetFilter.onClick.AddListener(ResetFilter);
            closeButton.onClick.AddListener(ApplicationQuit);
            selectedCategory = 0;
            if(!enableLogs) Debug.unityLogger.logEnabled = false;
            else Debug.unityLogger.logEnabled = true;
            PoolItems();
 
        }
        private void PoolItems()
        {
             for(int k = 0;k < items.Count; k++)
             {
                for(int i = 0; i < itemsCount[k]; i++)
                {

                    GameObject item = new GameObject("Image" + i.ToString());
                    RectTransform rect = item.AddComponent<RectTransform>();
                    rect.transform.SetParent(parents[k]);
                    rect.localScale = Vector3.one;
                    item.tag = categories[k][Random.Range(0,categories[k].Count)];
                    Image im = item.AddComponent<Image>();
                    im.color = color[k];
                    item.AddComponent<PointerClick>();
                    items[k].Add(item);
                    Instantiate(text, rect);
                    text.text = item.tag;


                }
             }


        }
        public void GetCategories()
        {
             categories = dropdownController.Sep();
        }
#region Adding items on selection

        public void AddToSelectedList(GameObject item)
        {
            selectedList.Add(item);

        }
        public void RemoveFromList(GameObject item)
        {
            selectedList.Remove(item);

        }
#endregion

#region Filter
        private  void ApplyFilter()
        {
            transferred = true;
            for(int i = 0; i<selectedList.Count; i++)
            {
                selectedList[i].transform.SetParent(selectedListTranfrom);
            }
            parents[selectedCategory].gameObject.SetActive(false);
            selectedListTranfrom.parent.gameObject.SetActive(true);

        }
        private void ResetFilter()
        {
            transferred = false;
            for(int i = 0; i < items[selectedCategory].Count; i++)
            {
                items[selectedCategory][i].SetActive(false);
                items[selectedCategory][i].SetActive(true);

            }
            foreach(KeyValuePair<Image,int> item in selectedlistValues)
            {
                item.Key.transform.SetParent(parents[item.Value]);
                item.Key.color = color[item.Value];
            }
              selectedListTranfrom.parent.gameObject.SetActive(false);
            selectedList.Clear();

        }
#endregion

#region Arranging Items on value Selected
    
        public void ArrangeItemsByGender(string gender)
        {
             transferred = false;
            selectedListTranfrom.parent.gameObject.SetActive(false);
            for(int i = 0; i < items[selectedCategory].Count; i++)
            {
                if(items[selectedCategory][i].tag.Contains(gender))
                {
                    items[selectedCategory][i].SetActive(true);
                }
                else
                {
                     items[selectedCategory][i].SetActive(false);
                }

            }


        }
        public  void ArrangeItemsByCategory(int value, int count)
        {
            transferred = false;
            selectedListTranfrom.parent.gameObject.SetActive(false);
            selectedCategory = value;
            for(int i = 0; i < count; i++)
            {
                if(i == value)
                {
                    parents[i].gameObject.SetActive(true);
                    scrollRect.content = parents[i];
                }
                else
                {
                    parents[i].gameObject.SetActive(false);
                }
            }
            foreach(var item in items[selectedCategory])
            {
                item.SetActive(true);
            }





        }
#endregion

        private void ApplicationQuit()
        {
            Application.Quit();
        }





    }
}
