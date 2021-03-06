﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinalLaboratorio2_JoséBocci
{
    public partial class RegistroDeHuespedes : Form
    {
        #region variables
        ArrayList ClientesGerente = new ArrayList();
        ArrayList ClientesEmpleado1 = new ArrayList();
        ArrayList ClientesEmpleado2 = new ArrayList();
        ArrayList DNIREGISTRO = new ArrayList();
        ArrayList HABITACIONREGISTRO = new ArrayList();
        Cliente C1;
        Habitacion H1;
        String nombre, apellido, direccion, usuario, contraseña, codigoHabitacion;
        int dni, noches, n, personal;
        double precioFinal, precio;
        long numero;
        #endregion
        public RegistroDeHuespedes()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Sesiones();
        }
        #region dgvCellClick
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = e.RowIndex;
        }
        private void dgvClientes2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = e.RowIndex;
        }
        #endregion
        #region cBxChange
        private void cBxTurista_CheckedChanged(object sender, EventArgs e)
        {
            if (cBxFrecuente.Checked == true)
            {
                cBxFrecuente.Checked = false;
            }
        }

        private void cBxFrecuente_CheckedChanged(object sender, EventArgs e)
        {
            if (cBxTurista.Checked == true)
            {
                cBxTurista.Checked = false;
            }
        }
        #endregion
        #region btnClicks&keyPress
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            usuario = txtNombre.Text;
            contraseña = txtApellido.Text;
            if (usuario == "Miguel" && contraseña == "123456")
            {
                personal = 1;
                Mostrar();
            }
            else if (usuario == "Tomas" && contraseña == "TT456")
            {
                personal = 2;
                Mostrar();
            }
            else if (usuario == "Samuel" && contraseña == "Geren2000")
            {
                personal = 3;
                Mostrar();
            }
            else
            {
                MessageBox.Show("Error al Ingresar al Sistema. \n Intente nuevamente.", "ERROR", MessageBoxButtons.OK);
                txtNombre.Text = "";
                txtApellido.Text = "";
            }
        }

        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            Limpiar();
            Ocultar();
            btnIngresar.Visible = true;
            Sesiones();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            switch (personal) 
            {
                case 1:
                    if (ClientesEmpleado1.Count > 0)
                    {
                        try {
                            BorradoDeRegistro(personal);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Debe seleccionar un huésped de la lista.", "Error", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No tiene registrados Clientes.", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case 2:
                    if (ClientesEmpleado2.Count > 0)
                    {
                        try {
                            BorradoDeRegistro(personal);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Debe seleccionar un huésped de la lista.", "Error", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No tiene registrados Clientes.", "Error", MessageBoxButtons.OK);
                    }
                    break;
            
            }
        }
        
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Calcular();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Calcular();
            string preferencia = "";
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtLugarDeResidencia.Text == "" || txtDNI.Text == "" || txtTelefono.Text == "" || Utiles.validarCampo(txtDNI.Text, "int") == false || Utiles.validarCampo(txtTelefono.Text, "long") == false || txtHabitacion.Text == "")
            {
                MessageBox.Show("Error al ingresar los datos de Registro. Hay campos vacíos y/o formato de datos erróneo. Corrija", "Error", MessageBoxButtons.OK);
            }
            else if (ComprobarDNI(Convert.ToInt32(txtDNI.Text)) == 1)
            {
                MessageBox.Show("DNI de Cliente ya se encuentra registrado.", "Error");
            }
            else if (ComprobarHabitacion(txtHabitacion.Text) == 1)
            {

                MessageBox.Show("La Habitación ya se encuentra asignada.", "Error");

            }else
            {
                nombre = txtNombre.Text;
                apellido = txtApellido.Text;
                direccion = txtLugarDeResidencia.Text;
                dni = Convert.ToInt32(txtDNI.Text);
                DNIREGISTRO.Add(dni);
                numero = Convert.ToInt64(txtTelefono.Text);
                codigoHabitacion = txtHabitacion.Text;
                HABITACIONREGISTRO.Add(codigoHabitacion);

                verificarNochesyHabitacion();

            if (cBxFrecuente.Checked == true)
            {
                preferencia = "Frecuente";
            }
            if (cBxTurista.Checked == true)
            {
                preferencia = "Turista";
            }
            if (cBxTurista.Checked == false && cBxFrecuente.Checked == false)
            {
                MessageBox.Show("Error al seleccionar tipo de cliente. Corrija.", "Error");
            }

                if (comboBxNoches.SelectedIndex != -1 && (cBxFrecuente.Checked == true || cBxTurista.Checked))
                {
                    switch (personal)
                    {
                        case 1:
                            H1 = new Habitacion(codigoHabitacion);
                            C1 = new Cliente(nombre, apellido, direccion, numero, dni,H1);
                            ClientesEmpleado1.Add(C1);
                            ClientesGerente.Add(C1);
                            n = dgvClientes1.Rows.Add();
                            dgvClientes1.Rows[n].Cells[0].Value = nombre;
                            dgvClientes1.Rows[n].Cells[1].Value = apellido;
                            dgvClientes1.Rows[n].Cells[2].Value = dni;
                            dgvClientes1.Rows[n].Cells[3].Value = noches;
                            dgvClientes1.Rows[n].Cells[4].Value = precioFinal;
                            dgvClientes1.Rows[n].Cells[5].Value = preferencia;
                            dgvClientes1.Rows[n].Cells[6].Value = codigoHabitacion;
                            n = dgvClientesGerente.Rows.Add();
                            dgvClientesGerente.Rows[n].Cells[0].Value = nombre;
                            dgvClientesGerente.Rows[n].Cells[1].Value = apellido;
                            dgvClientesGerente.Rows[n].Cells[2].Value = dni;
                            dgvClientesGerente.Rows[n].Cells[3].Value = precioFinal;
                            dgvClientesGerente.Rows[n].Cells[4].Value = preferencia;
                            dgvClientesGerente.Rows[n].Cells[5].Value = noches;
                            dgvClientesGerente.Rows[n].Cells[6].Value = numero;
                            dgvClientesGerente.Rows[n].Cells[7].Value = comboBxHabitacion.SelectedItem;
                            dgvClientesGerente.Rows[n].Cells[8].Value = codigoHabitacion;
                            break;
                        case 2:
                            H1 = new Habitacion(codigoHabitacion);
                            C1 = new Cliente(nombre, apellido, direccion, numero, dni, H1);
                            ClientesEmpleado2.Add(C1);
                            ClientesGerente.Add(C1);
                            n = dgvClientes2.Rows.Add();
                            dgvClientes2.Rows[n].Cells[0].Value = nombre;
                            dgvClientes2.Rows[n].Cells[1].Value = apellido;
                            dgvClientes2.Rows[n].Cells[2].Value = dni;
                            dgvClientes2.Rows[n].Cells[3].Value = noches;
                            dgvClientes2.Rows[n].Cells[4].Value = precioFinal;
                            dgvClientes2.Rows[n].Cells[5].Value = preferencia;
                            dgvClientes2.Rows[n].Cells[6].Value = codigoHabitacion;
                            n = dgvClientesGerente.Rows.Add();
                            dgvClientesGerente.Rows[n].Cells[0].Value = nombre;
                            dgvClientesGerente.Rows[n].Cells[1].Value = apellido;
                            dgvClientesGerente.Rows[n].Cells[2].Value = dni;
                            dgvClientesGerente.Rows[n].Cells[3].Value = precioFinal;
                            dgvClientesGerente.Rows[n].Cells[4].Value = preferencia;
                            dgvClientesGerente.Rows[n].Cells[5].Value = noches;
                            dgvClientesGerente.Rows[n].Cells[6].Value = numero;
                            dgvClientesGerente.Rows[n].Cells[7].Value = comboBxHabitacion.SelectedItem;
                            dgvClientesGerente.Rows[n].Cells[8].Value = codigoHabitacion;
                            break;                    
                    }
                    Limpiar();
                }
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void comboBxHabitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            comboBxHabitacion.Text = "-SELECCIONE-";
            comboBxHabitacion.SelectedIndex = -1;
        }

        private void comboBxNoches_KeyPress(object sender, KeyPressEventArgs e)
        {
            comboBxNoches.Text = "-SELECCIONE-";
            comboBxNoches.SelectedIndex = -1;
        }


        #endregion
        #region Métodos
        //Buscar y borrar
        private void BorradoDeRegistro(int personal)
        {
            int dnicliente, dnilista, dniborrar, indice;
            string habitacion;
            if (personal == 1)
            {
                for (int i = 0; i < ClientesEmpleado1.Count; i++)
                {
                    for (int j = 0; j < ClientesGerente.Count; j++)
                    {
                        dnicliente = Convert.ToInt32(dgvClientes1.Rows[i].Cells[2].Value.ToString());
                        dnilista = Convert.ToInt32(dgvClientesGerente.Rows[j].Cells[2].Value.ToString());
                        if (dnicliente == dnilista)
                        {
                            habitacion = dgvClientesGerente.Rows[j].Cells[8].Value.ToString();
                            dniborrar = Convert.ToInt32(dgvClientesGerente.Rows[j].Cells[2].Value.ToString());
                            dgvClientesGerente.Rows.RemoveAt(j);
                            j = ClientesGerente.Count;
                            i = ClientesEmpleado1.Count;
                            if (DNIREGISTRO.Contains(dniborrar))
                            {
                                indice = DNIREGISTRO.IndexOf(dniborrar);
                                DNIREGISTRO.RemoveAt(indice);
                            }
                            if (HABITACIONREGISTRO.Contains(habitacion))
                            {
                                indice = HABITACIONREGISTRO.IndexOf(habitacion);
                                HABITACIONREGISTRO.RemoveAt(indice);
                            }
                        }
                    }
                }
                if (n != -1)
                {
                    dgvClientes1.Rows.RemoveAt(n);
                }
                ClientesEmpleado1.RemoveAt(n);
            }
            else
            {
                for (int i = 0; i < ClientesEmpleado2.Count; i++)
                {
                    for (int j = 0; j < ClientesGerente.Count; j++)
                    {
                        dnicliente = Convert.ToInt32(dgvClientes2.Rows[i].Cells[2].Value.ToString());
                        dnilista = Convert.ToInt32(dgvClientesGerente.Rows[j].Cells[2].Value.ToString());
                        if (dnicliente == dnilista)
                        {
                            habitacion = dgvClientesGerente.Rows[j].Cells[8].Value.ToString();
                            dniborrar = Convert.ToInt32(dgvClientesGerente.Rows[j].Cells[2].Value.ToString());
                            dgvClientesGerente.Rows.RemoveAt(j);
                            j = ClientesGerente.Count;
                            i = ClientesEmpleado2.Count;
                            if (DNIREGISTRO.Contains(dniborrar))
                            {
                                indice = DNIREGISTRO.IndexOf(dniborrar);
                                DNIREGISTRO.RemoveAt(indice);
                            }
                            if (HABITACIONREGISTRO.Contains(habitacion))
                            {
                                indice = HABITACIONREGISTRO.IndexOf(habitacion);
                                HABITACIONREGISTRO.RemoveAt(indice);
                            }
                        }
                    }
                }
                if (n != -1)
                {
                    dgvClientes2.Rows.RemoveAt(n);
                }
                ClientesEmpleado2.RemoveAt(n);
            }
        }
        //verifica que los DNI no se repitan
        private int ComprobarDNI(int dni)
        {
            int i, verificacion = 0;
            for (i = 0;i < DNIREGISTRO.Count; i++)
            {
                if (dni == (int) DNIREGISTRO[i])
                {
                    verificacion = 1;
                }
                else
                {
                    verificacion = 0;
                }
            }
            return verificacion;
        }
        //Verifica que el código de habitación no se repita
        private int ComprobarHabitacion(string codigo)
        {
            int i, verificacion = 0;
            for (i = 0; i < HABITACIONREGISTRO.Count; i++)
            {
                if (codigo == (string) HABITACIONREGISTRO[i])
                {
                    verificacion = 1;
                }
                else
                {
                    verificacion = 0;
                }
            }
            return verificacion;
        }
        //Limpia los campos de entrada
        private void Limpiar()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtLugarDeResidencia.Text = "";
            txtTelefono.Text = "";
            txtHabitacion.Text = "";
            lblPrecioCantidad.Text = "";
            rBtn5.Checked = false;
            rBtn10.Checked = false;
            rBtn20.Checked = false;
            cBxFrecuente.Checked = false;
            cBxTurista.Checked = false;
            comboBxHabitacion.SelectedIndex = -1;
            comboBxHabitacion.Text = "-SELECCIONE-";
            comboBxNoches.SelectedIndex = -1;
            comboBxNoches.Text = "-SELECCIONE-";
        }
        //Verifica la seleccion de noches y tipo de habitacion
        private void verificarNochesyHabitacion()
        {
            switch (comboBxHabitacion.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("No seleccionó tipo de habitación. Corrija.", "ERROR", MessageBoxButtons.OK);
                    break;
                case 0:
                    precio = 4500;
                    break;
                case 1:
                    precio = 5000;
                    break;
                case 2:
                    precio = 6000;
                    break;
            }
            switch (comboBxNoches.SelectedIndex)
            {
                case -1:
                    MessageBox.Show("No seleccionó la duración de la estadía. Corrija.", "ERROR", MessageBoxButtons.OK);
                    break;
                case 0:
                    noches = 3;
                    break;
                case 1:
                    noches = 7;
                    break;
                case 2:
                    noches = 15;
                    break;
                case 3:
                    noches = 30;
                    break;
            }
        }
        //Oculta los elementos para el inicio de sesion
        private void Ocultar()
        {
            lblDNI.Visible = false;
            lblDescuento.Visible = false;
            lblEtiqueta.Visible = false;
            lblLugarDeResidencia.Visible = false;
            lblNoches.Visible = false;
            lblPrecio.Visible = false;
            lblPrecioCantidad.Visible = false;
            lblPrecioDoble.Visible = false;
            lblPrecioFamiliar.Visible = false;
            lblPrecioPorNoche.Visible = false;
            lblPrecioSimple.Visible = false;
            lblTelefonoDeContacto.Visible = false;
            lblTipoDeCliente.Visible = false;
            lblTipoDeHabitacion.Visible = false;
            lblPersonal.Visible = false;
            lblHabitacion.Visible = false;
            txtDNI.Visible = false;
            txtLugarDeResidencia.Visible = false;
            txtTelefono.Visible = false;
            txtHabitacion.Visible = false;
            btnBorrar.Visible = false;
            btnCalcular.Visible = false;
            btnLimpiar.Visible = false;
            btnRegistrar.Visible = false;
            btnCerrarSesión.Visible = false;
            rBtn10.Visible = false;
            rBtn5.Visible = false;
            rBtn20.Visible = false;
            comboBxHabitacion.Visible = false;
            comboBxNoches.Visible = false;
            dgvClientes1.Visible = false;
            dgvClientesGerente.Visible = false;
            dgvClientes2.Visible = false;
            cBxTurista.Visible = false;
            cBxFrecuente.Visible = false;
        }
        //Muestra los elementos luego del inicio de sesion
        private void Mostrar()
        {
            lblNombre.Text = "Nombre";
            lblApellido.Text = "Apellido";
            txtNombre.Text = "";
            txtApellido.Text = "";
            btnCerrarSesión.Visible = true;
            lblDNI.Visible = true;
            lblDescuento.Visible = true;
            lblEtiqueta.Visible = true;
            lblLugarDeResidencia.Visible = true;
            lblNoches.Visible = true;
            lblPrecio.Visible = true;
            lblPrecioCantidad.Visible = true;
            lblPrecioDoble.Visible = true;
            lblPrecioFamiliar.Visible = true;
            lblPrecioPorNoche.Visible = true;
            lblPrecioSimple.Visible = true;
            lblTelefonoDeContacto.Visible = true;
            lblTipoDeCliente.Visible = true;
            lblTipoDeHabitacion.Visible = true;
            lblEtiqueta.Visible = true;
            lblHabitacion.Visible = true;
            txtDNI.Visible = true;
            txtLugarDeResidencia.Visible = true;
            txtTelefono.Visible = true;
            txtHabitacion.Visible = true;
            btnBorrar.Visible = true;
            btnCalcular.Visible = true;
            btnLimpiar.Visible = true;
            btnRegistrar.Visible = true;
            rBtn10.Visible = true;
            rBtn5.Visible = true;
            rBtn20.Visible = true;
            comboBxHabitacion.Visible = true;
            comboBxNoches.Visible = true;
            cBxTurista.Visible = true;
            cBxFrecuente.Visible = true;
            btnIngresar.Visible = false;


            switch (personal)
            {
                case 1:
                    lblPersonal.Visible = true;
                    lblPersonal.Text = "Miguel";
                    dgvClientes1.Visible = true;
                    break;
                case 2:
                    lblPersonal.Visible = true;
                    lblPersonal.Text = "Tomas";
                    dgvClientes2.Visible = true;
                    break;
                case 3:
                    Ocultar();
                    lblNombre.Visible = false;
                    lblApellido.Visible = false;
                    txtNombre.Visible = false;
                    txtApellido.Visible = false;
                    lblPersonal.Visible = true;
                    lblPersonal.Text = "Samuel";
                    btnCerrarSesión.Visible = true;
                    dgvClientesGerente.Visible = true;
                    break;
            }
            
        }
        //Inicio de sesion
        private void Sesiones()
        {
            Ocultar();
            lblNombre.Visible = true;
            lblApellido.Visible = true;
            txtApellido.Visible = true;
            txtNombre.Visible = true;
            lblNombre.Text = "Usuario";
            lblApellido.Text = "Contraseña";

        }
        //Calcula el precio de la estadia
        private void Calcular()
        {
            verificarNochesyHabitacion();

            double precioCalculo = 0;
            double precioDescuento = 0;

            precio = precio * noches;

            if (rBtn5.Checked == true)
            {
                precioDescuento = precio * 0.05;
            }
            if (rBtn10.Checked == true)
            {
                precioDescuento = precio * 0.1;
            }
            if (rBtn20.Checked == true)
            {
                precioDescuento = precio * 0.2;
            }
            if (cBxFrecuente.Checked == true)
            {
                precioCalculo = precio * 0.05;
            }

            precio = precio - precioCalculo - precioDescuento;

            if (comboBxNoches.SelectedIndex == -1 || comboBxHabitacion.SelectedIndex == -1)
            {
                lblPrecioCantidad.Text = "Error al Calcular";
                lblEtiqueta.Visible = false;
            }
            else
            {
                lblEtiqueta.Visible = true;
                string precioCalculado = precio.ToString();

                lblPrecioCantidad.Text = precioCalculado;
                precioFinal = precio;
            }
        }
        #endregion
    }
}