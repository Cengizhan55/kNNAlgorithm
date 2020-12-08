using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNProject
{
    class Program
    {

        static double Varyans;
        static double Carpiklik;
        static double basiklik;
        static double entropi;
        static int k;
        static void Main(string[] args)
        {
            Console.Write("Varyans degerini giriniz :  ");
            Varyans=Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.Write("Carpiklik degerini giriniz :  ");
            Carpiklik= Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.Write("Basiklik degerini giriniz :  ");
            basiklik= Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.Write("Entropi degerini giriniz :  ");
            entropi= Double.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
            Console.Write("k degerini giriniz :  ");
            k= System.Int32.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);






            Banknote banknote = new Banknote(Varyans,Carpiklik,basiklik,entropi);
            banknote.ListeyiKur();


            banknote.TestListesiOlustur(k);
            Console.ReadKey();
            
            
            //banknote.TurBelirleme(banknote,k,banknote.BanknoteArray);
            Console.ReadKey();


          
        }
       
    }
}
