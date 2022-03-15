
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using kiksAr.EcommerceFilter.Controllers;

namespace kiksAr.EcommerceFilter.Views
{
    public class PointerClick : MonoBehaviour, IPointerClickHandler
    {
        private bool selected;

        private Color c = new Color();
        private Image im;
        private int parentIndex;
        public Text details;


        void Start()
        {
            im = this.gameObject.GetComponent<Image>();
            c = im.color;
            parentIndex = this.transform.parent.GetSiblingIndex();
            details = this.transform.GetChild(0).GetComponent<Text>();
          //  Debug.Log(details.text);

        }    

        public void OnPointerClick( PointerEventData eventData )
        {
            if(!UIObjectPooler.uIObjectPoolerInstance.transferred)
            {
                if(selected)
                {
                    im.color = c;
                    UIObjectPooler.uIObjectPoolerInstance.RemoveFromList(this.gameObject);
                     UIObjectPooler.uIObjectPoolerInstance.selectedlistValues.Remove(im);
                    Debug.Log("UnSelected");
                    selected = false;


                }
                else
                {
                    im.color = Color.blue;
                    UIObjectPooler.uIObjectPoolerInstance.AddToSelectedList(this.gameObject);
                    UIObjectPooler.uIObjectPoolerInstance.selectedlistValues.Add(im, parentIndex);
                    UIObjectPooler.uIObjectPoolerInstance.ShowItemDetailsOnClick(details);
                    Debug.Log( "Clicked!" ); 
                    selected = true;



                }
            }


        }

    }
}
