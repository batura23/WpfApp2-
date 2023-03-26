using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2ПРАВИЛЬНЫЙ.Classes
{
    public class Sale
    {
        decimal Карандаш;
        decimal Тетрадь;
        decimal Альбом;

        public Sale(decimal Карандаш, decimal Тетрадь, decimal Альбом)
        {
            this.Карандаш = Карандаш;
            this.Тетрадь = Тетрадь;
            this.Альбом = Альбом;


        }
        public decimal Расчет(bool карандаш, bool тетрадь, bool альбом)
        {
            decimal result = 0;
            if (карандаш)
            {
                result += Карандаш;



            }
            if (тетрадь)
            {
                result += Тетрадь;



            }
            if (альбом)
            {
                result += Альбом;



            }

            return result;
        }
    }
}
