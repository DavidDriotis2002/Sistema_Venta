﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmCategoria : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;


        public FrmCategoria()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre de la categoria");
        }

        //Mostrar mensaje de confirmacion

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mostrar mensaje de error

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Limpiar todos los controles del formulario

        private void Limpiar()
        {
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdcategoria.Text = string.Empty;
        }

        //Habilitar los controles del formulario

        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtIdcategoria.ReadOnly = !valor;
        }

        //Habilitar los botones

        private void Botones()
        {
            if(this.IsNuevo || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }

        }
        //Metodo para ocultar columnas

        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = true;
            this.dataListado.Columns[1].Visible = false;
        }

        //Metodo mostrar

        private void Mostrar()
        {
            this.dataListado.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Metodo buscar nombre

        private void BuscarNombre()
        {
            if (RdbLike.Checked)
            {
                this.dataListado.DataSource = NCategoria.BuscarNombre(this.txtBuscar.Text);
                this.OcultarColumnas();
                lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
            }
            else
            {
                RdbActivarBoton.Enabled = true;
            }

        }
        //Metodo para buscar nombre por palabra completa
        private void BuscarNombreExacto()
        {
            if (RdbActivarBoton.Checked)
            {
                this.dataListado.DataSource = NCategoria.BuscarNombreExtacto(this.txtBuscar.Text);
                lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
            }
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            this.Top = 0;
            this.Left = 0;

            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string BNombre = txtBuscar.Text;
            if (RdbActivarBoton.Checked)
            {

                this.BuscarNombreExacto();
                this.dataListado.DataSource = NCategoria.BuscarNombreExtacto(this.txtBuscar.Text);
                lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
            }
            else
            {
                this.BuscarNombre();
                this.dataListado.DataSource = NCategoria.BuscarNombre(this.txtBuscar.Text);
                this.OcultarColumnas();
                lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
            }

        }

        private void º(object sender, EventArgs e)
        {

            if (RdbLike.Checked)
            {
                    this.BuscarNombre();
            }
            else
            {
                txtBuscar.Text.Equals("nombre");
                this.BuscarNombreExacto();
            }


            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if(this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtNombre,"Ingrese un nombre");

                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = NCategoria.Insertar(this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim());
                    }
                    else
                    {
                        rpta = NCategoria.Editar(Convert.ToInt32(this.txtIdcategoria.Text), this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim());

                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se insertó de forma correcta el registro");
                        }
                        else
                        {
                            this.MensajeOk("Se actualizó de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdcategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            this.tabControl1.SelectedIndex = 1;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdcategoria.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe seleccionar primero el registro a modificar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string Codigo;
                List<string> codigosEliminar = new List<string>();
                string Rpta = "";

                foreach (DataGridViewRow row in dataListado.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        Codigo = Convert.ToString(row.Cells[1].Value);
                        codigosEliminar.Add(Codigo);
                    }
                }

                if (codigosEliminar.Count <= 0)
                {
                    MensajeError("No existe ningún elemento seleccionado, por favor seleccionar como mínimo 1");
                    return;
                }
                else {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Realmente desea eliminar los registros", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (Opcion == DialogResult.OK)
                    {

                        for (int x = 0; x < codigosEliminar.Count; x++)
                        {
                            Rpta = NCategoria.Eliminar(Convert.ToInt32(codigosEliminar[x]));

                            if (Rpta == "OK")
                            {
                                Console.WriteLine("Correcto, se eliminó el código: " + codigosEliminar[x]);
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                                break;
                            }
                        }

                        if (codigosEliminar.Count == 1)
                        {
                            this.MensajeOk("Se eliminó correctamente el registro");
                        }
                        else {
                            this.MensajeOk("Se eliminaron correctamente los registros");
                        }
                        
                    }

                    //Mostramos nuestro dataListado ya actualizado
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void RdbLike_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbLike.Checked)
            {
                btnBuscar.Enabled = false;
            }
            else
            {
                btnBuscar.Enabled = true;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            

            //Buscar solo en el datagridview
            if (rdbFiltroDgv.Checked)
            {
                string Keyword = txtBuscar.Text;
                (dataListado.DataSource as DataTable).DefaultView.RowFilter = string.Format("nombre LIKE '%"+Keyword+"%'");
            }
            else
            {
                BuscarNombre();
            }
        }
    }
}
  