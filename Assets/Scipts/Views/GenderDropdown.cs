using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using kiksAr.EcommerceFilter.Controllers;

namespace kiksAr.EcommerceFilter.Views
{
    public class GenderDropdown : MonoBehaviour
    {
        [SerializeField] private Button button;
        private string gender;
        [SerializeField] private GameObject itemCheck;
        
        // Start is called before the first frame update
        
        void Start()
        {
            gender = this.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text;
            itemCheck = this.transform.GetChild(1).gameObject;
        }

        public void ArrangeItemsGender()
        {
            if(!itemCheck.activeInHierarchy)
            {
                DropdownController.dropdownControllerInstance.OnOptionsClickGender(gender, true);
                itemCheck.SetActive(true);
            }
            else
            {
                 DropdownController.dropdownControllerInstance.OnOptionsClickGender(gender, false);
                itemCheck.SetActive(false);
            }
            
            
        }
    }
}
