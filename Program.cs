using System;

namespace Kurierzy
{
    public interface IPaczka
    {
        void Spakuj();
    }

    public interface IKurier
    {
        void Dostarcz();
    }

    public class MałaPaczka : IPaczka
    {
        public void Spakuj()
        {
            Console.WriteLine("Spakowano małą paczkę.");
        }
    }

    public class DużaPaczka : IPaczka
    {
        public void Spakuj()
        {
            Console.WriteLine("Spakowano dużą paczkę.");
        }
    }

    public class DHLKurier : IKurier
    {
        public void Dostarcz()
        {
            Console.WriteLine("Dostarczono przez kuriera DHL.");
        }
    }

    public class UPSKurier : IKurier
    {
        public void Dostarcz()
        {
            Console.WriteLine("Dostarczono przez kuriera UPS.");
        }
    }

    public interface IFabrykaLogistyki
    {
        IPaczka UtwórzPaczkę();
        IKurier UtwórzKuriera();
    }

    public class FabrykaLogistykiPolska : IFabrykaLogistyki
    {
        public IPaczka UtwórzPaczkę()
        {
            return new MałaPaczka();
        }

        public IKurier UtwórzKuriera()
        {
            return new DHLKurier();
        }
    }

    public class FabrykaLogistykiUSA : IFabrykaLogistyki
    {
        public IPaczka UtwórzPaczkę()
        {
            return new DużaPaczka();
        }

        public IKurier UtwórzKuriera()
        {
            return new UPSKurier();
        }
    }

    public class ZarządzaniePrzesyłkami
    {
        private IFabrykaLogistyki fabrykaLogistyki;
        private static ZarządzaniePrzesyłkami _instancja;

        private ZarządzaniePrzesyłkami() { }

        public static ZarządzaniePrzesyłkami Instancja
        {
            get
            {
                if (_instancja == null)
                {
                    _instancja = new ZarządzaniePrzesyłkami();
                }
                return _instancja;
            }
        }

        public void PrzyjmijZamówienie(string lokalizacja)
        {
            switch (lokalizacja)
            {
                case "Polska":
                    fabrykaLogistyki = new FabrykaLogistykiPolska();
                    break;
                case "USA":
                    fabrykaLogistyki = new FabrykaLogistykiUSA();
                    break;
                default:
                    throw new ArgumentException("Nieobsługiwana lokalizacja.");
            }

            var paczka = fabrykaLogistyki.UtwórzPaczkę();
            var kurier = fabrykaLogistyki.UtwórzKuriera();
            paczka.Spakuj();
            kurier.Dostarcz();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie("Polska");
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie("USA");
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie("Polska");
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie("USA");
        }
    }
}
