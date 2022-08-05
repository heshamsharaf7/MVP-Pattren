using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVP.Models;
using MVP.Views;

namespace MVP.Presenters
{
   public class PetPresenter
    {
        //fileds
        private IPetView view;
        private IPetRepositry repositry;
        private BindingSource petrsBindingSource;
        private IEnumerable<PetModel> petList;

        //constructor
        public PetPresenter(IPetView view, IPetRepositry repositry)
        {
            this.petrsBindingSource = new BindingSource();
            this.view = view;
            this.repositry = repositry;
            //subsecribe event handler methods to view events
            this.view.SearchEvent += SearchPet;
            this.view.AddNewEvent += AddNewPet;
            this.view.EditEvent += LoadSelectedPetToEdit;
            this.view.DeleteEvent += DeleteSelectedPet;
            this.view.SaveEvent += SavePet;
            this.view.CancelEvent += CancelAction;
            //set the pets bindind source
            this.view.SetPetListBindingSource(petrsBindingSource);
            //Load pet list view
            LoadAllPetList(); 
            //show view
            this.view.Show();
        }
        //methods
        private void LoadAllPetList()
        {
            petList = repositry.GetAll();
            petrsBindingSource.DataSource = petList;//set data source
        }
        private void SearchPet(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                petList = repositry.GetByValue(this.view.SearchValue);
            else petList = repositry.GetAll();
            petrsBindingSource.DataSource = petList;
        } 
        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFileds();
        }

        private void SavePet(object sender, EventArgs e)
        {
            var model = new PetModel();
            model.Id = Convert.ToInt32(view.PetID);
            model.Name = view.PetName.ToString();
            model.Type = view.PetType.ToString();
            model.Color = view.PetColor.ToString();
            try
            {
                new Common.ModelDataValdation().Validate(model);
                if(view.IsEdited)//edit model
                {
                    repositry.Edit(model);
                    view.Message = "pet edited sucessfuly";
                }
                else
                {
                    repositry.Add(model);
                    view.Message = "pet added sucessfuly";
                }
                view.IsSuccessful = true;
                LoadAllPetList();
                CleanViewFileds();
            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFileds()
        {
            view.PetID = "0";
            view.PetName = "";
            view.PetType = "";
            view.PetColor = "";
        }

        private void DeleteSelectedPet(object sender, EventArgs e)
        {
            try
            {
                var pet = (PetModel)petrsBindingSource.Current;
                repositry.Delete(pet.Id);
                view.IsSuccessful = true;
                view.Message = "Pet deleted successfuly";
                LoadAllPetList();

            }catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "an error ocurred, could not delete pet";
            }
        }

        private void LoadSelectedPetToEdit(object sender, EventArgs e)
        {
            var  pet = (PetModel)petrsBindingSource.Current;
            view.PetID = pet.Id.ToString();
            view.PetName = pet.Name;
            view.PetType = pet.Type;
            view.PetColor = pet.Color;
            view.IsEdited = true;
        }

        private void AddNewPet(object sender, EventArgs e)
        {
            view.IsEdited = false;
        }

        
    }
}
