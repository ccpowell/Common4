using System;
using System.Collections.Generic;
using DRCOG.WebService.GlobalUserService.Core.Domain;

namespace DRCOG.WebService.GlobalUserService.Core.DataInterfaces
{
    public interface ICheckoutRepository
    {
        //Checkout - Systems
        List<CheckoutSystem> GetCheckoutSystems();
        CheckoutSystem GetCheckoutSystem(int systemID);

        //Checkout - Products
        List<CheckoutProduct> GetProducts();
        CheckoutProduct GetProduct(int productID);

        //Checkout - Debit
        List<CheckoutDebit> GetDebitSummary(Guid userID, bool showUnpaid);
        bool CreateDebit(Guid userID, CheckoutDebit debit, string systemValue);

        //Checkout - Receipts
        List<CheckoutReceipt> GetReceiptSummary(Guid userID);
    }
}
