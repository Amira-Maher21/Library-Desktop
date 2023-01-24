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
    public partial class BorrowerBookForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();


        public bool IsEdite;
        public int Code;
        public BorrowerBookForm1()
        {
            InitializeComponent();
        }


        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }
        void InsertOrUpdate()
        {
            try
            {
                if (IsEdite)
                {
                    var getdata = dbcontxt.BorrowerBooks.Where(a => a.BookId == Code).FirstOrDefault();
                    BorrowerBook br = new BorrowerBook();
                    br.BorrowerId = Convert.ToInt32(searchLookUpEditBorrower.EditValue);
                    br.BookId = Convert.ToInt32(searchLookUpEditBook.EditValue);
                    br.BorrowDate = Convert.ToDateTime(dateEditBorrowDate.EditValue);
                    br.BorrowReturnDate = Convert.ToDateTime(dateEditBorrowReturnDate.EditValue);
                    br.IsReturned = Convert.ToBoolean(checkEditIsReturned.EditValue);
                    br.EmployeeIdForBorrow = Convert.ToInt32(textEditEmployeeIdForBorrow.EditValue);
                    br.EmployeeIdForBorrow = Convert.ToInt32(textEditEmployeeIdForReturn.EditValue);
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    IsEdite = false;
                    Clear();
                    Fill();
                }
                else
                {

                    BorrowerBook br = new BorrowerBook();
                    br.BorrowerId = Convert.ToInt32(searchLookUpEditBorrower.EditValue);
                    br.BookId = Convert.ToInt32(searchLookUpEditBook.EditValue);
                    br.BorrowDate = Convert.ToDateTime(dateEditBorrowDate.EditValue);
                    br.BorrowReturnDate = Convert.ToDateTime(dateEditBorrowReturnDate.EditValue);
                    br.IsReturned = Convert.ToBoolean(checkEditIsReturned.EditValue);
                    br.EmployeeIdForBorrow = Convert.ToInt32(textEditEmployeeIdForBorrow.EditValue);
                    br.EmployeeIdForBorrow = Convert.ToInt32(textEditEmployeeIdForReturn.EditValue);

                    dbcontxt.BorrowerBooks.InsertOnSubmit(br);
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

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }
        void Clear()
        {
            searchLookUpEditBorrower.EditValue = 0;
            searchLookUpEditBook.EditValue = 0;
            dateEditBorrowDate.EditValue = null;
            dateEditBorrowReturnDate.EditValue = null;
            checkEditIsReturned.EditValue = null;
            textEditEmployeeIdForBorrow.EditValue = null;
            textEditEmployeeIdForReturn.EditValue = null;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void BorrowerBookForm1_Load(object sender, EventArgs e)
        {
            Fill();
        }
        void Fill()
        {

            searchLookUpEditBorrower.Properties.DataSource = dbcontxt.Borrowers.ToList();
            searchLookUpEditBook.Properties.DataSource = dbcontxt.Books.ToList();
            repositoryItemSearchLookUpEdit1.DataSource = dbcontxt.Borrowers.ToList();
            repositoryItemSearchLookUpEdit2.DataSource = dbcontxt.Books.ToList();
            gridControl1.DataSource = dbcontxt.BorrowerBooks.ToList();

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            var getdata = dbcontxt.BorrowerBooks.Where(a => a.BookId == Code).FirstOrDefault();
            Code = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BorrowerBookId"));

            
            searchLookUpEditBorrower.EditValue = Convert.ToInt32(getdata.BorrowerId);
            searchLookUpEditBook.EditValue = Convert.ToInt32(getdata.BookId);
            dateEditBorrowDate.EditValue = Convert.ToDateTime(getdata.BorrowDate);
            dateEditBorrowReturnDate.EditValue = Convert.ToDateTime(getdata.BorrowReturnDate);
            checkEditIsReturned.EditValue = Convert.ToBoolean(getdata.EmployeeIdForBorrow);
            textEditEmployeeIdForBorrow.EditValue = Convert.ToInt32(getdata.EmployeeIdForReturn);
            textEditEmployeeIdForReturn.EditValue = null;


            IsEdite = true;
        }
    }
}