using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace idk
{
    public partial class Form1 : Form
    {
        private Dictionary<string, int> extraPrices = new Dictionary<string, int>
        {
            { "Skoda Fabia", 19640 },
            { "Ford Focus", 25990 },
            { "Toyota Land Cruiser", 21850 },
            { "Ferrari", 30990 }
        };
        private Dictionary<string, int> checkboxPrices = new Dictionary<string, int>
{
            { "Feature 1", 500 },
            { "Feature 2", 1000 },
            { "Feature 3", 1500 }
};
        public Form1()
        {
            InitializeComponent();
            InitializeCarModels();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            buttonReset.Click += buttonReset_Click;
        }
        private void InitializeCarModels()
        {
            comboBox1.Items.Clear();

            comboBox1.Items.AddRange(extraPrices.Keys.ToArray());
            buttonCalculate.Click += buttonCalculate_Click;



        }
        private Dictionary<string, string> carImages = new Dictionary<string, string>
{
          { "Skoda Fabia", "img/skoda-fabia-monte-carlo-tech-07-04-13.jpg" },
          { "Ford Focus", "img/s-l400.jpg" },
    { "Toyota Land Cruiser", "img/151902413.jpg" },
    { "Ferrari", "images/1c5438761d631e2f699a82f72ce5aef3.jpg" }
};
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string basePath = Path.Combine(Application.StartupPath, "img");
            Console.WriteLine($"Base path: {basePath}"); // Debug output

            if (comboBox1.SelectedItem != null)
            {
                string selectedModel = comboBox1.SelectedItem.ToString();

                string imagePath = string.Empty;

                switch (selectedModel)
                {
                    case "Skoda Fabia":
                        label1.Text = $"{extraPrices["Skoda Fabia"]:N0}лв";
                        imagePath = Path.Combine(basePath, "skoda-fabia-monte-carlo-tech-07-04-13.jpg");
                        break;
                    case "Ford Focus":
                        label1.Text = $"{extraPrices["Ford Focus"]:N0}лв";
                        imagePath = Path.Combine(basePath, "s-l400.jpg");
                        break;
                    case "Toyota Land Cruiser":
                        label1.Text = $"{extraPrices["Toyota Land Cruiser"]:N0}лв";
                        imagePath = Path.Combine(basePath, "151902413.jpg");
                        break;
                    case "Ferrari":
                        label1.Text = $"{extraPrices["Ferrari"]:N0}лв";
                        imagePath = Path.Combine(basePath, "1c5438761d631e2f699a82f72ce5aef3.jpg");
                        break;
                    default:
                        label1.Text = string.Empty;
                        pictureBox1.Image = null;
                        return;
                }

                Console.WriteLine($"Image path: {imagePath}"); 
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
            

                comboBox1.Enabled = false;
                comboBox1.Text = selectedModel;
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            int basePrice = 0;

            
            if (comboBox1.SelectedItem != null)
            {
                string selectedModel = comboBox1.SelectedItem.ToString();
                if (extraPrices.TryGetValue(selectedModel, out int price))
                {
                    basePrice = price;
                }
            }

            
            int extraPrice = 0;
            if (checkBox1.Checked)
            {
                extraPrice += checkboxPrices["Feature 1"];
            }
            if (checkBox2.Checked)
            {
                extraPrice += checkboxPrices["Feature 2"];
            }
            if (checkBox3.Checked)
            {
                extraPrice += checkboxPrices["Feature 3"];
            }

            
            int totalPriceBeforeDiscount = basePrice + extraPrice;

           
            double discountAmount = 0;
            double totalPriceAfterDiscount = totalPriceBeforeDiscount;

            
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
            {
                discountAmount = totalPriceBeforeDiscount * 0.01; 
                totalPriceAfterDiscount = totalPriceBeforeDiscount - discountAmount;
            }

            
            labelPrice.Text = $"Цена с избрани екстри: {totalPriceBeforeDiscount:N0}лв";

            
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
            {
                labelDiscount.Text = $"Отстъпка(1%): {discountAmount:N0}лв";
            }
            else
            {
                labelDiscount.Text = string.Empty; 
            }

            
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked)
            {
                labelTotal.Text = $"Общо: {totalPriceAfterDiscount:N0}лв";
            }
            else
            {
                labelTotal.Text = $"Общо: {totalPriceBeforeDiscount:N0}лв"; 
            }
        }
        private void InitializeCheckBoxes()
        {
            checkBox1.Text = $"Feature 1 (+${checkboxPrices["Feature 1"]:N0})";
            checkBox2.Text = $"Feature 2 (+${checkboxPrices["Feature 2"]:N0})";
            checkBox3.Text = $"Feature 3 (+${checkboxPrices["Feature 3"]:N0})";
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = -1;
            comboBox1.Text = string.Empty;


            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;


            label1.Text = string.Empty;
            labelPrice.Text = string.Empty;
            labelDiscount.Text = string.Empty; 
            labelTotal.Text = string.Empty;

            pictureBox1.Image = null;


            comboBox1.Enabled = true;


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
