using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNProject
{
     class Banknote
        {
        private double Varyans;
        private double Carpiklik;
        private double basiklik;
        private double entropi;
        private int tur;
        private double yakinlik;
        int TestTur1;
        int TestTur0;
        Banknote[] banknoteArray;
        Banknote[] valuesArray;
        Banknote[] testArray;
 
        public Banknote(double Varyans, double Carpiklik, double basiklik, double entropi,int tur=-1)
        {
            this.Varyans1 = Varyans;
            this.Carpiklik1 = Carpiklik;
            this.Basiklik = basiklik;
            this.Entropi = entropi;
            this.tur = tur;
        }

        public int TurBelirleme(Banknote banknote, int k,Banknote[] BanknoteArray)
        {
            string sonuc = "";
            Banknote[] myBanknoteList = kNNKarsilastir(banknote, k,BanknoteArray);

            int Tur1 = 0;
            int Tur0 = 0;

            for (int i = 0; i < k; i++)
            {
                if (myBanknoteList[i].Tur == 0)   { Tur0++; }
                else {Tur1++;}

            }
            if (Tur1 > Tur0)   {banknote.tur = 1;}
            else
            {
                banknote.tur = 0;
                if (Tur1 == Tur0) {

                    banknote.tur = myBanknoteList[0].tur;
                
                }
            }
            Console.Write("\tVaryans \t" + "Çarpıklık\t" + "Basıklık\t" + "Entropi \t" + "Tür");
            Console.WriteLine("");

            for (int i = 0; i < k; i++)
            {
                Console.WriteLine(i+
                    "\t" + String.Format("{0:0.000}",myBanknoteList[i].Varyans1)+ 
                    "\t\t" + String.Format("{0:0.000}", myBanknoteList[i].Carpiklik) + 
                    "\t\t" + String.Format("{0:0.000}", myBanknoteList[i].basiklik) +
                    "\t\t" + String.Format("{0:0.000}", myBanknoteList[i].entropi) + 
                    "\t\t" + (myBanknoteList[i].tur));

            }

            if (banknote.tur == 0)  {sonuc = "Sahtedir!";}
            else {sonuc = "Gerçektir!";}

            Console.WriteLine("" +
                "\nVerilan Banknotun değerlerine göre bu para : " + sonuc
                +"\n*******************************************************************************\n");

            return banknote.tur;
        }

        public void ListeyiYazdir(Banknote[] banknote)
        {       
            Console.Write("\tVaryans \t" + "Çarpıklık\t" + "Basıklık\t" + "Entropi \t" + "Tür\n");
          
            for (int i = 0; i < banknote.Length; i++)
            {
                Console.WriteLine(i +
                    "\t" + String.Format("{0:0.000}", banknote[i].Varyans1) + 
                    "\t\t" + String.Format("{0:0.000}", banknote[i].Carpiklik) +
                    "\t\t" + String.Format("{0:0.000}", banknote[i].basiklik) +
                    "\t\t" + String.Format("{0:0.000}", banknote[i].Entropi) +
                    "\t\t" + (banknote[i].tur));
            }
            Console.WriteLine("*******************************************************************************");
        }
        public void VeriListesiKur()
        {
            string[] lines = File.ReadAllLines("data_banknote_authentication.txt");
            BanknoteArray = new Banknote[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] dizi = lines[i].Split(',').ToArray();

                BanknoteArray[i] = (new Banknote(
                   Math.Round(Double.Parse(dizi[0], System.Globalization.CultureInfo.InvariantCulture),4),
                   Math.Round(Double.Parse(dizi[1], System.Globalization.CultureInfo.InvariantCulture),4),
                   Math.Round(Double.Parse(dizi[2], System.Globalization.CultureInfo.InvariantCulture),4),
                   Math.Round(Double.Parse(dizi[3], System.Globalization.CultureInfo.InvariantCulture),4),
                    System.Int32.Parse(dizi[4], System.Globalization.CultureInfo.InvariantCulture)));
            }
        }

        public void VeriTestIslemi(int k)
        {
             TestTur1 = 0;
             TestTur0 = 0;
            double TruthValue = 0;

            Banknote[] tempArray=new Banknote[banknoteArray.Length-200];
            testArray = new Banknote[200];

            string[] lines = File.ReadAllLines("data_banknote_authentication.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] dizi = lines[i].Split(',').ToArray();
                int TestTur = System.Int32.Parse(dizi[4]);

                if (TestTur == 0) { TestTur0++;}
                else { TestTur1++; }
            
            }
            for (int x = 0; x<TestTur0 - 100; x++) {

                tempArray[x] = banknoteArray[x];
                
            };

            for (int a = TestTur0 - 100; a < TestTur0; a++) {

                testArray[a - TestTur0 + 100] = banknoteArray[a];

            }
            for (int y =100; y < 200; y++) {

                testArray[y] = banknoteArray[banknoteArray.Length-200+y];

            }
            for (int w = TestTur0; w < banknoteArray.Length-100; w++) {

                tempArray[w - 100] = banknoteArray[w];
                
            }
            for (int c = 0; c < 200; c++) {

               if(testArray[c].tur== TurBelirleme(testArray[c], k, tempArray))  {TruthValue++;}

            }
             Console.WriteLine("Doğruluk başarı oranı:   %" +(TruthValue/200f)*100);
        }

        public Banknote[] kNNKarsilastir(Banknote banknote, int k,Banknote[] BanknoteArray)
        {
            int count = k;
            this.ValuesArray = new Banknote[k];

            int arraylength = BanknoteArray.Length;

            for (int i = 0; i < arraylength; i++)
            {
                double toplamYakinlik = 0;
                toplamYakinlik += Math.Pow(banknote.Varyans1 - BanknoteArray[i].Varyans, 2);
                toplamYakinlik += Math.Pow(banknote.Entropi - BanknoteArray[i].Entropi, 2);
                toplamYakinlik += Math.Pow(banknote.Basiklik - BanknoteArray[i].Basiklik, 2);
                toplamYakinlik += Math.Pow(banknote.Carpiklik1 - BanknoteArray[i].Carpiklik, 2);
                toplamYakinlik += Math.Sqrt(toplamYakinlik);
                Banknote suankiBanknote = new Banknote(Math.Round(BanknoteArray[i].Varyans,4), Math.Round(BanknoteArray[i].Basiklik, 4), Math.Round(BanknoteArray[i].Carpiklik,4), Math.Round(BanknoteArray[i].Entropi, 4), BanknoteArray[i].tur);
                suankiBanknote.Yakinlik = toplamYakinlik;
               
                if (count == 0)
                {
                    if (this.ValuesArray[k - 1].Yakinlik > suankiBanknote.Yakinlik)
                    {
                        this.ValuesArray[k - 1] = suankiBanknote;
                        MySelectionSort(this.ValuesArray, k);

                    }
                }
                else
                {
                    this.ValuesArray[count - 1] = suankiBanknote;
                    count--;

                    if (count == 0)
                    {
                        MySelectionSort(this.ValuesArray, k);
                    }
                }
            }
            return this.ValuesArray;
        }
        public void MySelectionSort(Banknote[] valuesArray, int k)
        {
            for (int j = 0; j < k - 1; j++)
            {
                int index = j;
                Banknote minYakın = this.ValuesArray[j];
                for (int a = j + 1; a < k; a++)
                {
                    if (minYakın.Yakinlik > this.ValuesArray[a].Yakinlik)
                    {
                        minYakın = this.ValuesArray[a];
                        index = a;
                    }
                }
                Banknote temp = this.ValuesArray[j];
                this.ValuesArray[j] = this.ValuesArray[index];
                this.ValuesArray[index] = temp;
            }
        }
        public double Varyans1 { get => Varyans; set => Varyans = value; }
        public double Carpiklik1 { get => Carpiklik; set => Carpiklik = value; }
        public double Basiklik { get => basiklik; set => basiklik = value; }
        public double Entropi { get => entropi; set => entropi = value; }
        public int Tur { get => tur; set => tur = value; }
        public double Yakinlik { get => yakinlik; set => yakinlik = value; }
        internal Banknote[] BanknoteArray { get => banknoteArray; set => banknoteArray = value; }
        internal Banknote[] ValuesArray { get => valuesArray; set => valuesArray = value; }
    }
}
