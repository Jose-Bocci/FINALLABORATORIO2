using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLaboratorio2_JoséBocci
{
    class Habitacion
    {

        private string codigoHabitacion;

        public Habitacion()
        {

        }
        public Habitacion(string codigoHabitacion)
        {
            this.codigoHabitacion = codigoHabitacion;
        }
        public void SetCodigo(string codigo)
        {
            this.codigoHabitacion = codigo;
        }
    }
}
