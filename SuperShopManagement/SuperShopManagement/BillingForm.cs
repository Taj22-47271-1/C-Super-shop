using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SuperShopManagement
{
    public class BillingForm : Form
    {
        TextBox txtSearch, txtPrice, txtQuantity, txtTotal;
        TextBox txtNewProduct, txtNewPrice, txtUpdatePrice;
        ListBox lstProducts, listReceipt;
        Button btnAdd, btnPrint, btnClear, btnAddNewProduct, btnHistory, btnEdit, btnRemove, btnUpdatePrice;
        Label lblGrandTotal, lblRole;

        string selectedProduct = "";
        double grandTotal = 0;
        string receiptText = "";
        string userRole = "";

        List<SaleItem> currentSales = new List<SaleItem>();
        PrintDocument printDocument = new PrintDocument();

        public BillingForm(string role)
        {
            userRole = role;

            Text = "Super Shop Billing System";
            Size = new Size(1200, 740);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label()
            {
                Text = "SUPER SHOP MANAGEMENT",
                Location = new Point(0, 0),
                Size = new Size(1200, 60),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblRole = new Label()
            {
                Text = "Login As: " + userRole,
                Location = new Point(40, 70),
                Size = new Size(250, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            Label lblSearch = new Label()
            {
                Text = "Search Product",
                Location = new Point(40, 110),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtSearch = new TextBox()
            {
                Location = new Point(170, 110),
                Size = new Size(350, 30)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            Label lblProductList = new Label()
            {
                Text = "Product List",
                Location = new Point(40, 150),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            lstProducts = new ListBox()
            {
                Location = new Point(170, 150),
                Size = new Size(350, 140)
            };
            lstProducts.SelectedIndexChanged += LstProducts_SelectedIndexChanged;

            Label lblPrice = new Label()
            {
                Text = "Price",
                Location = new Point(40, 310),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtPrice = new TextBox()
            {
                Location = new Point(170, 310),
                Size = new Size(350, 30),
                ReadOnly = true
            };

            Label lblQty = new Label()
            {
                Text = "Quantity",
                Location = new Point(40, 360),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtQuantity = new TextBox()
            {
                Location = new Point(170, 360),
                Size = new Size(350, 30)
            };
            txtQuantity.TextChanged += CalculateTotal;

            Label lblTotal = new Label()
            {
                Text = "Total",
                Location = new Point(40, 410),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtTotal = new TextBox()
            {
                Location = new Point(170, 410),
                Size = new Size(350, 30),
                ReadOnly = true
            };

            btnAdd = new Button()
            {
                Text = "Add Sale",
                Location = new Point(170, 460),
                Size = new Size(120, 35),
                BackColor = Color.Green,
                ForeColor = Color.White
            };
            btnAdd.Click += BtnAdd_Click;

            btnClear = new Button()
            {
                Text = "Clear",
                Location = new Point(310, 460),
                Size = new Size(120, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White
            };
            btnClear.Click += BtnClear_Click;

            Label lblAdmin = new Label()
            {
                Text = "Admin Product Control",
                Location = new Point(40, 520),
                Size = new Size(250, 25),
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            Label lblNewProduct = new Label()
            {
                Text = "New Product",
                Location = new Point(40, 555),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtNewProduct = new TextBox()
            {
                Location = new Point(170, 555),
                Size = new Size(350, 30)
            };

            Label lblNewPrice = new Label()
            {
                Text = "New Price",
                Location = new Point(40, 600),
                Size = new Size(120, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtNewPrice = new TextBox()
            {
                Location = new Point(170, 600),
                Size = new Size(350, 30)
            };

            btnAddNewProduct = new Button()
            {
                Text = "Add Product",
                Location = new Point(170, 645),
                Size = new Size(140, 35),
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White
            };
            btnAddNewProduct.Click += BtnAddNewProduct_Click;

            Label lblUpdatePrice = new Label()
            {
                Text = "Update Price",
                Location = new Point(330, 645),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            txtUpdatePrice = new TextBox()
            {
                Location = new Point(430, 645),
                Size = new Size(90, 30)
            };

            btnUpdatePrice = new Button()
            {
                Text = "Update",
                Location = new Point(530, 645),
                Size = new Size(90, 35),
                BackColor = Color.Orange,
                ForeColor = Color.White
            };
            btnUpdatePrice.Click += BtnUpdatePrice_Click;

            Label lblReceipt = new Label()
            {
                Text = "Receipt / Selected Sell Items",
                Location = new Point(650, 75),
                Size = new Size(300, 25),
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            listReceipt = new ListBox()
            {
                Location = new Point(650, 105),
                Size = new Size(500, 330)
            };
            listReceipt.SelectedIndexChanged += ListReceipt_SelectedIndexChanged;

            lblGrandTotal = new Label()
            {
                Text = "Grand Total: 0 Tk",
                Location = new Point(650, 455),
                Size = new Size(350, 30),
                Font = new Font("Arial", 14, FontStyle.Bold)
            };

            btnEdit = new Button()
            {
                Text = "Edit Sale",
                Location = new Point(650, 505),
                Size = new Size(120, 40),
                BackColor = Color.Orange,
                ForeColor = Color.White
            };
            btnEdit.Click += BtnEdit_Click;

            btnRemove = new Button()
            {
                Text = "Remove Sale",
                Location = new Point(790, 505),
                Size = new Size(120, 40),
                BackColor = Color.Firebrick,
                ForeColor = Color.White
            };
            btnRemove.Click += BtnRemove_Click;

            btnPrint = new Button()
            {
                Text = "Print",
                Location = new Point(650, 565),
                Size = new Size(120, 40),
                BackColor = Color.Teal,
                ForeColor = Color.White
            };
            btnPrint.Click += BtnPrint_Click;

            btnHistory = new Button()
            {
                Text = "History",
                Location = new Point(790, 565),
                Size = new Size(120, 40),
                BackColor = Color.Purple,
                ForeColor = Color.White
            };
            btnHistory.Click += BtnHistory_Click;

            printDocument.PrintPage += PrintDocument_PrintPage;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblRole,
                lblSearch, txtSearch,
                lblProductList, lstProducts,
                lblPrice, txtPrice,
                lblQty, txtQuantity,
                lblTotal, txtTotal,
                btnAdd, btnClear,
                lblAdmin,
                lblNewProduct, txtNewProduct,
                lblNewPrice, txtNewPrice,
                btnAddNewProduct,
                lblUpdatePrice, txtUpdatePrice, btnUpdatePrice,
                lblReceipt, listReceipt,
                lblGrandTotal,
                btnEdit, btnRemove,
                btnPrint, btnHistory
            });

            LoadProducts();
            ApplyRolePermission();
        }

        private void ApplyRolePermission()
        {
            if (userRole == "Employee")
            {
                txtNewProduct.Enabled = false;
                txtNewPrice.Enabled = false;
                txtUpdatePrice.Enabled = false;

                btnAddNewProduct.Enabled = false;
                btnUpdatePrice.Enabled = false;
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
            }
        }

        private void LoadProducts(string search = "")
        {
            lstProducts.Items.Clear();
            DataTable dt = DatabaseHelper.GetProducts(search);

            foreach (DataRow row in dt.Rows)
                lstProducts.Items.Add(row["ProductName"].ToString());
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void LstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProducts.SelectedItem == null) return;

            selectedProduct = lstProducts.SelectedItem.ToString();
            DataTable dt = DatabaseHelper.GetProducts(selectedProduct);

            if (dt.Rows.Count > 0)
            {
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtUpdatePrice.Text = dt.Rows[0]["Price"].ToString();
            }

            CalculateTotal(sender, e);
        }

        private void CalculateTotal(object sender, EventArgs e)
        {
            if (double.TryParse(txtPrice.Text, out double price) &&
                double.TryParse(txtQuantity.Text, out double qty))
            {
                txtTotal.Text = (price * qty).ToString();
            }
            else
            {
                txtTotal.Text = "";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (selectedProduct == "" || txtQuantity.Text == "" || txtTotal.Text == "")
            {
                MessageBox.Show("Please select product and enter quantity.");
                return;
            }

            double price = Convert.ToDouble(txtPrice.Text);
            double qty = Convert.ToDouble(txtQuantity.Text);
            double total = Convert.ToDouble(txtTotal.Text);

            long saleId = DatabaseHelper.AddSale(selectedProduct, price, qty, total);

            currentSales.Add(new SaleItem()
            {
                SaleId = saleId,
                ProductName = selectedProduct,
                Price = price,
                Quantity = qty,
                Total = total
            });

            RefreshReceipt();

            txtSearch.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtTotal.Clear();
            txtUpdatePrice.Clear();
            selectedProduct = "";
            LoadProducts();
        }

        private void BtnAddNewProduct_Click(object sender, EventArgs e)
        {
            string name = txtNewProduct.Text.Trim();

            if (name == "" || txtNewPrice.Text == "")
            {
                MessageBox.Show("Enter product name and price.");
                return;
            }

            if (!double.TryParse(txtNewPrice.Text, out double price))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            bool added = DatabaseHelper.AddProduct(name, price);

            if (added)
            {
                MessageBox.Show("Product added successfully.");
                txtNewProduct.Clear();
                txtNewPrice.Clear();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Product already exists.");
            }
        }

        private void BtnUpdatePrice_Click(object sender, EventArgs e)
        {
            if (selectedProduct == "")
            {
                MessageBox.Show("Select a product first.");
                return;
            }

            if (!double.TryParse(txtUpdatePrice.Text, out double newPrice))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            bool updated = DatabaseHelper.UpdateProductPrice(selectedProduct, newPrice);

            if (updated)
            {
                MessageBox.Show("Price updated successfully.");
                txtPrice.Text = newPrice.ToString();
                LoadProducts(txtSearch.Text);
            }
            else
            {
                MessageBox.Show("Price update failed.");
            }
        }

        private void ListReceipt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listReceipt.SelectedIndex < 0) return;

            SaleItem item = currentSales[listReceipt.SelectedIndex];

            selectedProduct = item.ProductName;
            txtPrice.Text = item.Price.ToString();
            txtQuantity.Text = item.Quantity.ToString();
            txtTotal.Text = item.Total.ToString();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listReceipt.SelectedIndex < 0)
            {
                MessageBox.Show("Select receipt item first.");
                return;
            }

            if (!double.TryParse(txtQuantity.Text, out double newQty))
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            int index = listReceipt.SelectedIndex;
            SaleItem item = currentSales[index];

            item.Quantity = newQty;
            item.Total = item.Price * newQty;

            DatabaseHelper.UpdateSale(item.SaleId, item.Quantity, item.Total);
            RefreshReceipt();

            MessageBox.Show("Sale item updated.");
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listReceipt.SelectedIndex < 0)
            {
                MessageBox.Show("Select receipt item first.");
                return;
            }

            int index = listReceipt.SelectedIndex;
            SaleItem item = currentSales[index];

            DatabaseHelper.DeleteSale(item.SaleId);
            currentSales.RemoveAt(index);

            RefreshReceipt();

            txtPrice.Clear();
            txtQuantity.Clear();
            txtTotal.Clear();
            selectedProduct = "";

            MessageBox.Show("Sale item removed.");
        }

        private void RefreshReceipt()
        {
            listReceipt.Items.Clear();
            grandTotal = 0;
            receiptText = "";

            foreach (SaleItem item in currentSales)
            {
                string line = item.ProductName +
                              " | Price: " + item.Price +
                              " | Qty: " + item.Quantity +
                              " | Total: " + item.Total + " Tk";

                listReceipt.Items.Add(line);
                receiptText += line + "\n";
                grandTotal += item.Total;
            }

            lblGrandTotal.Text = "Grand Total: " + grandTotal + " Tk";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtTotal.Clear();
            txtUpdatePrice.Clear();
            selectedProduct = "";
            LoadProducts();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (listReceipt.Items.Count == 0)
            {
                MessageBox.Show("No product added.");
                return;
            }

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            string printText = "";
            printText += "SUPER SHOP RECEIPT\n";
            printText += "-----------------------------\n";
            printText += receiptText;
            printText += "-----------------------------\n";
            printText += "Grand Total: " + grandTotal + " Tk\n";
            printText += "Thank you for shopping!\n";

            e.Graphics.DrawString(printText, new Font("Arial", 12), Brushes.Black, new PointF(100, 100));
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            Form historyForm = new Form();
            historyForm.Text = "Sales History";
            historyForm.Size = new Size(750, 550);
            historyForm.StartPosition = FormStartPosition.CenterScreen;

            DateTimePicker datePicker = new DateTimePicker()
            {
                Location = new Point(20, 20),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Short
            };

            DataGridView grid = new DataGridView()
            {
                Location = new Point(20, 60),
                Size = new Size(690, 350),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            Label lblDayTotal = new Label()
            {
                Location = new Point(20, 430),
                Size = new Size(500, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            void LoadHistory()
            {
                DataTable dt = DatabaseHelper.GetSalesByDate(datePicker.Value);
                grid.DataSource = dt;

                double dayTotal = 0;
                foreach (DataRow row in dt.Rows)
                    dayTotal += Convert.ToDouble(row["Total"]);

                lblDayTotal.Text = "Selected Day Total Sell: " + dayTotal + " Tk";
            }

            datePicker.ValueChanged += (s, ev) => LoadHistory();

            historyForm.Controls.Add(datePicker);
            historyForm.Controls.Add(grid);
            historyForm.Controls.Add(lblDayTotal);

            LoadHistory();
            historyForm.ShowDialog();
        }

        public class SaleItem
        {
            public long SaleId { get; set; }
            public string ProductName { get; set; }
            public double Price { get; set; }
            public double Quantity { get; set; }
            public double Total { get; set; }
        }
    }
}