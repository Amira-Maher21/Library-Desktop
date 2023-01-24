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
using DevExpress.XtraEditors;

namespace LibraryMainForm.Forms
{
    public partial class InvoiceForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();

        public bool ISEdite;
        public DataTable Invoicedata;
        public InvoiceForm1()
        {
            InitializeComponent();
            CreateGrid();


        }
        void InsertOrUpdate()
        {
            //gridView4.DataSource = Invoicedata;
            //Invoicedata = (DataTable)gridView4.DataSource;


            try
            {

                InvoiceMaster ma = new InvoiceMaster();

                ma.DocTypeId = Convert.ToInt32(searchLookUpEditDocType.EditValue);
                ma.InvoiceDate = Convert.ToDateTime(dateEditInvoice.EditValue);
                ma.StoreId = Convert.ToInt32(searchLookUpEditStore.EditValue);
               
                ma.MainDiscount = Convert.ToDecimal(MainDiscount.EditValue);
                ma.LastFinalTotalDiscount = Convert.ToDecimal(LastFinalTotalDiscount.EditValue);
                ma.FinalInvoiceWinValue = Convert.ToDecimal(FinalInvoiceWinValue.EditValue);
                ma.InvoiceWinValue = Convert.ToDecimal(InvoiceWinValue.EditValue);
                ma.InvoiceNetValue2 = Convert.ToDecimal(InvoiceNetValue2.EditValue);
                ma.InvoiceNetValueAfterDiscount = Convert.ToDecimal(InvoiceNetValueAfterDiscount.EditValue);

                dbcontxt.InvoiceMasters.InsertOnSubmit(ma);

                dbcontxt.SubmitChanges();



                for (int i = 0; i < gridView4.RowCount; i++)
                {

                    int id = Convert.ToInt32(gridView4.GetRowCellValue(i, "BookId"));
                    int qty = Convert.ToInt32(gridView4.GetRowCellValue(i, "BookQuantity"));
                    decimal pri = Convert.ToDecimal(gridView4.GetRowCellValue(i, "BookPrice"));
                    decimal tot = Convert.ToDecimal(gridView4.GetRowCellValue(i, "Total"));

                    dbcontxt.InsertInDetail(ma.Invoice_Id, id, qty, pri, tot);
                    dbcontxt.SubmitChanges();
                }

                MessageBox.Show("تم الحفظ");
            }
            catch
            {

                MessageBox.Show("لم يتم الحفظ");
            }


        }

        private void CreateGrid()
        {
            Invoicedata = new DataTable();

            Invoicedata.Columns.Add("BookId", typeof(int));
            Invoicedata.Columns.Add("BookQuantity", typeof(decimal));
            Invoicedata.Columns.Add("BookPrice", typeof(decimal));
            Invoicedata.Columns.Add("Total", typeof(decimal)).Expression = "BookQuantity*BookPrice";
            gridControl1.DataSource = Invoicedata;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void InvoiceForm1_Load(object sender, EventArgs e)
        {
            Fill();
        }

        private void Fill()
        {

            dateEditInvoice.EditValue = DateTime.Now;
            searchLookUpEditDocType.Properties.DataSource = dbcontxt.DocTypes.ToList();
            searchLookUpEditDocType.EditValue = 1;
            searchLookUpEditStore.Properties.DataSource = dbcontxt.Stores.ToList();
           

            repositoryItemSearchLookUpEdit1.DataSource = dbcontxt.Books.ToList();

        }

        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int id = new int();
            int docid = Convert.ToInt32(searchLookUpEditDocType.EditValue);

            if (e.Column.FieldName == "BookId")
            {
                id = Convert.ToInt32(gridView4.GetRowCellValue(e.RowHandle, "BookId"));

                var data = dbcontxt.Books.Where(a => a.BookId == id).FirstOrDefault();

                gridView4.SetRowCellValue(e.RowHandle, "BookQuantity", 1);

                if (docid == 1 || docid == 2 || docid == 5 || docid == 6)
                {
                    gridView4.SetRowCellValue(e.RowHandle, "BookPrice", data.Bookllpirice);
                }
                else
                {
                    gridView4.SetRowCellValue(e.RowHandle, "BookPrice", data.Bookbuypirice);
                }

                gridView4.FocusedRowHandle = -1;
                Claculation();
            }
        }

        private void Claculation()
        {

            decimal tot = new decimal();
            decimal netval = new decimal();
            decimal dis = new decimal();
            decimal maintot = new decimal();

            for (int i = 0; i < gridView4.RowCount; i++)
            {
                gridView4.FocusedRowHandle = -1;
                tot = Convert.ToDecimal(gridView4.GetRowCellValue(i, "Total"));
                netval = netval + tot;

            }

            dis = Convert.ToDecimal(MainDiscount.EditValue);
            if (dis == 0)
            {
                InvoiceNetValueAfterDiscount.EditValue = Convert.ToDecimal(netval);
                InvoiceNetValue2.EditValue = Convert.ToDecimal(netval);
            }
            else
            {
                maintot = netval - dis;
                InvoiceNetValueAfterDiscount.EditValue = Convert.ToDecimal(netval);
                InvoiceNetValue2.EditValue = 0;
                InvoiceNetValue2.EditValue = Convert.ToDecimal(maintot);
            }

        }

        private void textEditbarcode_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void MainDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            Claculation();
        }
        int BookId;
        decimal price;
        decimal price2;
        decimal SoldQty;

        private void textEditbarcode_KeyDown(object sender, KeyEventArgs e)
        {


            string bookcode = bookbarcode.Text;
            int docid = Convert.ToInt32(searchLookUpEditDocType.EditValue);
            if (e.KeyCode == Keys.Enter)
            {

                if (bookcode != null || bookcode != string.Empty || bookcode != "")
                {
                    var data = dbcontxt.Books.Where(a => a.BookBarcode == bookcode).FirstOrDefault();
                    if (data != null)
                    {
                        BookId = Convert.ToInt32(data.BookId);
                        price = Convert.ToDecimal(data.Bookllpirice);
                        price2 = Convert.ToDecimal(data.Bookbuypirice);
                        SoldQty = 1;

                        if (docid == 1 || docid == 2 || docid == 5 || docid == 6)
                        {
                           
                            Invoicedata.Rows.Add(BookId, price, SoldQty);
                            bookbarcode.Text = "";
                            bookbarcode.Focus();
                            Claculation();
                        }
                        else

                          
                        {
                           
                            Invoicedata.Rows.Add(BookId, price2, SoldQty);
                            bookbarcode.Text = "";
                             bookbarcode.Focus();
                            Claculation();
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("لايوجد صنف بهذا الرقم  الباركود خاطئ");
                        bookbarcode.Text = null;
                        bookbarcode.Focus();


                    }

                }
            }
                }
               

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void InvoiceWinValue_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
        } 