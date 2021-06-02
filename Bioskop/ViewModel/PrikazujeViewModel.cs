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
    public class PrikazujeViewModel : BindableBase
    {
        private bool isVisibleModifikuj;
        private bool isVisibleObrisi;
        private bool isVisibleDodaj;
        private bool isVisibleStek;

        private Prikazuje prikazujeMD;
        private Prikazuje selektovanoPrikazivanje;
        public BindingList<Prikazuje> prikazivanja;
        private BindingList<int> filmovi;
        private BindingList<int> sale;

        public ICommand NavCommand { get; private set; }
        public ICommand DodajCommand { get; private set; }
        public ICommand ModifikujCommand { get; private set; }
        public ICommand ObrisiCommand { get; private set; }

        public PrikazujeViewModel()
        {
            IsVisibleModifikuj = false;
            IsVisibleDodaj = false;
            IsVisibleObrisi = false;
            IsVisibleStek = false;

            NavCommand = new MyICommand<string>(OnNav);
            DodajCommand = new MyICommand(OnDodaj);
            ModifikujCommand = new MyICommand(OnModifikuj);
            ObrisiCommand = new MyICommand(OnObrisi);

            PrikazujeMD = new Prikazuje();
            SelektovanoPrikazivanje = new Prikazuje();
            Prikazivanja = GetAll();
            Filmovi = GetAllFilms();
            Sale = GetAllSalas();
        }

        public BindingList<int> GetAllFilms()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Films;

                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {
                    vs.Add(v.IdFilma);
                }
                return vs;
            }
        }
        public BindingList<int> GetAllSalas()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Sadrzis;

                BindingList<int> vs = new BindingList<int>();
                foreach (var v in vl)
                {   if(!vs.Contains(v.SalaIdSale))
                        vs.Add(v.SalaIdSale);
                }
                return vs;
            }
        }
        public BindingList<Prikazuje> GetAll()
        {
            using (var access = new ModelContainer())
            {
                var vl = access.Prikazujes;

                BindingList<Prikazuje> vs = new BindingList<Prikazuje>();

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
                    if (PrikazujeMD.SadrziSalaIdSale <= 0)
                    {
                        MessageBox.Show("Polje Sala mora biti popunjeno!");
                        return;

                    }
                    else if (PrikazujeMD.FilmIdFilma <= 0)
                    {
                        MessageBox.Show("Polje film mora biti popunjeno!");
                        return;
                    }
                    else if(PrikazujeMD.Termin.ToString().Contains("01/01/0001 00:00:00"))
                    {
                        MessageBox.Show("Polje termin mora biti popunjeno!");
                        return;
                    }
                    #endregion
                    access.Prikazujes.Add(PrikazujeMD);

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno dodat objekat!");
                    }

                    PrikazujeMD = new Prikazuje();
                    Prikazivanja = GetAll();
                }
                catch (DbUpdateException e)
                {
                    MessageBox.Show("Prikazivanje sa tim imenom vec postoji!");
                }

            }
        }
        public void OnModifikuj()
        {
            using (var access = new ModelContainer())
            {
                try
                {
                    access.Prikazujes.FirstOrDefault(n => n.IdPrikazivanja == SelektovanoPrikazivanje.IdPrikazivanja).FilmIdFilma = PrikazujeMD.FilmIdFilma;
                    access.Prikazujes.FirstOrDefault(n => n.IdPrikazivanja == SelektovanoPrikazivanje.IdPrikazivanja).SadrziSalaIdSale = PrikazujeMD.SadrziSalaIdSale;
                    access.Prikazujes.FirstOrDefault(n => n.IdPrikazivanja == SelektovanoPrikazivanje.IdPrikazivanja).Termin = PrikazujeMD.Termin;

                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspesno modifikovan objekat!");
                    }

                    Prikazivanja = GetAll();

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
                    var prikazivanje = access.Prikazujes.FirstOrDefault(n => n.IdPrikazivanja == SelektovanoPrikazivanje.IdPrikazivanja);
                    prikazivanje.Kartas.Clear();
                    var karta = access.Kartas.FirstOrDefault(x => x.PrikazujeIdPrikazivanja == prikazivanje.IdPrikazivanja);
                    if (karta != null)
                    { 
                        access.Kartas.Remove(karta);
                        access.Prikazujes.Remove(prikazivanje);
                    }
                    else
                    {
                        access.Prikazujes.Remove(prikazivanje);

                    }
                    
                    int success = access.SaveChanges();
                    if (success > 0)
                    {
                        MessageBox.Show("Uspijesno obrisan objekat!");
                    }

                    Prikazivanja = GetAll();
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
                    PrikazujeMD = new Prikazuje();
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
                    PrikazujeMD = SelektovanoPrikazivanje;
                    

                    break;
            }
        }

        #region Property
        public Prikazuje PrikazujeMD
        {
            get { return prikazujeMD; }
            set
            {

                if (value != prikazujeMD)
                {
                    prikazujeMD = value;
                    OnPropertyChanged("PrikazujeMD");
                }
            }
        }
        public Prikazuje SelektovanoPrikazivanje
        {
            get { return selektovanoPrikazivanje; }
            set
            {

                if (value != selektovanoPrikazivanje)
                {
                    selektovanoPrikazivanje = value;
                    OnPropertyChanged("SelektovanoPrikazivanje");
                    if (IsVisibleModifikuj == true)
                    {
                        PrikazujeMD = SelektovanoPrikazivanje;
                        
                    }
                }
            }
        }
        public BindingList<Prikazuje> Prikazivanja
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

        public BindingList<int> Filmovi { get => filmovi; set => filmovi = value; }
        public BindingList<int> Sale { get => sale; set => sale = value; }
        #endregion*/

    }
}
