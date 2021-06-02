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
using System.Windows.Data;
using System.Windows.Input;

namespace Bioskop.ViewModel
{
    public class RepertoarViewModel : BindableBase
    {

        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Repertoar repertoarMD;
        private Repertoar selektovaniRepertoar;
        private BindingList<Repertoar> repertoari;
        

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }
        private Dictionary<int, string> menadzeri;

        public RepertoarViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            RepertoarMD = new Repertoar();
            SelektovaniRepertoar = new Repertoar(); ;
            Menadzeri = GetAllMenadzeri();
            Repertoari = GetAll();
        }
        public Dictionary<int, string> GetAllMenadzeri()
        {
            using (var access = new ModelContainer())
            {

                var vl = access.Radniks_Menadzer;

                Dictionary<int, string> vs = new Dictionary<int, string>();
                foreach (var v in vl)
                {
                    vs[v.IdRadnika] = v.Ime + " " + v.Prezime;
                }
                return vs;
            }
        }
        public BindingList<Repertoar> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Repertoars;
                BindingList<Repertoar> vs = new BindingList<Repertoar>();
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

                    if (RepertoarMD.Naziv == null)
                    {
                        MessageBox.Show("Polje naziv mora biti popunjeno!");
                        return;
                    }
                    if (RepertoarMD.Trajanje <= 0)
                    {
                        MessageBox.Show("Polje trajanje mora biti vece od 0!");
                        return;
                    }
                    else
                    {
                        if (RepertoarMD.MenadzerIdRadnika <= 0)
                        {
                            MessageBox.Show("Menadzer mora biti odabran!");
                            return;
                        }
                    }
                    
                    #endregion
                    access.Repertoars.Add(RepertoarMD);


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    Repertoari = GetAll();
                    RepertoarMD = new Repertoar();
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
                    #region Validation

                    if (RepertoarMD.Naziv == null)
                    {
                        MessageBox.Show("Polje naziv mora biti popunjeno!");
                        return;
                    }
                    if (RepertoarMD.Trajanje <= 0)
                    {
                        MessageBox.Show("Polje trajanje mora biti vece od 0!");
                        return;
                    }
                    else
                    {
                        if (RepertoarMD.MenadzerIdRadnika <= 0)
                        {
                            MessageBox.Show("Menadzer mora biti odabran!");
                            return;
                        }
                    }

                    #endregion
                    access.Repertoars.Where(n => n.IdRepertoara == SelektovaniRepertoar.IdRepertoara).FirstOrDefault().Naziv = RepertoarMD.Naziv;
                    access.Repertoars.Where(n => n.IdRepertoara == SelektovaniRepertoar.IdRepertoara).FirstOrDefault().Trajanje = RepertoarMD.Trajanje;
                    //menadzer


                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Repertoari = GetAll();

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
                    access.Repertoars.Remove(access.Repertoars.Where(n => n.IdRepertoara == SelektovaniRepertoar.IdRepertoara).FirstOrDefault());
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno obrisan objekat!");
                    }

                    Repertoari = GetAll();
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
                    RepertoarMD = new Repertoar();
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
                    RepertoarMD = SelektovaniRepertoar;

                    break;
            }
        }

        #region Property
        public Repertoar RepertoarMD
        {
            get { return repertoarMD; }
            set
            {

                if (value != repertoarMD)
                {
                    repertoarMD = value;
                    OnPropertyChanged("RepertoarMD");
                }
            }
        }
        public Repertoar SelektovaniRepertoar
        {
            get { return selektovaniRepertoar; }
            set
            {

                if (value != selektovaniRepertoar)
                {
                    selektovaniRepertoar = value;
                    OnPropertyChanged("SelektovaniRepertoar");
                    RepertoarMD = SelektovaniRepertoar;

                }
            }
        }
        public BindingList<Repertoar> Repertoari
        {
            get { return repertoari; }
            set
            {

                if (value != repertoari)
                {
                    repertoari = value;
                    OnPropertyChanged("Repertoari");
                }
            }
        }

        public Dictionary<int,string> Menadzeri
        {
            get { return menadzeri; }
            set
            {

                if (value != menadzeri)
                {
                    menadzeri = value;
                    OnPropertyChanged("Menadzeri");
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

