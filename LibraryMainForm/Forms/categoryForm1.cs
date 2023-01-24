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

namespace LibraryMainForm
{
    public partial class categoryForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {


        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();


        public categoryForm1()
        {
            InitializeComponent();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Category ca = new Category();
                ca.CategoryName = Convert.ToString(textEditCategoryName.EditValue);
                ca.IsActive = Convert.ToBoolean(checkEditIsActive.Checked);
                dbcontxt.Categories.InsertOnSubmit(ca);
                dbcontxt.SubmitChanges();
                Clear();


            }
            catch 
            {

                MessageBox.Show("لم يتم الحفظ");
                return;
            }
            gridControl1.DataSource = dbcontxt.Categories.ToList();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            textEditCategoryName.EditValue = "";
            checkEditIsActive.Checked = false;
        }
        private void categoryForm1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dbcontxt.Categories.ToList();
        }
    }
}