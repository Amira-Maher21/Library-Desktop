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
    public partial class AuthorForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();
        public bool IsEdite;
        public int Code;
        public AuthorForm1()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                if (true)
                {
                    var getdata = dbcontxt.Authors.Where(a=> a.NationalityId == Code).FirstOrDefault();

                    getdata.AuthorName =Convert.ToString(textEditAuthor.EditValue);
                    getdata.AuthorBirthdate = Convert.ToDateTime(dateEditAuthorBirthdate.Text);
                    getdata.NationalityId = Convert.ToInt32(searchLookUpEditNationality.EditValue);
                    getdata.AuthorDeathdate = Convert.ToDateTime(textEditAuthorDeathdate.Text);
                    getdata.AuthorBookNumber = Convert.ToInt32(textEditAuthorBookNumber.EditValue);
                    
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    IsEdite = false;
                    clear();
              
              
                }
                else
                {
                    Author Au = new Author();
                    Au.AuthorName = Convert.ToString(textEditAuthor.EditValue);
                    Au.AuthorBirthdate = Convert.ToDateTime(dateEditAuthorBirthdate.Text);
                    Au.NationalityId = Convert.ToInt32(searchLookUpEditNationality.EditValue);
                    Au.AuthorDeathdate = Convert.ToDateTime(textEditAuthorDeathdate.Text);
                    Au.AuthorBookNumber = Convert.ToInt32(textEditAuthorBookNumber.EditValue);
                    dbcontxt.Authors.InsertOnSubmit(Au);
                    dbcontxt.SubmitChanges();
                    gridControl1.DataSource = dbcontxt.Authors.ToList();
                    MessageBox.Show("تم الحفظ");
                    clear();
                }
              
            }
            catch 
            {
                MessageBox.Show("لم يتم الحفظ ");

            }

        }

        private void AuthorForm1_Load(object sender, EventArgs e)
        {
            searchLookUpEditNationality.Properties.DataSource = dbcontxt.Nationalities.ToList();
            gridControl1.DataSource = dbcontxt.Authors.ToList();

            var data = (from a in dbcontxt.Authors
                        join n in dbcontxt.Nationalities on a.NationalityId equals n.NationalityId
                        select new { a.AuthorName, a.AuthorDeathdate, a.AuthorBookNumber, a.AuthorBirthdate, a.NationalityId, }).ToList();

            gridControl1.DataSource = data;








        }
        private void clear()
        {
            textEditAuthor.EditValue = null;
            dateEditAuthorBirthdate.Text = "";
            searchLookUpEditNationality.EditValue = null;
            textEditAuthorDeathdate.Text = "";
            textEditAuthorBookNumber.EditValue = null;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Code = Convert.ToInt32(gridView1.GetFocusedRowCellValue("AuthorId"));
            var getdata = dbcontxt.Authors.Where(a => a.NationalityId == Code).FirstOrDefault();

            textEditAuthor.EditValue = Convert.ToString(getdata.AuthorName);
            dateEditAuthorBirthdate.EditValue = Convert.ToDateTime(getdata.AuthorBirthdate);
            searchLookUpEditNationality.EditValue = Convert.ToInt32(getdata.NationalityId);
            textEditAuthorDeathdate.EditValue = Convert.ToDateTime(getdata.AuthorDeathdate);
            textEditAuthorBookNumber.EditValue = Convert.ToInt32(getdata.AuthorBookNumber);


               IsEdite = true;


       
     

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            clear();
        }

        private void textEditAuthorBookNumber_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}