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
    public partial class BookRequestForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();
        public bool IsEdite;
        public int Code;
        public BookRequestForm1()
        {
            InitializeComponent();
        }


        void InsertOrUpdate()
        {

            try
            {
                if (true)
                {
                    var getdata = dbcontxt.BookRequests.Where(a => a.BookRequestId == Code).FirstOrDefault();
                    getdata.DocTypeId = Convert.ToInt32(searchLookUpEditDocTypeId.EditValue);
                    getdata.BookRequestDate = Convert.ToDateTime(dateEditBookRequestDate.EditValue);
                    getdata.TotalNumberOfBooks = Convert.ToInt32(textEditTotalNumberOfBooks.EditValue);
                    getdata.IsRecieved = Convert.ToBoolean(checkEditIsRecieved.Checked);
                    getdata.IsClosed = Convert.ToBoolean(checkEditIsClosed.Checked);
                    getdata.EmployeeIdForTransffering = Convert.ToInt32(searchLookUpEditEmployeeIdForTransffering.EditValue);
                    getdata.EmployeeIdForRecieving = Convert.ToInt32(searchLookUpEditEmployeeIdForRecieving.EditValue);
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    IsEdite = false;
                    Clear();
                    Fill();
                }
                else
                {

                    BookRequest qu = new BookRequest();
                    qu.DocTypeId = Convert.ToInt32(searchLookUpEditDocTypeId.EditValue);
                    qu.BookRequestDate = Convert.ToDateTime(dateEditBookRequestDate.EditValue);
                    qu.TotalNumberOfBooks = Convert.ToInt32(textEditTotalNumberOfBooks.EditValue);
                    qu.IsRecieved = Convert.ToBoolean(checkEditIsRecieved.Checked);
                    qu.IsClosed = Convert.ToBoolean(checkEditIsClosed.Checked);
                    qu.EmployeeIdForTransffering = Convert.ToInt32(searchLookUpEditEmployeeIdForTransffering.EditValue);
                    qu.EmployeeIdForRecieving = Convert.ToInt32(searchLookUpEditEmployeeIdForRecieving.EditValue);

                    dbcontxt.BookRequests.InsertOnSubmit(qu);
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                   
                    Clear();
                    Fill();



                }
            }
            catch (Exception)
            {

                MessageBox.Show("لم يتم الحفظ ");
            }


        }

        void Clear()
        {
            searchLookUpEditDocTypeId.EditValue = 0;
            dateEditBookRequestDate.EditValue = null;
            textEditTotalNumberOfBooks.EditValue = null;
            checkEditIsRecieved.Checked = false;
            checkEditIsClosed.Checked = false; 
            searchLookUpEditEmployeeIdForTransffering.EditValue = 0;
            searchLookUpEditEmployeeIdForRecieving.EditValue = 0;
        }
        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void BookRequestForm1_Load(object sender, EventArgs e)
        {
            Fill();
        }
        void Fill()
        {

            searchLookUpEditDocTypeId.Properties.DataSource = dbcontxt.DocTypes.ToList();
            searchLookUpEditEmployeeIdForTransffering.Properties.DataSource = dbcontxt.Employees.ToList();
            searchLookUpEditEmployeeIdForRecieving.Properties.DataSource = dbcontxt.Employees.ToList();

          repositoryItemSearchLookUpEdit1.DataSource= dbcontxt.DocTypes.ToList();
            repositoryItemSearchLookUpEdit2.DataSource = dbcontxt.Employees.ToList();
            repositoryItemSearchLookUpEdit3.DataSource = dbcontxt.Employees.ToList();


            gridControl1.DataSource = dbcontxt.BookRequests.ToList();


        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
             
            Code =Convert.ToInt32(gridView1.GetFocusedRowCellValue("BookRequestId"));
            var getdata = dbcontxt.BookRequests.Where(a => a.BookRequestId == Code).FirstOrDefault();


            searchLookUpEditDocTypeId.EditValue = Convert.ToInt32(getdata.DocTypeId);
            dateEditBookRequestDate.EditValue = Convert.ToBoolean(getdata.BookRequestDate);
            textEditTotalNumberOfBooks.EditValue = Convert.ToInt32(getdata.TotalNumberOfBooks);
            checkEditIsRecieved.Checked = Convert.ToBoolean(getdata.IsRecieved);
            checkEditIsClosed.Checked = Convert.ToBoolean(getdata.IsClosed);
            searchLookUpEditEmployeeIdForTransffering.EditValue = Convert.ToInt32(getdata.EmployeeIdForTransffering);
            searchLookUpEditEmployeeIdForRecieving.EditValue = Convert.ToInt32(getdata.EmployeeIdForTransffering);



            IsEdite = true;
        }
    }
}