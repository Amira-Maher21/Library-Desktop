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
    public partial class NationalityForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();
        public NationalityForm1()
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
                Nationality Na = new Nationality();
                Na.NationalityDesc = Convert.ToString(textEditNationalityDesc.EditValue);
                Na.IsActive = Convert.ToBoolean(checkEditIsActive.Checked);
                dbcontxt.Nationalities.InsertOnSubmit(Na);
                dbcontxt.SubmitChanges();
                MessageBox.Show("تم الحفظ");
                Clear();
            }
            catch (Exception)
            {

                MessageBox.Show("لم يتم الحفظ");
            }

            gridControl1.DataSource = dbcontxt.Nationalities.ToList();

        }
        private void Clear()
        {
            textEditNationalityDesc.EditValue = null;
            checkEditIsActive.Checked = false;
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void NationalityForm1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dbcontxt.Nationalities.ToList();
        }
    }
}