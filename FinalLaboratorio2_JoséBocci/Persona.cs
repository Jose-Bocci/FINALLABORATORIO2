using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLaboratorio2_JoséBocci
{
    abstract class Persona
    {
        protected String nombre, apellido, direccion;
        protected int dni;
        protected long numero;

        protected string Nombre { get => nombre; set => nombre = value; }
        protected string Apellido { get => apellido; set => apellido = value; }
        protected string Direccion { get => direccion; set => direccion = value; }
        protected long Numero { get => numero; set => numero = value; }
        protected int Dni { get => dni; set => dni = value; }

    }
}
