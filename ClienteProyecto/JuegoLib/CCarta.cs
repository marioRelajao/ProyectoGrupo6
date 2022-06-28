using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JuegoLib
{
    public class CCarta
    {
        int id;
        string tipo;
        
        //Constructores
        public CCarta()
        {

        }
        public CCarta(int id)//Constructor que se le pasa un ID y este añade el tipo que es
        {
            this.id = id;
            if (id < 4)
            {
                this.tipo = "bomba";
            }
            else if (id >3 && id < 10)
            {
                this.tipo = "defuse";
            }
            else if (id>9 && id < 15)
            {
                this.tipo = "nope";
            }
            else if (id>14 && id < 20)
            {
                this.tipo = "futuro";
            }
            else if (id > 19 && id < 24)
            {
                this.tipo = "skip";
            }
            else if (id > 23 && id < 28)
            {
                this.tipo = "ataca";
            }
            else if (id > 27 && id < 32)
            {
                this.tipo = "shuffle";
            }
            else if (id > 31 && id < 36)
            {
                this.tipo = "favor";
            }
            else if (id > 35 && id < 40)
            {
                this.tipo = "potato";
            }
            else if (id > 41 && id < 46)
            {
                this.tipo = "rainbow";
            }
            else if (id > 45 && id < 50)
            {
                this.tipo = "taco";
            }
            else if (id > 49 && id < 54)
            {
                this.tipo = "melon";
            }
            else if (id > 53 && id < 58)
            {
                this.tipo = "barba";
            }
        }

        //Metodos
        public string RetornaTipo()
        {
            return this.tipo;
        }
    }
}
