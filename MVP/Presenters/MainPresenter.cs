using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP.Models;
using MVP.Views;
using MVP._Repositories;


namespace MVP.Presenters
{
   public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnnectionString = sqlConnectionString;
            this.mainView.ShowPetView += shwoPetsView;
        }

        private void shwoPetsView(object sender, EventArgs e)
        {
            IPetView view =  PetView.GetInstance((MainView)mainView);
            IPetRepositry repositry = new PetRepository(sqlConnnectionString);
            new PetPresenter(view, repositry);
        }
    }
}
