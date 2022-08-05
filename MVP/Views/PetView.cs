using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP.Views
{
    public partial class PetView : Form,IPetView
    {
        //fileds
        private string message;
        private bool isSuccessful;
        private bool isEdited;

        //constructor
        public PetView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPagePetDetail);
            btnCancel.Click += delegate { this.Close(); };
        }
        private void AssociateAndRaiseViewEvents()
        {
            //search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
             {
                 if (e.KeyCode == Keys.Enter)
                     SearchEvent?.Invoke(this, EventArgs.Empty);

             };
            //add new 
            btnAddNew.Click += delegate {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetList);
                tabControl1.TabPages.Add(tabPagePetDetail);
                tabPagePetDetail.Text = "Add new pet";
            };
            //edit
            btnEdit.Click += delegate {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetList);
                tabControl1.TabPages.Add(tabPagePetDetail);
                tabPagePetDetail.Text = "Edit pet";
            };
            //save change
            btnSave.Click += delegate {
                SaveEvent?.Invoke(this, EventArgs.Empty);
            if(isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPagePetDetail);
                    tabControl1.TabPages.Add(tabPagePetList);

                }
                MessageBox.Show(Message);
                    };
            //cancel
            btnCancel.Click += delegate {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPagePetDetail);
                tabControl1.TabPages.Add(tabPagePetList);
            };
            //delete
            btnDelete.Click += delegate {
                DeleteEvent?.Invoke(this, EventArgs.Empty);
              var result=  MessageBox.Show("Are you sure you want to delete the selected pet","warining",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(result==DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

        }
        //proprites
        public string PetID { get => txtID.Text; set => txtID.Text=value; }
        public string PetName { get => txtName.Text; set => txtName.Text=value; }
        public string PetType { get => txtType.Text; set => txtType.Text=value; }
        public string PetColor { get => txtColor.Text; set => txtColor.Text=value; }
        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text=value; }
        public string SearchView { get => SearchView; set => SearchView=value; }
        public bool IsEdited { get => isEdited; set => isEdited=value; }
        public bool IsSuccessful { get => isSuccessful; set => isSuccessful=value; }
        public string Message { get =>   message; set => message=value; }

        //events
        public event EventHandler SearchEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //methods
        public void SetPetListBindingSource(BindingSource petList)
        {
            dataGridView1.DataSource = petList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PetView_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
        //singlton pattern (open a single form instance)
        private static PetView instance;
        public static PetView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {

                instance = new PetView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
