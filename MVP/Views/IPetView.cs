using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP.Views
{
   public interface IPetView
    {
        //fileds
        string PetID { get; set; }
        string PetName { get; set; }
        string PetType { get; set; }
        string PetColor { get; set; }
        string SearchValue { get; set; }
        string SearchView { get; set; }
        bool  IsEdited { get; set; }
        bool  IsSuccessful { get; set; }
        string  Message { get; set; }

        //events
        event EventHandler SearchEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler AddNewEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //methods
        void SetPetListBindingSource(BindingSource petList);
        void Show();//optionl
    }
}
