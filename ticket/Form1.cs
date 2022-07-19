using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace ticket
{
    public partial class Form1 : Form
    {
        //string imagen = @"C:\Users\usuario\Pictures\prueba.jpeg";
        string imagen = @"C:\Users\Gerardo\Pictures\prueba.jpeg";
        public Form1()
        {
            InitializeComponent();
        }

        private void productoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.ticketDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            productoTextBox.Focus();
            // TODO: esta línea de código carga datos en la tabla 'ticketDataSet.producto' Puede moverla o quitarla según sea necesario.
            this.productoTableAdapter.Fill(this.ticketDataSet.producto);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Insertar
        {

            if (string.IsNullOrEmpty(productoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
            {
                MessageBox.Show("Inserte el nombre del producto");
                productoTextBox.Focus();
            }
            else if (string.IsNullOrEmpty(precioTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
            {
                MessageBox.Show("Inserte el precio del producto");
                precioTextBox.Focus();
            }
            else if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
            {
                MessageBox.Show("Inserte la descripción del producto");
                descripcionTextBox.Focus();
            }
            else if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
            {
                MessageBox.Show("Inserte el código del producto");
                codigoTextBox.Focus();
            }
            else
            {
                this.productoTableAdapter.Insertar(productoTextBox.Text, precioTextBox.Text, descripcionTextBox.Text, codigoTextBox.Text);
                this.productoTableAdapter.Fill(this.ticketDataSet.producto);
            }
        }
            

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.productoTableAdapter.Buscar(ticketDataSet.producto, codigoTextBox.Text);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.productoTableAdapter.Eliminar(codigoTextBox.Text);
            this.productoTableAdapter.Fill(this.ticketDataSet.producto);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            this.productoTableAdapter.Actualizar(productoTextBox.Text, precioTextBox.Text, descripcionTextBox.Text, codigoTextBox.Text, codigoTextBox.Text);
            this.productoTableAdapter.Fill(this.ticketDataSet.producto);
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw codigodebarra = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = codigodebarra.Draw(codigoTextBox.Text, 40);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += Imprimir;
            printDocument1.Print();
        }

        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point);
            int width = 200;
            int y = 20;
            e.Graphics.DrawString(productoTextBox.Text, font, Brushes.Black, new Rectangle(10, y += 20, width, 20));
            //e.Graphics.DrawString(pictureBox1, font, Brushes.Black, new Rectangle(0, y += 20, width, 20));
            //e.Graphics.DrawString(pictureBox1, font, Brushes.Black, new Rectangle(0, y += 20, width, 20));
            //e.Graphics.DrawString(pictureBox1, font, Brushes.Black, new Rectangle(0, y += 20, width, 20));
            //e.Graphics.DrawString(pictureBox1, font, Brushes.Black, new Rectangle(0, y += 20, width, 20));

            Image img = Image.FromFile(imagen);
            e.Graphics.DrawImage(img, new Rectangle(10, y += 20, 180, 180));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.FileName = @"C:\Users\Gerardo\Pictures\prueba.jpeg";
            saveFileDialog1.Filter = "JPEG|*.jpeg";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                string varimg = saveFileDialog1.FileName;
                Bitmap varbmp = new Bitmap(pictureBox1.Image);
                varbmp.Save(varimg, ImageFormat.Jpeg);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de que decea salir? ", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // --------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------- //
        // --------------------Inicio de los eventos para insertar -------------------- //
        // --------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------- //

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(productoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el nombre del producto");
                    productoTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(precioTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el precio del producto");
                    precioTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte la descripción del producto");
                    descripcionTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el código del producto");
                    codigoTextBox.Focus();
                }
                else
                {
                    this.productoTableAdapter.Insertar(productoTextBox.Text, precioTextBox.Text, descripcionTextBox.Text, codigoTextBox.Text);
                    this.productoTableAdapter.Fill(this.ticketDataSet.producto);
                }
            }


        }

        private void productoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(productoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el nombre del producto");
                    productoTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(precioTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el precio del producto");
                    precioTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte la descripción del producto");
                    descripcionTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el código del producto");
                    codigoTextBox.Focus();
                }
                else
                {
                    this.productoTableAdapter.Insertar(productoTextBox.Text, precioTextBox.Text, descripcionTextBox.Text, codigoTextBox.Text);
                    this.productoTableAdapter.Fill(this.ticketDataSet.producto);
                }
            }
        }

        private void precioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(productoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el nombre del producto");
                    productoTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(precioTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el precio del producto");
                    precioTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte la descripción del producto");
                    descripcionTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el código del producto");
                    codigoTextBox.Focus();
                }
                else
                {
                    this.productoTableAdapter.Insertar(productoTextBox.Text, precioTextBox.Text, descripcionTextBox.Text, codigoTextBox.Text);
                    this.productoTableAdapter.Fill(this.ticketDataSet.producto);
                }
            }
        }

        private void descripcionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(productoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el nombre del producto");
                    productoTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(precioTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el precio del producto");
                    precioTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte la descripción del producto");
                    descripcionTextBox.Focus();
                }
                else if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()) || string.IsNullOrWhiteSpace(productoTextBox.Text.Trim()))
                {
                    MessageBox.Show("Inserte el código del producto");
                    codigoTextBox.Focus();
                }
                else
                {
                    this.productoTableAdapter.Insertar(productoTextBox.Text, precioTextBox.Text, descripcionTextBox.Text, codigoTextBox.Text);
                    this.productoTableAdapter.Fill(this.ticketDataSet.producto);
                }
            }
        }

        // --------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------- //
        // --------------------Fin de los eventos para insertar -------------------- //
        // --------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------- //

    }
}
