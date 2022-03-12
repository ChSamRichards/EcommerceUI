using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace kiksAr.EcommerceFilter.Controllers
{
    public class DropdownController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdownCategory;
        [SerializeField] private TMP_Dropdown dropdownGender;

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
        
            dropdownCategory.options.Clear();
            dropdownGender.options.Clear();

            dropdownCategory.onValueChanged.AddListener(delegate { ValueChangedOnDropdown(dropdownCategory,gender); });
             dropdownGender.onValueChanged.AddListener(delegate { ValueChangedOnDropdownGender(dropdownGender); });
            foreach(var item in itemCategories)
            {
                dropdownCategory.options.Add(new TMP_Dropdown.OptionData(){text = item});
            }
            foreach(var item in genderCategories)
            {
                dropdownGender.options.Add(new TMP_Dropdown.OptionData(){text = item});
            }



        }

        private void ValueChangedOnDropdownGender(TMP_Dropdown dropdownGender)
        {
            int value = dropdownGender.value;


           UIObjectPooler.uIObjectPoolerInstance.ArrangeItemsByGender(dropdownGender.options[value].text);


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
