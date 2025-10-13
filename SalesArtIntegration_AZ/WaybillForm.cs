using SalesArtIntegration_AZ.Manager.Api;
using SalesArtIntegration_AZ.Manager.Config;
using SalesArtIntegration_AZ.Models.Invoice;
using SalesArtIntegration_AZ.Models.Request;
using SalesArtIntegration_AZ.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SalesArtIntegration_AZ
{
    public partial class WaybillForm : Form
    {
        string documentType = "";
        WayBillModelResponse waybillResponse = new WayBillModelResponse();
        public WaybillForm()
        {
            InitializeComponent();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
            this.Hide();
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollectionForm collectionForm = new CollectionForm();
            collectionForm.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private async void bttnGetWaybill_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(documentType) || documentType == "SEÇİNİZ...")
            {
                MessageBox.Show("Lütfen bir fatura türü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur
            }

            string beginDate = dateTimeStartDate.Value.ToString("yyyy-MM-dd");
            string endDate = dateTimeFinishDate.Value.ToString("yyyy-MM-dd");

            var invoiceRequest = new WayBillRequest
            {
                startDate = beginDate, //"2023-12-01",
                endDate = endDate,//"2024-01-01",
                waybillTypes = new[] { documentType }
            };

            waybillResponse = await ApiManager.PostAsync<WayBillRequest, WayBillModelResponse>(Configuration.GetUrl() + "management/waybills-for-erp", invoiceRequest);

            List<DisplayInvoiceInfo> displayInfoList = waybillResponse.data.Select(header => new DisplayInvoiceInfo
            {
                Number = header.number,
                Date = header.date.ToShortDateString(),
                DocumentNumber = header.documentNumber,
                CustomerCode = header.customerCode,
                CustomerName = header.customerName,
                DiscountTotal = header.discountTotal.ToString(),
                VatTotal = header.vatTotal.ToString(),
                GrossTotal = header.grossTotal.ToString()
            }).ToList();

            dataGridInvoiceList.DataSource = displayInfoList;
        }

        private void comboboxInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxInvoiceType.SelectedItem != null)
            {
                if (comboboxInvoiceType.SelectedItem.ToString() == "SEÇİNİZ...")
                {
                    MessageBox.Show("Lütfen bir değer seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    documentType = string.Empty;
                }
                else
                {
                    documentType = comboboxInvoiceType.SelectedItem.ToString();
                }
            }
            else
            {
                documentType = string.Empty;
            }
        }

        private void bttnSendWaybill_Click(object sender, EventArgs e)
        {

        }

        private void anaMenuyeDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }

        private void WaybillForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
