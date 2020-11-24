using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLaboratorio2_JoséBocci
{
    class Cliente : Persona
    {
        private Habitacion habitacion;
        
        public Cliente() { }

        public Cliente(string nombre, string apellido, string direccion, long numero, int dni, Habitacion habitacion)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Direccion = direccion;
            this.Numero = numero;
            this.Dni = dni;
            this.habitacion = habitacion;
        }

    }
}
