using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Console.WriteLine("\n********* Türünü belirlemek istediğiniz banknotun belirtilen değerlerini giriniz. ********* \n\n");

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
            Console.WriteLine("\n\n");

            Banknote banknote = new Banknote(Varyans, Carpiklik, basiklik, entropi);

            banknote.VeriListesiKur(); // Veri Listemizi Kuruyoruz.
            Console.WriteLine("********* Türünü belirlemek istediğiniz banknotunuza en yakın banknotların verileri aşağıda listelenmiştir. *********\n");
            Thread.Sleep(2000);

            banknote.TurBelirleme(banknote, k, banknote.BanknoteArray); // Girilen banknotun türünü belirleyen fonksiyonu çağırıyoruz.

            Console.Write("Test verisi için bir 'k' degeri giriniz :  ");
            k = System.Int32.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);

            Console.WriteLine("");
            Thread.Sleep(3500);
            banknote.VeriTestIslemi(k); // Test Verilerinin Sonuçlarını burada çağırdık.
                                         
            Console.WriteLine("*****************************************************************\n\nTüm veri seti yazdırılıyor...");
            Thread.Sleep(2000);
            banknote.ListeyiYazdir(banknote.BanknoteArray);// Son olarak tüm veriyi yazdırdık.

            Console.WriteLine("Çıkmak için herhangi bir tuşa basınız.");
            Console.ReadKey();
        } 
    }
}
