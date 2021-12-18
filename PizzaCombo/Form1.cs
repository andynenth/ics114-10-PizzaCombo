using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaCombo
{
    public partial class Form1 : Form
    {
        private float totalPrice;
        private string selectedPizza;
        private string size;
        private string toppings;
        private string diet;
        

        public Form1()
        {
            InitializeComponent();
            totalPrice = 0f;
            
        }

        public bool IsPizzaChecked()
        {
            return listBox1.SelectedIndex != -1;
        }

        private void PizzaSize_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPizzaChecked())CalulatePizzaPrice();
        }

        private void Toppings_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPizzaChecked()) CalulatePizzaPrice();
        }

        private void CalulatePizzaPrice()
        {
            totalPrice = GetPizza();
            totalPrice += AddToppings();
            ChooseSize();

            txtTotal.Text = String.Format("{0:C}", totalPrice);

            txtOrder.Text = selectedPizza + Environment.NewLine
                + "Size: " + size + Environment.NewLine
                + "Toppings: " + toppings;
        }


        public void ChooseSize()
        {
            if (rdoSmallSize.Checked)
            {
                totalPrice -= 2;
                size = rdoSmallSize.Text;
            }
            else if (rdoLargeSize.Checked)
            {
                totalPrice += 5;
                size = rdoMediumSize.Text;
            }
            else
            {
                size = rdoLargeSize.Text;
            }
        }

        public float GetPizza()
        {
            switch (selectedPizza)
            {
                case "Cheese":
                    return 10f;
                case "Neapolitan":
                    return 10f;
                case "Margherita":
                    return 10f;
                case "Calzone":
                    return 12.5f;
                case "Stromboli":
                    return 12.5f;
                case "Deep dish":
                    return 12.5f;
                case "Marinara":
                    return 12.5f;
                case "Hawaiian":
                    return 12.5f;
                case "Lahma Bi Afeen":
                    return 13f;
                case "M&L Special":
                    return 14f;
                default:
                    return 0;
            }
        }

        public float AddToppings()
        {
            toppings = "";
            float toppingPrice = 0;
            if (cbMushroom.Checked)
            {
                toppingPrice += 2;
                toppings += cbMushroom.Text.Remove(cbMushroom.Text.IndexOf("(")-1) + Environment.NewLine;
            }
            if (cbBlackOlive.Checked)
            {
                toppingPrice += 3;
                toppings += cbBlackOlive.Text.Remove(cbBlackOlive.Text.IndexOf("(") - 1) + Environment.NewLine;
            }
            if (cbPepperoni.Checked)
            {
                toppingPrice += 5;
                toppings += cbPepperoni.Text.Remove(cbPepperoni.Text.IndexOf("(") - 1) + Environment.NewLine;
            }

            if (toppingPrice == 0)
            {
                toppings += "none";
            }
            return toppingPrice;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPizza = listBox1.SelectedItem.ToString();
            CalulatePizzaPrice();
        }

        private void cboDietary_SelectedIndexChanged(object sender, EventArgs e)
        {
            diet = cboDietary.SelectedItem.ToString();
            ArrayList pizzaType = new ArrayList { "Cheese", "Neapolitan", "Margherita", "Calzone", "Stromboli", "Deep dish", "Marinara", "Hawaiian", "Lahma Bi Afeen", "M&L Special" };
            switch (diet)
            {
                case "Vegan":
                case "Vegetarian":
                    pizzaType.Remove("Hawaiian");
                    pizzaType.Remove("Calzone");
                    pizzaType.Remove("Stromboli");
                    pizzaType.Remove("Lahma Bi Afeen");
                    pizzaType.Remove("M&L Special");
                    break;
                case "Non-GMO":
                    pizzaType.Remove("Stromboli");
                    break;
                case "Gluten free":
                case "Lactose":
                case "Paleo":
                    pizzaType.Remove("Neapolitan");
                    pizzaType.Remove("Margherita");
                    pizzaType.Remove("Deep dish");
                    pizzaType.Remove("Cheese");
                    break;
                case "100 mile":
                case "Kosher":
                    pizzaType.Remove("Hawaiian");
                    pizzaType.Remove("Stromboli");
                    break;                    
            }
            pizzaType.Sort();
            listBox1.Items.Clear();   // Delete all the items already in the ListBox.
            listBox1.Items.AddRange((object[])pizzaType.ToArray()); // Convert the ArrayList to an array.
        }
    }
}