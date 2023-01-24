using System;

namespace LibraryMainForm.Forms
{
    internal class DataBookTestDataContext
    {
        public object InvoiceMaster { get; internal set; }

        internal void SubmitChanges()
        {
            throw new NotImplementedException();
        }

        internal void InsertInDetail(int invoice_Id, int id, int qty, decimal pri, decimal tot)
        {
            throw new NotImplementedException();
        }
    }
}