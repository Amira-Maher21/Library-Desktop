using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using LibraryMainForm.Database;

namespace LibraryMainForm.Forms
{
    public partial class StoreForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();
        public StoreForm1()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                Store st = new Store();
                st.StoreDesc = Convert.ToString(textEditStoreDesc.EditValue);
                st.StoreIsActive = Convert.ToBoolean(checkEditStoreIsActive.Checked);
                
                dbcontxt.Stores.InsertOnSubmit(st);
                dbcontxt.SubmitChanges();

                Clear();


            }
            catch 
            {
                MessageBox.Show("لم يتم الحفظ");
                return;
            }
           gridControl1.DataSource = dbcontxt.Stores.ToList();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
        private void Clear()
        {
            textEditStoreDesc.EditValue = null;
            checkEditStoreIsActive.Checked = false;


        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void StoreForm1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dbcontxt.Stores.ToList();
        }
    }
}