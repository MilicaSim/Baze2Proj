using Bioskop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bioskop.ViewModel
{
    public class KartaViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Karta kartaMD;
        private Karta selektovanaKarta;
        private BindingList<Karta> karte;
        

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }
        private BindingList<int> posjetioci;
        private BindingList<int> prikazivanja;

        public KartaViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            KartaMD = new Karta();
            SelektovanaKarta = new Karta(); ;
            Posjetioci = GetAllPosjetilac();
            Prikazivanja = GetAllPrikazivanja();
            karte = GetAll();
        }
        public BindingList<int> GetAllPosjetilac()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Posjetilacs;

                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {
                    vs.Add(v.IdPosjetioca);
                }
                return vs;
            }
        }
        public BindingList<int> GetAllPrikazivanja()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Prikazujes;

                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {
                    vs.Add(v.IdPrikazivanja);
                }
                return vs;
            }
        }
        public BindingList<Karta> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Kartas;
                BindingList<Karta> vs = new BindingList<Karta>();
                foreach (var v in vl)
                {
                    vs.Add(v);

                }
                return vs;
            }
        }
        public void OnDodaj()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    #region Validation
                    if (KartaMD.Cijena <= 0)
                    {
                        MessageBox.Show("Polje cijena mora biti popunjeno!");
                        return;
                    }
                    else if (KartaMD.PosjetilacIdPosjetioca <= 0)
                    {
                        MessageBox.Show("Posjetilac mora biti odabran!");
                        return;
                    }
                    else if (KartaMD.PrikazujeIdPrikazivanja < 0)
                    {
                        MessageBox.Show("Prikazivanje mora biti odabranp!");
                        return;
                    }
                    #endregion
                    access.Kartas.Add(KartaMD);


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    Karte = GetAll();
                    KartaMD = new Karta();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Postoji objekat sa ovom kombinacijom kljuceva: broj sedista+broj vagona!");
                }

            }
        }
        public void OnModifikuj()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    access.Kartas.Where(n => n.IdKarte == SelektovanaKarta.IdKarte).FirstOrDefault().PosjetilacIdPosjetioca = KartaMD.PosjetilacIdPosjetioca;
                    access.Kartas.Where(n => n.IdKarte == SelektovanaKarta.IdKarte).FirstOrDefault().PrikazujeIdPrikazivanja = KartaMD.PrikazujeIdPrikazivanja;
                    //menadzer


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Karte = GetAll();

                }
                catch (Exception)
                {
                    MessageBox.Show("Ne mozete modifikovati objekat jer niste odabrali objekat!");
                }
            }
        }
        public void OnObrisi()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    access.Kartas.Remove(access.Kartas.FirstOrDefault(n => n.IdKarte == SelektovanaKarta.IdKarte));
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Karte = GetAll();
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer niste odabrali objekat!");
                }
                catch (TargetException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer niste odabrali objekat!");
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Ne mozete obrisati objekat jer je uvezan objekat!");
                }

            }
        }

        public void OnNav(object show)
        {

            string showView1 = (string)show;
            switch (showView1)
            {
                case "dodaj":
                    IsVisibleDodaj = true;
                    IsVisibleModifikuj = false;
                    IsVisibleObrisi = false;
                    IsVisibleStek = true;
                    KartaMD = new Karta();
                    break;
                case "obrisi":
                    IsVisibleDodaj = false;
                    IsVisibleModifikuj = false;
                    IsVisibleObrisi = true;
                    IsVisibleStek = false;
                    break;
                case "modifikuj":
                    IsVisibleDodaj = false;
                    IsVisibleModifikuj = true;
                    IsVisibleObrisi = false;
                    IsVisibleStek = true;
                    KartaMD = SelektovanaKarta;

                    break;
            }
        }

        #region Property
        public Karta KartaMD
        {
            get { return kartaMD; }
            set
            {

                if (value != kartaMD)
                {
                    kartaMD = value;
                    OnPropertyChanged("KartaMD");
                }
            }
        }
        public Karta SelektovanaKarta
        {
            get { return selektovanaKarta; }
            set
            {

                if (value != selektovanaKarta)
                {
                    selektovanaKarta = value;
                    OnPropertyChanged("SelektovanaKarta");
                    KartaMD = SelektovanaKarta;

                }
            }
        }
        public BindingList<Karta> Karte
        {
            get { return karte; }
            set
            {

                if (value != karte)
                {
                    karte = value;
                    OnPropertyChanged("Karte");
                }
            }
        }

        public BindingList<int> Posjetioci
        {
            get { return posjetioci; }
            set
            {

                if (value != posjetioci)
                {
                    posjetioci = value;
                    OnPropertyChanged("Posjetioci");
                }
            }
        }
        public BindingList<int> Prikazivanja
        {
            get { return prikazivanja; }
            set
            {

                if (value != prikazivanja)
                {
                    prikazivanja = value;
                    OnPropertyChanged("Prikazivanja");
                }
            }
        }
        public bool IsVisibleDodaj
        {
            get { return isVisibleDodaj; }
            set
            {
                isVisibleDodaj = value;
                OnPropertyChanged("IsVisibleDodaj");
            }
        }
        public bool IsVisibleStek
        {
            get { return isVisibleStek; }
            set
            {
                isVisibleStek = value;
                OnPropertyChanged("IsVisibleStek");
            }
        }
        public bool IsVisibleObrisi
        {
            get { return isVisibleObrisi; }
            set
            {
                isVisibleObrisi = value;
                OnPropertyChanged("IsVisibleObrisi");
            }
        }
        public bool IsVisibleModifikuj
        {
            get { return isVisibleModifikuj; }
            set
            {
                isVisibleModifikuj = value;
                OnPropertyChanged("IsVisibleModifikuj");
            }
        }
        
        #endregion

    }
}
