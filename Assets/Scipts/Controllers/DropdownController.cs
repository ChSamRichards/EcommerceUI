using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace kiksAr.EcommerceFilter.Controllers
{
    public class DropdownController : MonoBehaviour
    {
        public static DropdownController dropdownControllerInstance;
        [SerializeField] private TMP_Dropdown dropdownCategory;
        [SerializeField] private TMP_Dropdown dropdownGender;
        [SerializeField] private Button mainButtonGender;
        [SerializeField] private TMP_Text mainTextGender;
        [SerializeField] private Transform optionsGender;
        private List<Button> genderButtons = new List<Button>();

        private List<string> itemCategories = new List<string>{"Clothes", "Watches", "Jewellery"};
        private List<string> genderCategories = new List<string>{"Men", "Woman", "Kids"};

        private List<List<string>> tags = new List<List<string>>{
            new List<string>
            {
                "ClothesMen", "ClothesWoman", "ClothesKids"
            },
            new List<string>{
                "WatchesMen", "WatchesWoman", "WatchesKids"
            },
            new List<string>
            {                                               
                 "JewelleryMen", "JewelleryWoman", "JewelleryKids" 
            }
        };

        private string gender;


        private void Awake()
        {
            dropdownControllerInstance = this;
        
            dropdownCategory.options.Clear();
        //    dropdownGender.options.Clear();

            dropdownCategory.onValueChanged.AddListener(delegate { ValueChangedOnDropdown(dropdownCategory,gender); });
            mainButtonGender.onClick.AddListener(() => ValueChangedOnDropdownGender());
           //  dropdownGender.onValueChanged.AddListener(delegate { ValueChangedOnDropdownGender(); });
            foreach(var item in itemCategories)
            {
                dropdownCategory.options.Add(new TMP_Dropdown.OptionData(){text = item});
            }
            for(int i =0; i < genderCategories.Count; i++)
            {
                // dropdownGender.options.Add(new TMP_Dropdown.OptionData(){text = item});
                genderButtons.Add(optionsGender.GetChild(i).GetComponent<Button>());
                optionsGender.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = genderCategories[i];

            }
         


        }
        void Start()
        {
            mainTextGender.text = optionsGender.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
        }

        private void ValueChangedOnDropdownGender()
        {
            if(!optionsGender.transform.parent.gameObject.activeInHierarchy)

                 optionsGender.transform.parent.gameObject.SetActive(true);
            else
                optionsGender.transform.parent.gameObject.SetActive(false);
        //     int value = dropdownGender.value;


        //    UIObjectPooler.uIObjectPoolerInstance.ArrangeItemsByGender(dropdownGender.options[value].text);


        }
        public void OnOptionsClickGender(string gender, bool open)
        {
            UIObjectPooler.uIObjectPoolerInstance.ArrangeItemsByGender(gender,open);
             optionsGender.transform.parent.gameObject.SetActive(false);


        }
        private void ValueChangedOnDropdown(TMP_Dropdown dropdownCategory, string gender)
        {
            int value = dropdownCategory.value;

            UIObjectPooler.uIObjectPoolerInstance.ArrangeItemsByCategory(value, dropdownCategory.options.Count);

        }
        public List<List<string>> Sep()
        {
            return tags;
        }
    

    }
}
