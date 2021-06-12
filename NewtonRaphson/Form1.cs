using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octave.NET.Tools;
using System.Threading;

namespace NewtonRaphson
{
    public partial class Form1 : Form
    {
        Resolver resolver;
    
           

        System.Windows.Forms.Timer Tick;
        bool InfoDisplayed=true;
        public Form1()
        {
            
            InitializeComponent();
            resolver = new Resolver();
            MessageBox.Show("La inicialización del programa puede tomar mucho tiempo, por favor espere.");
            Tick = new System.Windows.Forms.Timer();
            Tick.Interval = 500;
            Tick.Tick += Timer_Tick;
            Tick.Start();
           
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (resolver.OctaveIsReady())
            {
                button1.Text = "Calcular raiz";
                button1.Enabled = true;
                textBoxF.Enabled = true;
                textBoxDx.Enabled = true;
                textBoxError.Enabled = true;
                textBoxXi.Enabled = true ;
                button2.Enabled = true;
                button2.Text = "Ayuda con la derivada!";
            }
            else 
            {
                button1.Text = "Loading...";
                labelF.Text = "f(x)= ...";
                labelResult.Text = "...";
                labelDx.Text = "f '(x) = ...";
                button1.Enabled = false;

                textBoxF.Enabled = false;
                textBoxDx.Enabled = false;
                textBoxError.Enabled = false;
                textBoxXi.Enabled = false;
                button2.Enabled = false;
                button2.Text = "Loading...";

            }
                
            if (resolver.OctaveIsReady()&&!InfoDisplayed)//Func.PackageLoaded()
            {
                Tick.Stop();
                InfoDisplayed = true;
                resolver.xi = float.Parse(textBoxXi.Text);
                resolver.acceptableError = float.Parse(textBoxError.Text);
                resolver.Func.StrFunc = textBoxF.Text;
                resolver.Func.UserFuncDerivative = textBoxDx.Text;
                try
                {
                    labelF.Text = "f(x)= " + resolver.function;
                    labelF.Text = "f(x)= " + resolver.function;
                    labelResult.Text = "Xi= " + resolver.Result().ToString();
                    labelDx.Text = "f '(x)= " + resolver.Func.Derivative();
                }
                catch(Exception ex)
                {
                    labelResult.Text = "Error";
                    labelDx.Text = "f '(x)= " + resolver.Func.Derivative();
                    MessageBox.Show(ex.Message);
                }
                button1.Text = "Calcular raiz";
                button1.Enabled = true;
                Tick.Start();
           
                
            }
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Loading...";
            labelF.Text = "";
            labelResult.Text = "";
            labelDx.Text = "";
            button1.Enabled = false;
            
            InfoDisplayed = false;
            MessageBox.Show("Esto puede tomar mucho tiempo porfavor espere.");
        
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string text = "";
            MessageBox.Show("Debido al formato de la derivada posible que ésta no pueda apreciarse correctamente en todos los casos.");
            try
            {
                resolver.Func.Clear();
                labelF.Text = "f(x) = " + textBoxF.Text;
                if (textBoxF.Text != null)
                    resolver.Func.StrFunc = textBoxF.Text;
                text = resolver.Func.Derivative();
                             
                labelDx.Text = text;
                

            }
            catch (Exception ex)
            {
                labelDx.Text = "f '(x)= ...";
                MessageBox.Show(ex.Message);
            }
        }


    }
}
