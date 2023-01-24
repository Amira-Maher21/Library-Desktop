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
    public partial class BookForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();

        public bool Barcode;
        public bool IsEdite;
        public int Code;
        private object g;

        public BookForm()
        {
            InitializeComponent();
        }
        void InsertOrUpdate()
        {
            try
            {
                if (IsEdite)
                {

                    var getdata = dbcontxt.Books.Where(a => a.BookId == Code).FirstOrDefault();
                    getdata.BookName = Convert.ToString(textEditBook.Text);
                    getdata.CategoryId =Convert.ToInt32(searchLookUpEditCategory.EditValue);
                    getdata.AuthorId = Convert.ToInt32(searchLookUpEditAuthor.EditValue);
                    getdata.DateOfPublication =Convert.ToDateTime(dateEditDateOfPublication.EditValue);
                    getdata.BeginingBalance =Convert.ToInt32(textEditBeginingBalance.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEditIsActive.Checked);
                    getdata.BookBarcode = Convert.ToString(MainDiscount.EditValue);
        
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    IsEdite = false;
                    Clear();
                    Fill();

                  

                }
                else
                {
                    Book bo = new Book();
                    bo.BookName = textEditBook.Text;
                    bo.AuthorId = Convert.ToInt32(searchLookUpEditAuthor.EditValue);
                    bo.CategoryId = Convert.ToInt32(searchLookUpEditAuthor.EditValue);
                    bo.DateOfPublication = Convert.ToDateTime(dateEditDateOfPublication.EditValue);
                    bo.BeginingBalance =Convert.ToInt32( textEditBeginingBalance.EditValue);
                    bo.IsActive = Convert.ToBoolean(checkEditIsActive.Checked);
                    bo.BookBarcode = Convert.ToString(MainDiscount.EditValue);
              
                    dbcontxt.Books.InsertOnSubmit(bo);
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                   // gridControl1.DataSource = dbcontxt.Books;
                    Clear();
                    Fill();
                }
            }
            catch 
            {

                MessageBox.Show("لم يتم الحفظ ");
            }
        }

        void Clear()
        {
            textEditBook.Text = "";
            searchLookUpEditAuthor.EditValue = null;
            dateEditDateOfPublication.EditValue = null;
            textEditBeginingBalance.EditValue = 0;
            checkEditIsActive.Checked = false;
            searchLookUpEditCategory.EditValue = null;
            MainDiscount.EditValue = null;
        }
        private void textEditBeginingBalance_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            int br;
            Fill();
            var barcode = dbcontxt.Books.Select(a => a.BookBarcode).Max();
            br = Convert.ToInt32(barcode);
            MainDiscount.Text = Convert.ToInt32(br+1).ToString();
        }
        void Fill()
        {
            searchLookUpEditCategory.Properties.DataSource = dbcontxt.Categories.ToList();
            searchLookUpEditAuthor.Properties.DataSource = dbcontxt.Authors.ToList();
            repositoryItemLookUpcateg.DataSource = dbcontxt.Categories.ToList();
            repositoryItemLookUpAuth.DataSource = dbcontxt.Authors.ToList();

            //var data = (from b in dbcontxt.Books
            //            join c in dbcontxt.Categories on b.CategoryId equals c.CategoryId
            //            join a in dbcontxt.Authors on b.AuthorId equals a.AuthorId
            //            select new { b.BookId,a.AuthorName, c.CategoryName, b.BookName, b.DateOfPublication, b.BeginingBalance }).ToList();
            gridControl1.DataSource = dbcontxt.Books.ToList();
            
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

            Code = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BookId"));
            var getdata = dbcontxt.Books.Where(a => a.BookId == Code).FirstOrDefault();


            textEditBook.Text = Convert.ToString(getdata.BookName);
            searchLookUpEditAuthor.EditValue = Convert.ToInt32(getdata.AuthorId);
            searchLookUpEditCategory.EditValue = Convert.ToInt32(getdata.CategoryId);
            dateEditDateOfPublication.EditValue = Convert.ToDateTime(getdata.DateOfPublication);
            textEditBeginingBalance.EditValue = Convert.ToString(getdata.BeginingBalance);
            checkEditIsActive.Checked = Convert.ToBoolean(getdata.IsActive);
            MainDiscount.EditValue = Convert.ToString(getdata.BookBarcode);      
          IsEdite = true;

           

            
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void MainDiscount_EditValueChanged(object sender, EventArgs e)
        {
           
        }
    }

 }
